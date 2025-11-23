using CairoGo.Models.ENums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    public class PreferenceProfile
    {
        [Key]
        public Guid ProfileId { get; set; } = Guid.NewGuid();
        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        // Preference Fields
        [Required(ErrorMessage = "Travel vibe is required")]
        public TravelVibe TravelVibe { get; set; }

        [Required(ErrorMessage = "Budget is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Budget must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Weather preference is required")]
        public WeatherPref WeatherPref { get; set; }
        [Required(ErrorMessage = "Trip days is required")]
        [Range(1, 30, ErrorMessage = "Trip days must be between 1 and 30")]
        public int TripDays { get; set; }
        public DateTime LastQuizTakenAt { get; set; }

        // Navigation Properties
        public UserApplication User { get; set; } = default!;

        // M:N with ActivityType (EF Core will create join table automatically) --> relation
        public ICollection<ActivityType> ActivityTypes { get; set; } = new List<ActivityType>();
    }
}
