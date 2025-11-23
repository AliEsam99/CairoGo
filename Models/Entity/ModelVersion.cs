using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(Name), IsUnique = true)]
    public class ModelVersion
    {
        [Key]
        public Guid ModelVersionId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Model name is required")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Model type is required")]
        [StringLength(100)]
        public string Type { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? ConfigJson { get; set; }


        public DateTime DeployedAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeprecatedAt { get; set; }

        public bool IsActive { get; set; } = false;

        // Metrics
        public float? AverageCTR { get; set; }
        public float? AverageConversion { get; set; }
        public int PredictionCount { get; set; } = 0;

        // Navigation Properties
        public ICollection<RecommendationLog> RecommendationLogs { get; set; } = new List<RecommendationLog>();
    }
}
