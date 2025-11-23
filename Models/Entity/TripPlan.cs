using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(UserId), nameof(CreatedAt))]
    public class TripPlan
    {
        [Key]
        public Guid TripPlanId { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(PreferenceProfile))]
        public Guid? PreferenceProfileId { get; set; }

        [Required(ErrorMessage = "Trip title is required")]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Trip days is required")]
        [Range(1, 30, ErrorMessage = "Trip days must be between 1 and 30")]
        public int TripDays { get; set; }

        // Cost & Scoring

        public decimal? EstimatedCostPerPersonPerDay { get; set; }
        public decimal? AlignmentScore { get; set; }
        public string? ScoreBreakdownJson { get; set; }

        // Alternative Plans
        [ForeignKey(nameof(ParentPlan))]
        public Guid? ParentPlanId { get; set; }
        public int VariationNumber { get; set; } = 1;

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public UserApplication User { get; set; } = default!;
        public PreferenceProfile? PreferenceProfile { get; set; }

        public TripPlan? ParentPlan { get; set; }

        [InverseProperty(nameof(ParentPlan))]
        public ICollection<TripPlan> AlternativePlans { get; set; } = new List<TripPlan>();

        public ICollection<TripDay> TripDaysCollection { get; set; } = new List<TripDay>();
        public ICollection<RecommendationLog> RecommendationLogs { get; set; } = new List<RecommendationLog>();
    }
}
