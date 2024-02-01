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

namespace SilkFlo.Web.Controllers
{
    public class DataImportController : AbstractAPI
    {
        private readonly IConfiguration _configuration;
        public DataImportController(IUnitOfWork unitOfWork, ViewToString viewToString, IAuthorizationService authorization, IConfiguration configuration)
            : base(unitOfWork, viewToString, authorization)
        {
            _configuration = configuration;
        }

		[HttpPost("/Data/Import")]
		public async Task<IActionResult> ImportByFile([FromForm] IFormFile File)
		{
			try
			{
				List<COEBulkIdeaModel> rows = null;
				if (File.FileName.ToLower().Contains(".xlsx") || File.FileName.ToLower().Contains(".xls"))
				{
					rows = await UploadCeoExcelFile(File);
				}
				else
				{
					return Ok(new { status = false, message = "Invalid Data!", });
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
				var feedback = new Feedback();
				var tenant = await GetClientAsync();

				//validation
				if (tableData is null)
				{
					feedback.DangerMessage("Some error occurred during Import.");
					return BadRequest(feedback);
				}

				//Move all the permission clauses here
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

				var rows = tableData.ToObject<List<COEBulkIdeaModel>>();
				List<Models.Business.Idea> idealist = new List<Models.Business.Idea>();
				rowcount = rows.Count;

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

				//Write BulkUpload Functionality for DB

				foreach (var model in idealist)
				{
					model.ClientId = tenant.Id;
					// Validate
					if (string.IsNullOrWhiteSpace(model.Name))
						feedback.Add("Name", "The name of your idea is missing");
					else if (model.Name.Length > 100)
						feedback.Add("Name", "Name must be between 1 and 100 in length");


					if (string.IsNullOrWhiteSpace(model.Summary))
						feedback.Add("Summary", "Summary is missing");
					else if (model.Summary.Length > 750)
						feedback.Add("Summary", "Name must be between 1 and 750 in length");

					//TODO: Save Idea as a draft if any of the required field is not given in the file

					// Is NOT valid?
					if (!feedback.IsValid)
					{
						failedideacount++;
						continue;
					}
					
					var core = model.GetCore();

					if (!core.IsDraft)
					{
						await _unitOfWork.AddAsync(core);
						await _unitOfWork.CompleteAsync();
					}
					else
					{
						await _unitOfWork.AddAsync(core);
						await _unitOfWork.CompleteAsync();
					}
				}

				return Ok(new { status = true, message = "Ceo Ideas Added successfully.", SuccessCount = rowcount - failedideacount, FailedCount = failedideacount });
			}
			catch (Exception ex)
			{
				return Ok(new { status = false, message = "Some error occurred during Import.", SuccessCount = rowcount - failedideacount, FailedCount = failedideacount });
			}
		}


		public async Task<List<COEBulkIdeaModel>> UploadCeoExcelFile(IFormFile file)
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
						ApplicationStabilityId = dataSet.Tables[0].Rows[i].ItemArray[18] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[18]) : string.Empty,
						AverageWorkingDay = dataSet.Tables[0].Rows[i].ItemArray[19] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[19].ToString()) : 0,
						WorkingHour = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[20]?.ToString(), out decimal workingHourResult) ? workingHourResult : 0.0m,
						AverageEmployeeFullCost = dataSet.Tables[0].Rows[i].ItemArray[21] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[21].ToString()) : 0,
						EmployeeCount = dataSet.Tables[0].Rows[i].ItemArray[22] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[22].ToString()) : 0,
						TaskFrequencyId = dataSet.Tables[0].Rows[i].ItemArray[23] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[23]) : string.Empty,
						ActivityVolumeAverage = dataSet.Tables[0].Rows[i].ItemArray[24] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[24].ToString()) : 0,
						AverageProcessingTime = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[25]?.ToString(), out decimal averageProcessingTimeResult) ? averageProcessingTimeResult : 0.0m,
						AverageErrorRate = dataSet.Tables[0].Rows[i].ItemArray[26] != null ? int.Parse(dataSet.Tables[0].Rows[i].ItemArray[26].ToString()) : 0,
						AverageReworkTime = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[27]?.ToString(), out decimal averageReworkTimeResult) ? averageReworkTimeResult : 0.0m,
						AverageWorkToBeReviewed = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[28]?.ToString(), out decimal averageWorkToBeReviewedResult) ? averageWorkToBeReviewedResult : 0.0m,
						AverageReviewTimeComment = dataSet.Tables[0].Rows[i].ItemArray[29] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[29]) : string.Empty,
						ProcessPeakId = dataSet.Tables[0].Rows[i].ItemArray[30] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[30]) : string.Empty,
						AverageNumberOfStepId = dataSet.Tables[0].Rows[i].ItemArray[31] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[31]) : string.Empty,
						NumberOfWaysToCompleteProcessId = dataSet.Tables[0].Rows[i].ItemArray[32] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[32]) : string.Empty,
						DataInputPercentOfStructuredId = dataSet.Tables[0].Rows[i].ItemArray[33] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[33]) : string.Empty,
						DecisionCountId = dataSet.Tables[0].Rows[i].ItemArray[34] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[34]) : string.Empty,
						DecisionDifficultyId = dataSet.Tables[0].Rows[i].ItemArray[35] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[35]) : string.Empty,
						PotentialFineAmount = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[36]?.ToString(), out decimal potentialFineAmountResult) ? potentialFineAmountResult : 0.0m,
						PotentialFineProbability = decimal.TryParse(dataSet.Tables[0].Rows[i].ItemArray[37]?.ToString(), out decimal potentialFineProbabilityResult) ? potentialFineProbabilityResult : 0.0m,
						IsHighRisk = dataSet.Tables[0].Rows[i].ItemArray[38] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[38]) : string.Empty,
						IsDataSensitive = dataSet.Tables[0].Rows[i].ItemArray[39] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[39]) : string.Empty,
						IsAlternative = dataSet.Tables[0].Rows[i].ItemArray[40] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[40]) : string.Empty,
						IsHostUpgrade = dataSet.Tables[0].Rows[i].ItemArray[41] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[41]) : string.Empty,
						IsDataInputScanned = dataSet.Tables[0].Rows[i].ItemArray[42] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[42]) : string.Empty,
					};

					rows.Add(fileReader);
					}
				}
				return rows;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
