﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SilkFlo.Web.APIControllers.Apis
{
    public class EmployeeBulkIdeaModel
    {
        [StringLength(100,
            MinimumLength = 1,
            ErrorMessage = "Name cannot be greater than 100 characters in length.")]
        [Required]
        public string Name { get; set; }

        [StringLength(750,
            MinimumLength = 0,
            ErrorMessage = "Summary cannot be greater than 750 characters in length.")]
        [Required]
        public string Summary { get; set; }

        [Required]
        public string DepartmentId { get; set; }
        public string TeamId { get; set; }
        public string ProcessId { get; set; }
        public string RuleId { get; set; }
        public string InputId { get; set; }
        public string InputDataStructureId { get; set; }
        public string ProcessStabilityId { get; set; }
        public string DocumentationPresentId { get; set; }
        public string ProcessOwnerId { get; set; }
        public int Rating { get; set; }
        public string ClientId { get; set; }
        public string UserId { get; set; }
        public bool Ispractise { get; set; }
        public List<Models.Business.Collaborator> Collaborators { get; set; } = new();
    }
}
