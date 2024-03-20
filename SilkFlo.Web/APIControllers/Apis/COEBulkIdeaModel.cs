using System;

namespace SilkFlo.Web.APIControllers.Apis
{
	public class COEBulkIdeaModel
	{
		public string? Name { get; set; }
		public string? CreatedDate { get; set; }
		public string? SubmitterEmailAddress { get; set; }
		public string? ProcessOwnerEmailAddress { get; set; }
		public string? AutomationId { get; set; }
		public string? Description { get; set; } 
		public string? Department { get; set; }  
		public string? Team { get; set; }  
		public string? Process { get; set; } 
		public string? Stage { get; set; } 
		public string? Status { get; set; }  
		public DateTime? DeployementDate { get; set; } 
		public string? Rule { get; set; }
		public string? Input { get; set; }
		public string? InputDataStructure { get; set; }
		public string? ProcessStability { get; set; }
		public string? DocumentationPresent { get; set; }
		public string? AutomationGoal { get; set; }
		public string? ApplicationStability { get; set; }
		public string? TaskFrequency { get; set; }
		public decimal? AverageReviewTime { get; set; }
		public string? ProcessPeak { get; set; }
		public string? AverageNumberOfStep { get; set; }
		public string? NumberOfWaysToCompleteProcess { get; set; }
		public string? DataInputPercentOfStructured { get; set; }
		public string? DecisionCount { get; set; }
		public string? DecisionDifficulty { get; set; }
		public int? AverageWorkingDay { get; set; }
		public int? AverageEmployeeFullCost { get; set; }
		public int? ActivityVolumeAverage { get; set; }
		public int? EmployeeCount { get; set; }
		public int? AverageErrorRate { get; set; }
		public decimal? WorkingHour { get; set; }
		public decimal? AverageProcessingTime { get; set; }
		public decimal? AverageReworkTime { get; set; }
		public decimal? AverageWorkToBeReviewed { get; set; }
		public decimal? PotentialFineAmount { get; set; }
		public decimal? PotentialFineProbability { get; set; }
		public string? IsHighRisk { get; set; }
		public string? IsDataSensitive { get; set; }
		public string? IsAlternative { get; set; }
		public string? IsHostUpgrade { get; set; }
		public string? IsDataInputScanned { get; set; }
	}
}
