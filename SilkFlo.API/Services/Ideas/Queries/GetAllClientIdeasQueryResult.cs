using PetaPoco;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Services.Ideas.Queries
{
	public class GetAllClientIdeasQueryResult
	{
		public bool IsSucceed { get; set; }
		public bool Message { get; set; }
		public List<IdeaResult> Result { get; set; }
	}

	[TableName("Ideas")]
	public class IdeaResult
	{
		public string Id { get; set; }
		public double? ActivityVolumeAverage { get; set; }
		public string ActivityVolumeAverageComment { get; set; }
		public double? AhtRobot { get; set; }
		public string ApplicationStabilityComment { get; set; }
		public string ApplicationStabilityString { get; set; }
		public string ApplicationStabilityId { get; set; }
		public string AutomationGoalString { get; set; }
		public string AutomationGoalId { get; set; }
		public double? AverageEmployeeFullCost { get; set; }
		public string AverageEmployeeFullCostComment { get; set; }
		public double? AverageErrorRate { get; set; }
		public string AverageErrorRateComment { get; set; }
		public string AverageNumberOfStepComment { get; set; }
		public string AverageNumberOfStepString { get; set; }
		public string AverageNumberOfStepId { get; set; }
		public double? AverageProcessingTime { get; set; }
		public string AverageProcessingTimeComment { get; set; }
		public double? AverageReviewTime { get; set; }
		public string AverageReviewTimeComment { get; set; }
		public double? AverageReworkTime { get; set; }
		public string AverageReworkTimeComment { get; set; }
		public double? AverageWorkingDay { get; set; }
		public string AverageWorkingDayComment { get; set; }
		public double? AverageWorkToBeReviewed { get; set; }
		public string AverageWorkToBeReviewedComment { get; set; }
		public string BenefitActual { get; set; }
		public string BenefitExpected { get; set; }
		public string ChallengeActual { get; set; }
		public string ChallengeExpected { get; set; }
		public string ClientString { get; set; }
		public string ClientId { get; set; }
		public string DataInputPercentOfStructuredComment { get; set; }
		public string DataInputPercentOfStructuredString { get; set; }
		public string DataInputPercentOfStructuredId { get; set; }
		public string DataInputScannedComment { get; set; }
		public string DecisionCountComment { get; set; }
		public string DecisionCountString { get; set; }
		public string DecisionCountId { get; set; }
		public string DecisionDifficultyComment { get; set; }
		public string DecisionDifficultyString { get; set; }
		public string DecisionDifficultyId { get; set; }
		public string DepartmentString { get; set; }
		public string DepartmentId { get; set; }
		public string DocumentationPresentComment { get; set; }
		public string DocumentationPresentString { get; set; }
		public string DocumentationPresentId { get; set; }
		public string EaseOfImplementationFinal { get; set; }
		public int? EmployeeCount { get; set; }
		public string EmployeeCountComment { get; set; }
		public string InputComment { get; set; }
		public string InputDataStructureString { get; set; }
		public string InputDataStructureId { get; set; }
		public string InputString { get; set; }
		public string InputId { get; set; }
		public bool IsAlternative { get; set; }
		public bool IsDataInputScanned { get; set; }
		public bool IsDataSensitive { get; set; }
		public bool IsDraft { get; set; }
		public bool IsHighRisk { get; set; }
		public bool IsHostUpgrade { get; set; }
		public string LessenLearnt { get; set; }
		public string Name { get; set; }
		public string NegativeImpactComment { get; set; }
		public string NumberOfWaysToCompleteProcessComment { get; set; }
		public string NumberOfWaysToCompleteProcessString { get; set; }
		public string NumberOfWaysToCompleteProcessId { get; set; }
		public string PainPointComment { get; set; }
		public decimal? PotentialFineAmount { get; set; }
		public decimal? PotentialFineProbability { get; set; }
		public string ProcessString { get; set; }
		public string ProcessId { get; set; }
		public string ProcessOwnerString { get; set; }
		public string ProcessOwnerId { get; set; }
		public string ProcessPeakComment { get; set; }
		public string ProcessPeakString { get; set; }
		public string ProcessPeakId { get; set; }
		public string ProcessStabilityComment { get; set; }
		public string ProcessStabilityString { get; set; }
		public string ProcessStabilityId { get; set; }
		public int? ProcessVolumetryPerMonth { get; set; }
		public int? ProcessVolumetryPerYear { get; set; }
		public int Rating { get; set; }
		public string RatingComment { get; set; }
		public double? RobotSpeedMultiplier { get; set; }
		public int? RobotWorkDayYear { get; set; }
		public int? RobotWorkHourDay { get; set; }
		public string RuleComment { get; set; }
		public string RuleString { get; set; }
		public string RuleId { get; set; }
		public string RunningCostString { get; set; }
		public string RunningCostId { get; set; }
		public string StructureComment { get; set; }
		public string SubmissionPathString { get; set; }
		public string SubmissionPathId { get; set; }
		public string SubTitle { get; set; }
		public string Summary { get; set; }
		public string TaskFrequencyComment { get; set; }
		public string TaskFrequencyString { get; set; }
		public string TaskFrequencyId { get; set; }
		public string TeamString { get; set; }
		public string TeamId { get; set; }
		public int? WorkingHour { get; set; }
		public string WorkingHourComment { get; set; }
		public double? WorkloadSplit { get; set; }
		public string CreatedById { get; set; }
		public string CreatedDate { get; set; }
		public string UpdatedById { get; set; }
		public string UpdatedDate { get; set; }
	}
}
