using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.ExperimentAssignmentDTO
{
    public class ExperimentAssignmentCreateDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string ExperimentName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string VariantName { get; set; } = string.Empty;
    }
}
