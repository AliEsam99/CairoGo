using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(UserId), nameof(GeneratedAt))]
    [Index(nameof(ModelVersion))]
    public class RecommendationLog
    {
        [Key]
        public Guid RecommendationLogId { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        [ForeignKey(nameof(TripPlan))]
        public Guid? TripPlanId { get; set; }

        [Required(ErrorMessage = "Model version is required")]
        [StringLength(100)]
        public string ModelVersion { get; set; } = default!;
        public float Score { get; set; }
        public string? ScoreBreakdownJson { get; set; }

        public bool WasShown { get; set; } = false;
        public bool WasClicked { get; set; } = false;
        public bool WasBooked { get; set; } = false;

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public UserApplication User { get; set; } = default!;
        public Place Place { get; set; } = default!;
        public TripPlan? TripPlan { get; set; }
    }
}
