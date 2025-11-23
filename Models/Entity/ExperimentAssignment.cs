using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(UserId), nameof(ExperimentName), IsUnique = true)]
    public class ExperimentAssignment
    {
        [Key]
        public Guid AssignmentId { get; set; } = Guid.NewGuid();
        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Experiment name is required")]
        [StringLength(200)]
        public string ExperimentName { get; set; }

        [Required(ErrorMessage = "Variant name is required")]
        [StringLength(100)]
        public string VariantName { get; set; }


        public DateTime AssignedAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CompletedAt { get; set; }

        // Outcomes
     
        public int? TripPlansGenerated { get; set; }

        public int? PlacesClicked { get; set; }

        public bool? ConversionSuccess { get; set; }

        // Navigation Properties
        public UserApplication User { get; set; } = default!;
    }
}
