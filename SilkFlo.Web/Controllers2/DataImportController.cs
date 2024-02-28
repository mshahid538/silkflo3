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

namespace SilkFlo.Web.Controllers
{
    public class DataImportController : AbstractAPI
    {
        private readonly IConfiguration _configuration;
		static Dictionary<string, string> ImportStatus;
        public DataImportController(IUnitOfWork unitOfWork, ViewToString viewToString, IAuthorizationService authorization, IConfiguration configuration)
            : base(unitOfWork, viewToString, authorization)
        {
            _configuration = configuration;
            ImportStatus = new Dictionary<string, string>();
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
				}
				else
				{
					return Ok(new { status = false, message = "Invalid File. Please select a valid file format.", });
				}

				return Ok(new { status = true, message = "Ceo Ideas Added successfully.", data = rows });
			}
			catch (Exception ex)
			{
				throw;
			}
		}


		[HttpPost("/Data/Save")]
		public async Task<IActionResult> SaveByFile([FromBody] JArray tableData)
		{
			int rowcount = 0;
			int failedideacount = 0;

			try
			{
				var rows = tableData.ToObject<List<COEBulkIdeaModel>>();
				rowcount = rows.Count;

				var ideas = Task.Run(() => SaveIdeas(rows));
				
				return Ok(new { status = true, message = "Import process started.", SuccessCount = rowcount - failedideacount, FailedCount = failedideacount });
			}
			catch (Exception ex)
			{
				return Ok(new { status = false, message = "Some error occurred during Import." });
			}
		}
		
		[HttpGet("/Data/Status")]
		public async Task<IActionResult> GetImportStatus()
		{
            var tenant = await GetClientAsync();
            return Ok(new 
			{
				IsCompleted = ImportStatus.GetValueOrDefault($"{tenant}-Completed"),
				SuccessCount = ImportStatus.GetValueOrDefault($"{tenant}-SuccessCount"),
				FailedCount = ImportStatus.GetValueOrDefault($"{tenant}FailedCount"),
            });
        }

		public async Task SaveIdeas(List<COEBulkIdeaModel> rows)
		{
			try
			{
                var tenant = await GetClientAsync();

				ImportStatus.Add($"{tenant}Completed", "0");
				ImportStatus.Add($"{tenant}SuccessCount", "0");
				ImportStatus.Add($"{tenant}FailedCount", "0");

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

                    idea.Summary = row.Description;
                    idea.DepartmentId = (await _unitOfWork.BusinessDepartments.SingleOrDefaultAsync(x => x.ClientId == tenant.Id && x.Name == row.DepartmentId?.Trim())).Id;
                    idea.TeamId = (await _unitOfWork.BusinessTeams.SingleOrDefaultAsync(x => x.ClientId == tenant.Id && x.Name == row.TeamId?.Trim())).Id;
                    idea.ProcessId = (await _unitOfWork.BusinessProcesses.SingleOrDefaultAsync(x => x.ClientId == tenant.Id && x.Name == row.ProcessId?.Trim())).Id;

                    //idea.DeployeementDate = row.DeployeementDate;   pending
                    idea.RuleId = (await _unitOfWork.SharedRules.GetByNameAsync(row.RuleId))?.Id;
                    idea.InputId = (await _unitOfWork.SharedInputs.GetByNameAsync(row.InputId))?.Id;
                    idea.InputDataStructureId = (await _unitOfWork.SharedInputDataStructures.GetByNameAsync(row.InputDataStructureId))?.Id;
                    idea.ProcessStabilityId = (await _unitOfWork.SharedProcessStabilities.GetByNameAsync(row.ProcessStabilityId))?.Id;
                    idea.DocumentationPresentId = (await _unitOfWork.SharedDocumentationPresents.GetByNameAsync(row.DocumentationPresentId))?.Id;
                    idea.AutomationGoalId = (await _unitOfWork.SharedAutomationGoals.GetByNameAsync(row.AutomationGoalId))?.Id; ///// spelling mistake
                    idea.ApplicationStabilityId = (await _unitOfWork.SharedApplicationStabilities.GetByNameAsync(row.ApplicationStabilityId))?.Id;
                    idea.TaskFrequencyId = (await _unitOfWork.SharedTaskFrequencies.GetByNameAsync(row.TaskFrequencyId))?.Id;
                    idea.AverageReviewTimeComment = row.AverageReviewTimeComment;
                    idea.ProcessPeakId = (await _unitOfWork.SharedProcessPeaks.GetByNameAsync(row.ProcessPeakId))?.Id;
                    idea.AverageNumberOfStepId = (await _unitOfWork.SharedAverageNumberOfSteps.GetByNameAsync(row.AverageNumberOfStepId))?.Id;
                    idea.NumberOfWaysToCompleteProcessId = (await _unitOfWork.SharedNumberOfWaysToCompleteProcesses.GetByNameAsync(row.NumberOfWaysToCompleteProcessId))?.Id;
                    idea.DataInputPercentOfStructuredId = (await _unitOfWork.SharedDataInputPercentOfStructureds.GetByNameAsync(row.DataInputPercentOfStructuredId))?.Id;
                    idea.DecisionCountId = (await _unitOfWork.SharedDecisionCounts.GetByNameAsync(row.DecisionCountId))?.Id;
                    idea.DecisionDifficultyId = (await _unitOfWork.SharedDecisionDifficulties.GetByNameAsync(row.DecisionDifficultyId))?.Id;
                    idea.AverageWorkingDay = row.AverageWorkingDay;
                    idea.AverageEmployeeFullCost = row.AverageEmployeeFullCost;
                    idea.ActivityVolumeAverage = row.ActivityVolumeAverage;
                    idea.EmployeeCount = row.EmployeeCount;
                    idea.AverageErrorRate = row.AverageErrorRate;
                    idea.WorkingHour = row.WorkingHour;
                    idea.AverageProcessingTime = row.AverageProcessingTime;
                    idea.AverageReworkTime = row.AverageReworkTime;
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
					idea.ClientId = tenant.Id;
					idea.ImportStage = v.ImportStage;
					idea.ImportStatus = v.ImportStatus;
					cores.Add(idea);

                });
                var ideas = await _unitOfWork.ImportBulkIdeas(cores);
                await _unitOfWork.CompleteAsync();

				var successCount = rows.Count.Equals(ideas.Count) ? rows.Count : 0;
				var failedCount = rows.Count - ideas.Count;

                ImportStatus.Add($"{tenant}SuccessCount", $"{successCount}");
                ImportStatus.Add($"{tenant}FailedCount", $"{failedCount}");

                await SaveIdeasWorkflows(ideas);
                ImportStatus.Add($"{tenant}Completed", "1");
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
				var firstStage = Data.Core.Enumerators.Stage.n01_Assess;

				var date = DateTime.Now;
				var ideaStage = new Data.Core.Domain.Business.IdeaStage
				{
					Idea = idea,
					StageId = firstStage.ToString(),
					DateStartEstimate = date,
					DateStart = date,
					IsInWorkFlow = false,
				};

				await _unitOfWork.AddAsync(ideaStage);
				await _unitOfWork.CompleteAsync();


				var ideaStageStatus = new Data.Core.Domain.Business.IdeaStageStatus
				{
					IdeaStageId = ideaStage.Id,
					StatusId = Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview.ToString(),
					Date = date
				};
				await _unitOfWork.AddAsync(ideaStageStatus);
				await _unitOfWork.CompleteAsync();

				var stages = (await _unitOfWork.SharedStages.FindAsync(x => x.Id != firstStage.ToString())).ToArray();
				if (firstStage == Data.Core.Enumerators.Stage.n01_Assess)
					stages = stages.Where(x => x.Id != Data.Core.Enumerators.Stage.n00_Idea.ToString()).ToArray();

				var now = DateTime.Now;
				foreach (var stage in stages)
				{
					ideaStage = new Data.Core.Domain.Business.IdeaStage
					{
						Idea = idea,
						DateStartEstimate = now,
						Stage = stage
					};

					now = now.AddSeconds(1);
					await _unitOfWork.AddAsync(ideaStage);
				}
				await _unitOfWork.CompleteAsync();
			}
		}


		public async Task<(bool success, string message, List<COEBulkIdeaModel> rows)> UploadCeoExcelFile(IFormFile file)
		{
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
								DepartmentId = dataSet.Tables[0].Rows[i].ItemArray[6] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[6]) : string.Empty,
								TeamId = dataSet.Tables[0].Rows[i].ItemArray[7] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[7]) : string.Empty,
								ProcessId = dataSet.Tables[0].Rows[i].ItemArray[8] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[8]) : string.Empty,
								Stage = dataSet.Tables[0].Rows[i].ItemArray[9] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[9]) : string.Empty,
								Status = dataSet.Tables[0].Rows[i].ItemArray[10] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[10]) : string.Empty,
								DeployeementDate = DateTime.TryParse(Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[11]), out DateTime deployeementDate) ? deployeementDate : DateTime.MinValue,
								RuleId = dataSet.Tables[0].Rows[i].ItemArray[12] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[12]) : string.Empty,
								InputId = dataSet.Tables[0].Rows[i].ItemArray[13] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[13]) : string.Empty,
								InputDataStructureId = dataSet.Tables[0].Rows[i].ItemArray[14] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[14]) : string.Empty,
                                ProcessStabilityId = dataSet.Tables[0].Rows[i].ItemArray[15] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[15]) : string.Empty,
                                DocumentationPresentId = dataSet.Tables[0].Rows[i].ItemArray[16] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[16]) : string.Empty,
								AutomationGoalId = dataSet.Tables[0].Rows[i].ItemArray[17] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[17]) : string.Empty,
								ApplicationStabilityId = dataSet.Tables[0].Rows[i].ItemArray[18] != null  ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[18]) : string.Empty,
                                AverageWorkingDay = int.TryParse(dataSet.Tables[0].Rows[i].ItemArray[19]?.ToString(), out int parsedValue) ? parsedValue : 0,
                                WorkingHour = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[20]?.ToString(), out decimal workingHourResult) ? workingHourResult : 0.0m,
                                AverageEmployeeFullCost = int.TryParse(dataSet.Tables[0].Rows[i].ItemArray[21]?.ToString(), out var AverageEmployeeFullCostresult) ? AverageEmployeeFullCostresult : 0,
                                EmployeeCount = int.TryParse(dataSet.Tables[0].Rows[i].ItemArray[22]?.ToString(), out var EmployeeCountresult) ? EmployeeCountresult : 0,
                                TaskFrequencyId = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[23]) ?? string.Empty,
                                ActivityVolumeAverage = dataSet.Tables[0].Rows[i].ItemArray[24] != null ? int.TryParse(dataSet.Tables[0].Rows[i].ItemArray[24].ToString(), out var activityVolumeResult) ? activityVolumeResult : 0 : 0,
                                AverageProcessingTime = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[25]?.ToString(), out decimal avgProcessingTimeResult) ? avgProcessingTimeResult : 0.0m,
                                AverageErrorRate = int.TryParse(dataSet.Tables[0].Rows[i].ItemArray[26]?.ToString(), out int averageErrorRateResult) ? averageErrorRateResult : 0,
                                AverageReworkTime = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[27]?.ToString(), out decimal averageReworkTimeResult) ? averageReworkTimeResult : 0.0m,
                                AverageWorkToBeReviewed = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[28]?.ToString(), out decimal averageWorkToBeReviewedResult) ? averageWorkToBeReviewedResult : 0.0m,
								AverageReviewTimeComment = dataSet.Tables[0].Rows[i].ItemArray[29]?.ToString() ?? string.Empty,
								ProcessPeakId = dataSet.Tables[0].Rows[i].ItemArray[30]?.ToString() ?? string.Empty,
								AverageNumberOfStepId = dataSet.Tables[0].Rows[i].ItemArray[31]?.ToString() ?? string.Empty,
								NumberOfWaysToCompleteProcessId = dataSet.Tables[0].Rows[i].ItemArray[32]?.ToString() ?? string.Empty,
								DataInputPercentOfStructuredId = dataSet.Tables[0].Rows[i].ItemArray[33]?.ToString() ?? string.Empty,
								DecisionCountId = dataSet.Tables[0].Rows[i].ItemArray[34]?.ToString() ?? string.Empty,
                                DecisionDifficultyId = dataSet.Tables[0].Rows[i].ItemArray[35]?.ToString() ?? string.Empty,
                                PotentialFineAmount = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[36]?.ToString(), out decimal potentialFineAmountResult) ? potentialFineAmountResult : 0.0m,
                                PotentialFineProbability = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[37]?.ToString(), out decimal potentialFineProbabilityResult) ? potentialFineProbabilityResult : 0.0m,
                                IsHighRisk = dataSet.Tables[0].Rows[i].ItemArray[38]?.ToString() ?? string.Empty,
                                IsDataSensitive = dataSet.Tables[0].Rows[i].ItemArray[39]?.ToString() ?? string.Empty,
                                IsAlternative = dataSet.Tables[0].Rows[i].ItemArray[40]?.ToString() ?? string.Empty,
                                IsHostUpgrade = dataSet.Tables[0].Rows[i].ItemArray[41]?.ToString() ?? string.Empty,
                                IsDataInputScanned = dataSet.Tables[0].Rows[i].ItemArray[42]?.ToString() ?? string.Empty,
                            };

							rows.Add(fileReader);
						}
						
						if (rows.Any(x => String.IsNullOrEmpty(x.Name)))
                            return (false, "Some Idea(s) contain invalid or empty name.", rows);
						
						if (rows.Any(x => x.Name.Length > 100))
                            return (false, "Some Idea(s) exceeds name limit, it should be 100 characters max.", rows);

                        if (rows.Any(x => String.IsNullOrEmpty(x.Description)))
                            return (false, "Some Idea(s) contain invalid or empty description.", rows);

                        if (rows.Any(x => x.Description.Length > 100))
                            return (false, "Some Idea(s) exceeds description limit, it should be 750 characters max.", rows);

						if (_unitOfWork.CheckIdeasWithExistingName(rows.Select(x => x.Name).ToList()))
							return (false, "Some Idea(s) contains duplicate Name.", rows);

                        return (true, "Upload successful.", rows);

					}
					else
					{
						return (false, "Incorrect format detected. Please download our template, add your ideas, and try again.", new List<COEBulkIdeaModel>());
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
