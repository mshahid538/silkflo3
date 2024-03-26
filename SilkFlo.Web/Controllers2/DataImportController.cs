using System;
using SilkFlo.Data.Core;
using SilkFlo.Web.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using SilkFlo.Web.APIControllers.Apis;
using SilkFlo.Web.ViewModels;
using System.Collections.Generic;
using ExcelDataReader;
using System.Linq;
using SilkFlo.Web.ViewModels.Dashboard;
using System.Threading;
using MediatR;
using Silkflo.API.Services.ImportProcessState.Commands;
using Silkflo.API.Services.ImportProcessState.Queries;
using SilkFlo.Data.Core.Domain.Shared;

namespace SilkFlo.Web.Controllers
{
    public class DataImportController : AbstractAPI
    {
        private readonly IConfiguration _configuration;
        static Dictionary<string, string> ImportStatus;
        private readonly IMediator _mediator;

		public DataImportController(IUnitOfWork unitOfWork, ViewToString viewToString, IAuthorizationService authorization, IConfiguration configuration, IMediator mediator)
            : base(unitOfWork, viewToString, authorization)
        {
            _configuration = configuration;
            ImportStatus = new Dictionary<string, string>();
            _mediator = mediator;
		}

        [HttpPost("/Data/Import")]
        public async Task<IActionResult> ImportByFile([FromForm] IFormFile File)
        {
            try
            {
                #region ValidationAndPermissionCheck
                var feedback = new Feedback();
                var tenant = await GetClientAsync();

                // Permission Clause
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                {
                    feedback.DangerMessage("You are not authorised save an idea.");
                    return BadRequest(feedback);
                }

                // Permission Clause
                if (!(await AuthorizeAsync(Policy.SubmitCoEDrivenIdeas)).Succeeded)
                {
                    feedback.DangerMessage("You do not have permission to save a centre of excellence driven automation idea.");
                    return BadRequest(feedback);
                }

                // Permission Clause
                if (!(await AuthorizeAsync(Policy.ReviewNewIdeas)).Succeeded
                    && !(await AuthorizeAsync(Policy.ReviewAssessedIdeas)).Succeeded
                    && !(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
                {
                    feedback.DangerMessage("You do not have permission to save this idea.");
                    return BadRequest(feedback);
                }

                // Can add process permissions Clause
                var message = await CanAddProcess(new Models.Business.Client(tenant), "Cannot add additional process ideas.");
                if (!string.IsNullOrWhiteSpace(message))
                {
                    feedback.WarningMessage(message);
                    return BadRequest(feedback);
                }
                #endregion

                List<COEBulkIdeaModel> rows = null;
				dynamic lookups;
                if (File.FileName.ToLower().Contains(".xlsx") || File.FileName.ToLower().Contains(".xls"))
                {
                    //validation
                    if (File.Length == long.MinValue)
                    {
                        feedback.DangerMessage("Invalid or empty File.");
                        return BadRequest(feedback);
                    }

                    var uploadResult = await UploadCeoExcelFile(File);
                    if (!uploadResult.success)
                    {
                        return Ok(new { status = false, message = uploadResult.message });
                    }

                    rows = uploadResult.rows;
					lookups = uploadResult.lookups;
                }
                else
                {
                    return Ok(new { status = false, message = "Invalid File. Please select a valid file format.", });
                }

                return Ok(new { status = true, message = "Ceo Ideas Added successfully.", data = rows, look=lookups });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost("Data/Validate")]
        public async Task<IActionResult> ValidateRows([FromBody] JArray tableData)
        {
			var userId = GetUserId();
			var tenant = await GetClientAsync();

			var rows = tableData.ToObject<List<COEBulkIdeaModel>>();

            foreach(var row in rows)
            {
				var hasDuplicateEntry = _unitOfWork.CheckIdeasWithExistingName(new List<string>() { row.Name }, tenant.Id);
				if (hasDuplicateEntry)
				{
					row.HasError = true;
					row.ErrorMessage = "Some Idea(s) contains duplicate Name.";
				}
			}

			if (rows.Any(x => x.HasError == true))
				return BadRequest(new { message = "Some idea(s) have the duplicate name(s)", data = rows });

            return Ok(new { message = "Validation completed", data = rows });
		}

        [HttpPost("/Data/Save")]
        public async Task<IActionResult> SaveByFile([FromBody] JArray tableData)
        {
            try
            {
                var userId = GetUserId();
                var tenant = await GetClientAsync();

				await _mediator.Send(new SaveImportProcessStateOfUserCommand()
                {
                    UserId = userId,
                    ClientId = tenant.Id,
                    State = "InProgress",
                    Time = DateTime.Now,
                });

				var rows = tableData.ToObject<List<COEBulkIdeaModel>>();

                var result = await SaveIdeas(rows, tenant.Id, userId);

                return Ok(new { status = true, message = "Import process started.", SuccessCount = result.Item1, FailedCount = result.Item2 });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = "Some error occurred during Import." });
            }
        }

        [HttpGet("/Data/Status")]
        public async Task<IActionResult> GetImportStatus()
        {
            var userId = GetUserId();
            var tenant = await GetClientAsync();

            if (!String.IsNullOrEmpty(userId) && tenant is not null)
            {
                return Ok(await _mediator.Send(new GetImportProcessStateOfUserQuery()
                {
                    UserId = userId,
                    ClientId = tenant.Id,
                }));
            }
            else
            {
                return Ok(new
                {
                    IsSucceed = true
                });
            }
        }

        public async Task<(int, int)> SaveIdeas(List<COEBulkIdeaModel> rows, string clientId, string userId)
        {
            try
            {
                List<Models.Business.Idea> idealist = new List<Models.Business.Idea>();

				foreach (var row in rows)
                {
                    var idea = new Models.Business.Idea();

                    idea.Name = row.Name;
                    idea.CreatedDate = DateTime.Now;
                    idea.CreatedById = (await _unitOfWork.Users.GetUsingEmailAsync(row.SubmitterEmailAddress?.Trim()))?.Id;

                    idea.ProcessOwnerId = string.IsNullOrWhiteSpace(row.ProcessOwnerEmailAddress?.Trim())
                        ? null
                        : (await _unitOfWork.Users.GetUsingEmailAsync(row.ProcessOwnerEmailAddress.Trim()))?.Id;

                    if (idea.ProcessOwnerId == null)
                    {
                        idea.ProcessOwnerId = idea.CreatedById;
                    }



					//idea.DepartmentId = row.Department != null ? (await _unitOfWork.BusinessDepartments.SingleOrDefaultAsync(x => x.ClientId == clientId && x.Name.Trim().ToLower() == row.Department.Trim().ToLower()))?.Id : null;
					//idea.TeamId = row.Team != null ? (await _unitOfWork.BusinessTeams.SingleOrDefaultAsync(x => x.ClientId == clientId && x.Name.Trim().ToLower() == row.Team.Trim().ToLower()))?.Id : null;

					//idea.ProcessId = row.Process != null ? (await _unitOfWork.BusinessProcesses.SingleOrDefaultAsync(x => x.ClientId == clientId && x.Name.Trim().ToLower() == row.Process.Trim().ToLower()))?.Id : null;
					//idea.DeployeementDate = row.DeployeementDate;   pending
					//idea.RuleId = row.Rule != null ? (await _unitOfWork.SharedRules.GetByNameAsync(row.Rule.Trim().ToLower()))?.Id : null;
					//idea.InputId = row.Input != null ? (await _unitOfWork.SharedInputs.GetByNameAsync(row.Input.Trim().ToLower()))?.Id : null;
					//idea.InputDataStructureId = row.InputDataStructure != null ? (await _unitOfWork.SharedInputDataStructures.GetByNameAsync(row.InputDataStructure.Trim().ToLower()))?.Id : null;
					//idea.ProcessStabilityId = row.ProcessStability != null ? (await _unitOfWork.SharedProcessStabilities.GetByNameAsync(row.ProcessStability.Trim().ToLower()))?.Id : null;
					//idea.DocumentationPresentId = row.DocumentationPresent != null ? (await _unitOfWork.SharedDocumentationPresents.GetByNameAsync(row.DocumentationPresent.Trim().ToLower()))?.Id : null;

					//idea.AutomationGoalId = row.AutomationGoal != null ? (await _unitOfWork.SharedAutomationGoals.GetByNameAsync(row.AutomationGoal.Trim().ToLower()))?.Id : null;

					//idea.ApplicationStabilityId = row.ApplicationStability != null ? (await _unitOfWork.SharedApplicationStabilities.GetByNameAsync(row.ApplicationStability.Trim().ToLower()))?.Id : null;

					//idea.TaskFrequencyId = row.TaskFrequency != null ? (await _unitOfWork.SharedTaskFrequencies.GetByNameAsync(row.TaskFrequency.Trim().ToLower()))?.Id : null;
					//idea.ProcessPeakId = row.ProcessPeak != null ? (await _unitOfWork.SharedProcessPeaks.GetByNameAsync(row.ProcessPeak.Trim().ToLower()))?.Id : null;
					//idea.AverageNumberOfStepId = row.AverageNumberOfStep != null ? (await _unitOfWork.SharedAverageNumberOfSteps.GetByNameAsync(row.AverageNumberOfStep.Trim().ToLower()))?.Id : null;
					//idea.NumberOfWaysToCompleteProcessId = row.NumberOfWaysToCompleteProcess != null ? (await _unitOfWork.SharedNumberOfWaysToCompleteProcesses.GetByNameAsync(row.NumberOfWaysToCompleteProcess.Trim().ToLower()))?.Id : null;
					//idea.DataInputPercentOfStructuredId = row.DataInputPercentOfStructured != null ? (await _unitOfWork.SharedDataInputPercentOfStructureds.GetByNameAsync(row.DataInputPercentOfStructured.Trim().ToLower()))?.Id : null;
					//idea.DecisionCountId = row.DecisionCount != null ? (await _unitOfWork.SharedDecisionCounts.GetByNameAsync(row.DecisionCount.Trim().ToLower()))?.Id : null;
					//idea.DecisionDifficultyId = row.DecisionDifficulty != null ? (await _unitOfWork.SharedDecisionDifficulties.GetByNameAsync(row.DecisionDifficulty.Trim().ToLower()))?.Id : null;







					idea.Summary = row.Description;
					idea.DepartmentId = row.Department;
					idea.TeamId = row.Team;
					idea.ProcessId = row.SubArea;
					idea.RuleId = row.Rule;
					idea.InputId = row.Input;
					idea.InputDataStructureId = row.InputDataStructure;
					idea.ProcessStabilityId = row.ProcessStability;
					idea.DocumentationPresentId = row.DocumentationPresent;
					idea.AutomationGoalId = row.AutomationGoal;
					idea.ApplicationStabilityId = row.ApplicationStability;
					idea.TaskFrequencyId = row.TaskFrequency;
					idea.AverageReworkTime = row.AverageReworkTime;
					idea.ProcessPeakId = row.ProcessPeak;
					idea.AverageNumberOfStepId = row.AverageNumberOfStep;
					idea.NumberOfWaysToCompleteProcessId = row.NumberOfWaysToCompleteProcess;
					idea.DataInputPercentOfStructuredId = row.DataInputPercentOfStructured;
					idea.DecisionCountId = row.DecisionCount;
					idea.DecisionDifficultyId = row.DecisionDifficulty;
					idea.AverageWorkingDay = row.AverageWorkingDay;
					idea.AverageEmployeeFullCost = row.AverageEmployeeFullCost;
					idea.ActivityVolumeAverage = row.ActivityVolumeAverage;
					idea.EmployeeCount = row.EmployeeCount;
					idea.AverageErrorRate = row.AverageErrorRate;
					idea.WorkingHour = row.WorkingHour;
					idea.AverageProcessingTime = row.AverageProcessingTime;
					idea.AverageReviewTime = row.AverageReviewTime;
					idea.AverageWorkToBeReviewed = row.AverageWorkToBeReviewed;
					idea.PotentialFineAmount = row.PotentialFineAmount;
					idea.PotentialFineProbability = row.PotentialFineProbability;
					idea.IsHighRisk = (row.IsHighRisk?.ToLower()) == "yes";
					idea.IsDataSensitive = (row.IsDataSensitive?.ToLower()) == "yes";
					idea.IsAlternative = (row.IsAlternative?.ToLower()) == "yes";
					idea.IsHostUpgrade = (row.IsHostUpgrade?.ToLower()) == "yes";
					idea.IsDataInputScanned = (row.IsDataInputScanned?.ToLower()) == "yes";
					idea.SubmissionPathId = "COEUser";
					idea.ImportStage = row.Stage?.Trim() ?? "";
					idea.ImportStatus = row.Status?.Trim() ?? "";

					//TODO: discuss CreatedById
					if (string.IsNullOrEmpty(idea.Name) ||
                        string.IsNullOrEmpty(idea.Summary) ||
                        string.IsNullOrEmpty(idea.DepartmentId) ||
                        string.IsNullOrEmpty(idea.RuleId) ||
                        string.IsNullOrEmpty(idea.InputId) ||
                        string.IsNullOrEmpty(idea.InputDataStructureId) ||
                        string.IsNullOrEmpty(idea.ProcessStabilityId) ||
                        string.IsNullOrEmpty(idea.DocumentationPresentId) ||
                        string.IsNullOrEmpty(idea.ApplicationStabilityId)
                        )
                    {
                        idea.IsDraft = true;
                    }

                    idealist.Add(idea);
                }

                List<Data.Core.Domain.Business.Idea> cores = new List<Data.Core.Domain.Business.Idea>(); //.Select(x => x.GetCore()).ToList();
                idealist.ForEach((v) =>
                {
                    var idea = v.GetCore();
                    idea.ClientId = clientId;
                    idea.ImportStage = v.ImportStage;
                    idea.ImportStatus = v.ImportStatus;
                    cores.Add(idea);

                });
                var result = await _unitOfWork.ImportBulkIdeas(cores);
                await _unitOfWork.CompleteAsync();

                var failedCount = cores.Count - result.Item2;
                await SaveIdeasWorkflows(result.Item1);

				await _mediator.Send(new SaveImportProcessStateOfUserCommand()
				{
					UserId = userId,
					ClientId = clientId,
					State = "Completed",
					Time = DateTime.Now,
                    SuccessCount = result.Item2,
                    FailedCount = failedCount,
				});

				return (result.Item2, failedCount);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveIdeasWorkflows(List<Data.Core.Domain.Business.Idea> ideas)
        {
            List<Data.Core.Domain.Business.IdeaStage> ideaStages = new List<Data.Core.Domain.Business.IdeaStage>();
            List<Data.Core.Domain.Business.IdeaStageStatus> ideaStageStasues = new List<Data.Core.Domain.Business.IdeaStageStatus>();

            foreach (var idea in ideas)
            {
				Enumerators.Stage ideaStageId = Data.Core.Enumerators.Stage.n01_Assess;
				if (!String.IsNullOrEmpty(idea.ImportStage) && !String.IsNullOrEmpty(idea.ImportStatus))
				{
					if (idea.ImportStage.Equals("Idea", StringComparison.OrdinalIgnoreCase))
					{
						ideaStageId = Data.Core.Enumerators.Stage.n00_Idea;
						var ideaStageObj = new Data.Core.Domain.Business.IdeaStage
						{
							Idea = idea,
							StageId = ideaStageId.ToString(),
							DateStartEstimate = DateTime.Now,
							DateStart = DateTime.Now,
							IsInWorkFlow = true,
						};

						await _unitOfWork.AddAsync(ideaStageObj);
						await _unitOfWork.CompleteAsync();

						var statusId = Data.Core.Enumerators.IdeaStatus.n00_Idea_AwaitingReview.ToString();
						if (idea.ImportStatus.Equals("Duplicate", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n01_Idea_Duplicate.ToString();
						}
						else if (idea.ImportStatus.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n02_Idea_Rejected.ToString();
						}
						else if (idea.ImportStatus.Equals("Archived", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n03_Idea_Archived.ToString();
						}

						var ideaStageStatusObj = new Data.Core.Domain.Business.IdeaStageStatus
						{
							IdeaStageId = ideaStageObj.Id,
							StatusId = statusId,
							Date = DateTime.Now
						};
						await _unitOfWork.AddAsync(ideaStageStatusObj);
						await _unitOfWork.CompleteAsync();
					}
					if (idea.ImportStage.Equals("Assess", StringComparison.OrdinalIgnoreCase))
					{
						ideaStageId = Data.Core.Enumerators.Stage.n01_Assess;
						var ideaStageObj = new Data.Core.Domain.Business.IdeaStage
						{
							Idea = idea,
							StageId = ideaStageId.ToString(),
							DateStartEstimate = DateTime.Now,
							DateStart = DateTime.Now,
							IsInWorkFlow = true,
						};

						await _unitOfWork.AddAsync(ideaStageObj);
						await _unitOfWork.CompleteAsync();

						var statusId = Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString();
						if (idea.ImportStatus.Equals("NotStarted", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n05_Assess_NotStarted.ToString();
						}
						else if (idea.ImportStatus.Equals("InProgress", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress.ToString();
						}
						else if (idea.ImportStatus.Equals("OnHold", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n07_Assess_OnHold.ToString();
						}
						else if (idea.ImportStatus.Equals("Postponed", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n08_Assess_Postponed.ToString();
						}
						else if (idea.ImportStatus.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n09_Assess_Rejected.ToString();
						}
						else if (idea.ImportStatus.Equals("Archived", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n10_Assess_Archived.ToString();
						}

						var ideaStageStatusObj = new Data.Core.Domain.Business.IdeaStageStatus
						{
							IdeaStageId = ideaStageObj.Id,
							StatusId = statusId,
							Date = DateTime.Now
						};
						await _unitOfWork.AddAsync(ideaStageStatusObj);
						await _unitOfWork.CompleteAsync();
					}
					if (idea.ImportStage.Equals("Qualify", StringComparison.OrdinalIgnoreCase))
					{
						ideaStageId = Data.Core.Enumerators.Stage.n02_Qualify;
						var ideaStageObj = new Data.Core.Domain.Business.IdeaStage
						{
							Idea = idea,
							StageId = ideaStageId.ToString(),
							DateStartEstimate = DateTime.Now,
							DateStart = DateTime.Now,
							IsInWorkFlow = true,
						};

						await _unitOfWork.AddAsync(ideaStageObj);
						await _unitOfWork.CompleteAsync();

						var statusId = Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString();
						if (idea.ImportStatus.Equals("NotStarted", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n05_Assess_NotStarted.ToString();
						}
						else if (idea.ImportStatus.Equals("InProgress", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress.ToString();
						}
						else if (idea.ImportStatus.Equals("OnHold", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n07_Assess_OnHold.ToString();
						}
						else if (idea.ImportStatus.Equals("Postponed", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n08_Assess_Postponed.ToString();
						}
						else if (idea.ImportStatus.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n09_Assess_Rejected.ToString();
						}
						else if (idea.ImportStatus.Equals("Archived", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n10_Assess_Archived.ToString();
						}

						var ideaStageStatusObj = new Data.Core.Domain.Business.IdeaStageStatus
						{
							IdeaStageId = ideaStageObj.Id,
							StatusId = statusId,
							Date = DateTime.Now
						};
						await _unitOfWork.AddAsync(ideaStageStatusObj);
						await _unitOfWork.CompleteAsync();
					}
					if (idea.ImportStage.Equals("Analysis", StringComparison.OrdinalIgnoreCase))
					{
						ideaStageId = Data.Core.Enumerators.Stage.n03_Analysis;
						var ideaStageObj = new Data.Core.Domain.Business.IdeaStage
						{
							Idea = idea,
							StageId = ideaStageId.ToString(),
							DateStartEstimate = DateTime.Now,
							DateStart = DateTime.Now,
							IsInWorkFlow = true,
						};

						await _unitOfWork.AddAsync(ideaStageObj);
						await _unitOfWork.CompleteAsync();

						var statusId = Data.Core.Enumerators.IdeaStatus.n16_Analysis_NotStarted.ToString();
						if (idea.ImportStatus.Equals("NotStarted", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n16_Analysis_NotStarted.ToString();
						}
						else if (idea.ImportStatus.Equals("InProgress", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n17_Analysis_InProgress.ToString();
						}
						else if (idea.ImportStatus.Equals("OnHold", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n18_Analysis_OnHold.ToString();
						}
						else if (idea.ImportStatus.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n19_Analysis_Cancelled.ToString();
						}
						else if (idea.ImportStatus.Equals("AtRisk", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n20_Analysis_AtRisk.ToString();
						}
						else if (idea.ImportStatus.Equals("Delayed", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n21_Analysis_Delayed.ToString();
						}
						else if (idea.ImportStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n22_Analysis_Completed.ToString();
						}
						else if (idea.ImportStatus.Equals("Archived", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n23_Analysis_Archived.ToString();
						}

						var ideaStageStatusObj = new Data.Core.Domain.Business.IdeaStageStatus
						{
							IdeaStageId = ideaStageObj.Id,
							StatusId = statusId,
							Date = DateTime.Now
						};
						await _unitOfWork.AddAsync(ideaStageStatusObj);
						await _unitOfWork.CompleteAsync();
					}
					if (idea.ImportStage.Equals("SolutionDesign", StringComparison.OrdinalIgnoreCase))
					{
						ideaStageId = Data.Core.Enumerators.Stage.n04_SolutionDesign;
						var ideaStageObj = new Data.Core.Domain.Business.IdeaStage
						{
							Idea = idea,
							StageId = ideaStageId.ToString(),
							DateStartEstimate = DateTime.Now,
							DateStart = DateTime.Now,
							IsInWorkFlow = true,
						};

						await _unitOfWork.AddAsync(ideaStageObj);
						await _unitOfWork.CompleteAsync();

						var statusId = Data.Core.Enumerators.IdeaStatus.n24_SolutionDesign_NotStarted.ToString();
						if (idea.ImportStatus.Equals("NotStarted", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n24_SolutionDesign_NotStarted.ToString();
						}
						else if (idea.ImportStatus.Equals("InProgress", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n25_SolutionDesign_InProgress.ToString();
						}
						else if (idea.ImportStatus.Equals("OnHold", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n26_SolutionDesign_OnHold.ToString();
						}
						else if (idea.ImportStatus.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n27_SolutionDesign_Cancelled.ToString();
						}
						else if (idea.ImportStatus.Equals("AtRisk", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n28_SolutionDesign_AtRisk.ToString();
						}
						else if (idea.ImportStatus.Equals("Delayed", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n29_SolutionDesign_Delayed.ToString();
						}
						else if (idea.ImportStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n30_SolutionDesign_Completed.ToString();
						}
						else if (idea.ImportStatus.Equals("Archived", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n31_SolutionDesign_Archived.ToString();
						}

						var ideaStageStatusObj = new Data.Core.Domain.Business.IdeaStageStatus
						{
							IdeaStageId = ideaStageObj.Id,
							StatusId = statusId,
							Date = DateTime.Now
						};
						await _unitOfWork.AddAsync(ideaStageStatusObj);
						await _unitOfWork.CompleteAsync();
					}
					if (idea.ImportStage.Equals("Development", StringComparison.OrdinalIgnoreCase))
					{
						ideaStageId = Data.Core.Enumerators.Stage.n05_Development;
						var ideaStageObj = new Data.Core.Domain.Business.IdeaStage
						{
							Idea = idea,
							StageId = ideaStageId.ToString(),
							DateStartEstimate = DateTime.Now,
							DateStart = DateTime.Now,
							IsInWorkFlow = true,
						};

						await _unitOfWork.AddAsync(ideaStageObj);
						await _unitOfWork.CompleteAsync();

						var statusId = Data.Core.Enumerators.IdeaStatus.n32_Development_NotStarted.ToString();
						if (idea.ImportStatus.Equals("NotStarted", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n32_Development_NotStarted.ToString();
						}
						else if (idea.ImportStatus.Equals("InProgress", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n33_Development_InProgress.ToString();
						}
						else if (idea.ImportStatus.Equals("OnHold", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n34_Development_OnHold.ToString();
						}
						else if (idea.ImportStatus.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n35_Development_Cancelled.ToString();
						}
						else if (idea.ImportStatus.Equals("AtRisk", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n36_Development_AtRisk.ToString();
						}
						else if (idea.ImportStatus.Equals("Delayed", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n37_Development_Delayed.ToString();
						}
						else if (idea.ImportStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n38_Development_Completed.ToString();
						}
						else if (idea.ImportStatus.Equals("Archived", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n39_Development_Archived.ToString();
						}

						var ideaStageStatusObj = new Data.Core.Domain.Business.IdeaStageStatus
						{
							IdeaStageId = ideaStageObj.Id,
							StatusId = statusId,
							Date = DateTime.Now
						};
						await _unitOfWork.AddAsync(ideaStageStatusObj);
						await _unitOfWork.CompleteAsync();
					}
					if (idea.ImportStage.Equals("Testing", StringComparison.OrdinalIgnoreCase))
					{
						ideaStageId = Data.Core.Enumerators.Stage.n06_Testing;
						var ideaStageObj = new Data.Core.Domain.Business.IdeaStage
						{
							Idea = idea,
							StageId = ideaStageId.ToString(),
							DateStartEstimate = DateTime.Now,
							DateStart = DateTime.Now,
							IsInWorkFlow = true,
						};

						await _unitOfWork.AddAsync(ideaStageObj);
						await _unitOfWork.CompleteAsync();

						var statusId = Data.Core.Enumerators.IdeaStatus.n40_Testing_NotStarted.ToString();
						if (idea.ImportStatus.Equals("NotStarted", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n40_Testing_NotStarted.ToString();
						}
						else if (idea.ImportStatus.Equals("InProgress", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n41_Testing_InProgress.ToString();
						}
						else if (idea.ImportStatus.Equals("OnHold", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n42_Testing_OnHold.ToString();
						}
						else if (idea.ImportStatus.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n43_Testing_Cancelled.ToString();
						}
						else if (idea.ImportStatus.Equals("AtRisk", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n44_Testing_AtRisk.ToString();
						}
						else if (idea.ImportStatus.Equals("Delayed", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n45_Testing_Delayed.ToString();
						}
						else if (idea.ImportStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n46_Testing_Completed.ToString();
						}
						else if (idea.ImportStatus.Equals("Archived", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n47_Testing_Archived.ToString();
						}

						var ideaStageStatusObj = new Data.Core.Domain.Business.IdeaStageStatus
						{
							IdeaStageId = ideaStageObj.Id,
							StatusId = statusId,
							Date = DateTime.Now
						};
						await _unitOfWork.AddAsync(ideaStageStatusObj);
						await _unitOfWork.CompleteAsync();
					}
					if (idea.ImportStage.Equals("Deployed", StringComparison.OrdinalIgnoreCase))
					{
						ideaStageId = Data.Core.Enumerators.Stage.n07_Deployed;
						var ideaStageObj = new Data.Core.Domain.Business.IdeaStage
						{
							Idea = idea,
							StageId = ideaStageId.ToString(),
							DateStartEstimate = DateTime.Now,
							DateStart = DateTime.Now,
							IsInWorkFlow = true,
						};

						await _unitOfWork.AddAsync(ideaStageObj);
						await _unitOfWork.CompleteAsync();

						var statusId = Data.Core.Enumerators.IdeaStatus.n48_Deployed_ReadyForProduction.ToString();
						if (idea.ImportStatus.Equals("ReadyForProduction", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n32_Development_NotStarted.ToString();
						}
						else if (idea.ImportStatus.Equals("HyperCare", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n49_Deployed_HyperCare.ToString();
						}
						else if (idea.ImportStatus.Equals("OnHold", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n50_Deployed_OnHold.ToString();
						}
						else if (idea.ImportStatus.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n35_Development_Cancelled.ToString();
						}
						else if (idea.ImportStatus.Equals("InProduction", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n51_Deployed_InProduction.ToString();
						}
						else if (idea.ImportStatus.Equals("Archived", StringComparison.OrdinalIgnoreCase))
						{
							statusId = Data.Core.Enumerators.IdeaStatus.n52_Deployed_Archived.ToString();
						}

						var ideaStageStatusObj = new Data.Core.Domain.Business.IdeaStageStatus
						{
							IdeaStageId = ideaStageObj.Id,
							StatusId = statusId,
							Date = DateTime.Now
						};
						await _unitOfWork.AddAsync(ideaStageStatusObj);
						await _unitOfWork.CompleteAsync();
					}
				}
				else
				{
					ideaStageId = Data.Core.Enumerators.Stage.n01_Assess;
					var ideaStage = new Data.Core.Domain.Business.IdeaStage
					{
						Idea = idea,
						StageId = ideaStageId.ToString(),
						DateStartEstimate = DateTime.Now,
						DateStart = DateTime.Now,
						IsInWorkFlow = true,
					};

					await _unitOfWork.AddAsync(ideaStage);
					await _unitOfWork.CompleteAsync();


					var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
					{
						IdeaStageId = ideaStage.Id,
						StatusId = Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
						Date = DateTime.Now
					};
					await _unitOfWork.AddAsync(ideaStageStatus);
					await _unitOfWork.CompleteAsync();
				}

                var stages = (await _unitOfWork.SharedStages.FindAsync(x => x.Id != ideaStageId.ToString())).ToArray();
                //if (ideaStageId == Data.Core.Enumerators.Stage.n01_Assess)
                //    stages = stages.Where(x => x.Id != Data.Core.Enumerators.Stage.n00_Idea.ToString()).ToArray();

                var now = DateTime.Now;
                foreach (var stage in stages)
                {
                    var remainingIdeaStage = new Data.Core.Domain.Business.IdeaStage
                    {
                        Idea = idea,
                        DateStartEstimate = now,
                        Stage = stage
                    };

                    now = now.AddSeconds(1);
                    await _unitOfWork.AddAsync(remainingIdeaStage);
                }
                await _unitOfWork.CompleteAsync();
            }
        }


        public async Task<(bool success, string message, List<COEBulkIdeaModel> rows, dynamic lookups)> UploadCeoExcelFile(IFormFile file)
        {
			var tenant = await GetClientAsync();
			List<COEBulkIdeaModel> rows = new List<COEBulkIdeaModel>();
            try
            {
                using (var stream = file.OpenReadStream())
                {
                    IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                    System.Data.DataSet dataSet = reader.AsDataSet(
                        new ExcelDataSetConfiguration()
                        {
                            UseColumnDataType = false,
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = false,
                            }
                        });

                    DateTime CreatedDate = DateTime.Now;
                    string formattedDate = CreatedDate.ToString("yyyy-MM-dd");

					if ((dataSet.Tables[0].Rows[1].ItemArray[0].ToString() == "Name of Automation*") &&
						 (dataSet.Tables[0].Rows[1].ItemArray[1].ToString() == "Date Submitted") &&
						  (dataSet.Tables[0].Rows[1].ItemArray[2].ToString() == "Submitter's Email Address *") &&
							(dataSet.Tables[0].Rows[1].ItemArray[5].ToString() == "Description *"))
					{
						for (int i = 3; i < dataSet.Tables[0].Rows.Count; i++)
						{
							var fileReader = new COEBulkIdeaModel
							{
								Name = dataSet.Tables[0].Rows[i].ItemArray[0] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[0]) : string.Empty,
								CreatedDate = formattedDate,
								SubmitterEmailAddress = dataSet.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[2]) : string.Empty,
								ProcessOwnerEmailAddress = dataSet.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[3]) : string.Empty,
								AutomationId = dataSet.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[4]) : string.Empty,
								Description = dataSet.Tables[0].Rows[i].ItemArray[5] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[5]) : string.Empty,
								Department = dataSet.Tables[0].Rows[i].ItemArray[6] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[6]) : string.Empty,
								Team = dataSet.Tables[0].Rows[i].ItemArray[7] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[7]) : string.Empty,
								SubArea = dataSet.Tables[0].Rows[i].ItemArray[8] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[8]) : string.Empty,
								Stage = dataSet.Tables[0].Rows[i].ItemArray[9] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[9]) : string.Empty,
								Status = dataSet.Tables[0].Rows[i].ItemArray[10] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[10]) : string.Empty,
								DeployementDate = DateTime.TryParse(Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[11]), out DateTime deployementDate) && !string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[11])) ? deployementDate : (DateTime?)null,
								Rule = dataSet.Tables[0].Rows[i].ItemArray[12] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[12]) : string.Empty,
								Input = dataSet.Tables[0].Rows[i].ItemArray[13] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[13]) : string.Empty,
								InputDataStructure = dataSet.Tables[0].Rows[i].ItemArray[14] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[14]) : string.Empty,
								ProcessStability = dataSet.Tables[0].Rows[i].ItemArray[15] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[15]) : string.Empty,
								DocumentationPresent = dataSet.Tables[0].Rows[i].ItemArray[16] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[16]) : string.Empty,
								AutomationGoal = dataSet.Tables[0].Rows[i].ItemArray[17] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[17]) : string.Empty,
								ApplicationStability = dataSet.Tables[0].Rows[i].ItemArray[18] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[18]) : string.Empty,
								AverageWorkingDay = int.TryParse(dataSet.Tables[0].Rows[i].ItemArray[19]?.ToString(), out int parsedValue) ? parsedValue : (int?)null,
								WorkingHour = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[20]?.ToString(), out decimal workingHourResult) ? workingHourResult : (decimal?)null,
								AverageEmployeeFullCost = int.TryParse(dataSet.Tables[0].Rows[i].ItemArray[21]?.ToString(), out int averageEmployeeFullCostResult) ? averageEmployeeFullCostResult : (int?)null,
								EmployeeCount = int.TryParse(dataSet.Tables[0].Rows[i].ItemArray[22]?.ToString(), out int employeeCountResult) ? employeeCountResult : (int?)null,
								TaskFrequency = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[23]) ?? string.Empty,
								ActivityVolumeAverage = dataSet.Tables[0].Rows[i].ItemArray[24] != null ? int.TryParse(dataSet.Tables[0].Rows[i].ItemArray[24].ToString(), out int activityVolumeResult) ? activityVolumeResult : (int?)null : (int?)null,
								AverageProcessingTime = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[25]?.ToString(), out decimal avgProcessingTimeResult) ? avgProcessingTimeResult : (decimal?)null,
								AverageErrorRate = int.TryParse(dataSet.Tables[0].Rows[i].ItemArray[26]?.ToString(), out int averageErrorRateResult) ? averageErrorRateResult : (int?)null,
								AverageReviewTime = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[27]?.ToString(), out decimal averageReviewTimeResult) ? averageReviewTimeResult : (decimal?)null,
								AverageWorkToBeReviewed = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[28]?.ToString(), out decimal averageWorkToBeReviewedResult) ? averageWorkToBeReviewedResult : (decimal?)null,
								AverageReworkTime = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[29]?.ToString(), out decimal averageReworkTimeResult) ? averageReworkTimeResult : (decimal?)null,
								ProcessPeak = dataSet.Tables[0].Rows[i].ItemArray[30]?.ToString() ?? string.Empty,
								AverageNumberOfStep = dataSet.Tables[0].Rows[i].ItemArray[31]?.ToString() ?? string.Empty,
								NumberOfWaysToCompleteProcess = dataSet.Tables[0].Rows[i].ItemArray[32]?.ToString() ?? string.Empty,
								DataInputPercentOfStructured = dataSet.Tables[0].Rows[i].ItemArray[33]?.ToString() ?? string.Empty,
								DecisionCount = dataSet.Tables[0].Rows[i].ItemArray[34]?.ToString() ?? string.Empty,
								DecisionDifficulty = dataSet.Tables[0].Rows[i].ItemArray[35]?.ToString() ?? string.Empty,
								PotentialFineAmount = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[36]?.ToString(), out decimal potentialFineAmountResult) ? potentialFineAmountResult : (decimal?)null,
								PotentialFineProbability = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[37]?.ToString(), out decimal potentialFineProbabilityResult) ? potentialFineProbabilityResult : (decimal?)null,
								IsHighRisk = dataSet.Tables[0].Rows[i].ItemArray[38]?.ToString() ?? string.Empty,
								IsDataSensitive = dataSet.Tables[0].Rows[i].ItemArray[39]?.ToString() ?? string.Empty,
								IsAlternative = dataSet.Tables[0].Rows[i].ItemArray[40]?.ToString() ?? string.Empty,
								IsHostUpgrade = dataSet.Tables[0].Rows[i].ItemArray[41]?.ToString() ?? string.Empty,
								IsDataInputScanned = dataSet.Tables[0].Rows[i].ItemArray[42]?.ToString() ?? string.Empty
							};

							if (String.IsNullOrWhiteSpace(fileReader.Name))
							{
								fileReader.HasError = true;
								fileReader.ErrorMessage = "Idea contain invalid or empty Name. Please click on 'Name' column to edit.";
							}

							if (fileReader.Name.Length > 100)
							{
								fileReader.HasError = true;
								fileReader.ErrorMessage = "Idea exceeds name limit, it should be 100 characters max. Please click on 'Name' column to edit.";
							}

							if (String.IsNullOrEmpty(fileReader.Description))
							{
								fileReader.HasError = true;
								fileReader.ErrorMessage = "Idea contain invalid or empty Description. Please click on 'Description' column to edit.";
							}

							if (fileReader.Description.Length > 10000)
							{
								fileReader.HasError = true;
								fileReader.ErrorMessage = "Idea exceeds description limit, it should be 10000 characters max. Please click on 'Description' column to edit.";
							}

							var hasDuplicateEntry = _unitOfWork.CheckIdeasWithExistingName(new List<string>() { fileReader.Name }, tenant.Id);
							if (hasDuplicateEntry)
							{
								fileReader.HasError = true;
								fileReader.ErrorMessage = "Idea contains duplicate Name. Please click on 'Name' column to edit.";
							}

							rows.Add(fileReader);
						}

						var lookups = new {
							Department = await _unitOfWork.BusinessDepartments.GetAllAsync(),
							Team = await _unitOfWork.BusinessTeams.GetAllAsync(),
							SubArea = await _unitOfWork.BusinessProcesses.GetAllAsync(),
							Rule = await _unitOfWork.SharedRules.GetAllAsync(),
							Input = await _unitOfWork.SharedInputs.GetAllAsync(),
							InputDataStructure = await _unitOfWork.SharedInputDataStructures.GetAllAsync(),
							ProcessStability = await _unitOfWork.SharedProcessStabilities.GetAllAsync(),
							DocumentationPresent = await _unitOfWork.SharedDocumentationPresents.GetAllAsync(),
							AutomationGoal = await _unitOfWork.SharedAutomationGoals.GetAllAsync(),
							ApplicationStability = await _unitOfWork.SharedApplicationStabilities.GetAllAsync(),
							TaskFrequency = await _unitOfWork.SharedTaskFrequencies.GetAllAsync(),
							ProcessPeak = await _unitOfWork.SharedProcessPeaks.GetAllAsync(),
							AverageNumberOfStep = await _unitOfWork.SharedAverageNumberOfSteps.GetAllAsync(),
							NumberOfWaysToCompleteProcess = await _unitOfWork.SharedNumberOfWaysToCompleteProcesses.GetAllAsync(),
							DataInputPercentOfStructured = await _unitOfWork.SharedDataInputPercentOfStructureds.GetAllAsync(),
							DecisionCount = await _unitOfWork.SharedDecisionCounts.GetAllAsync(),
							DecisionDifficulty = await _unitOfWork.SharedDecisionDifficulties.GetAllAsync(),
						    Stage = await _unitOfWork.SharedStages.GetAllAsync(),
						    Status = await _unitOfWork.SharedIdeaStatuses.GetAllAsync(),
						};

						return (true, "Upload successful.", rows, lookups);

                    }
                    else
                    {
                        return (false, "Incorrect format detected. Please download our template, add your ideas, and try again.", new List<COEBulkIdeaModel>(), null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
