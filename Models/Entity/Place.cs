using CairoGo.Models.ENums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(Name))]
    [Index(nameof(District))]
    [Index(nameof(Category))]
    [Index(nameof(Latitude), nameof(Longitude))]
    public class Place
    {
        [Key]
        public Guid PlaceId { get; set; } = Guid.NewGuid();

        // Basic Info
        [Required(ErrorMessage = "Place name is required")]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, MinimumLength = 10)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Full description is required")]
        [StringLength(5000, MinimumLength = 50)]
        public string FullDescription { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Invalid image URL")]
        [StringLength(500)]
        public string ImageUrl { get; set; }

        // Location
        [Required(ErrorMessage = "District is required")]
        [StringLength(100)]
        public string District { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        // Classification

        [Required(ErrorMessage = "Category is required")]
        public PlaceCategory Category { get; set; }

        [Required(ErrorMessage = "Cost tier is required")]
        public CostTier CostTier { get; set; }

        [Required(ErrorMessage = "Indoor/Outdoor preference is required")]
        public WeatherPref IndoorOutdoor { get; set; }

        public SlotType? BestTimeOfDay { get; set; }

        public int? AvgVisitDurationMinutes { get; set; }

        //Ai must compute it
        public float? Rating { get; set; }

        // Contact & Details
        [Url(ErrorMessage = "Invalid website URL")]
        [StringLength(500)]
        public string? Website { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20)]
        [MinLength(8)]
        public string? PhoneNumber { get; set; }

        [StringLength(1000)]
        public string? AccessibilityInfo { get; set; }

        [StringLength(500)]
        public string? ParkingInfo { get; set; }

        [StringLength(200)]
        public string? BestTimeToVisit { get; set; }

        [Required]
        public int VisitorsPerYear { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<PlaceVibeTag> VibeTags { get; set; } = new List<PlaceVibeTag>();
        public ICollection<PlaceOperatingHours> OperatingHours { get; set; } = new List<PlaceOperatingHours>();
        public ICollection<TripSlot> TripSlots { get; set; } = new List<TripSlot>();

        [InverseProperty(nameof(TripSlot.AlternatePlace))]
        public ICollection<TripSlot> AlternateTripSlots { get; set; } = new List<TripSlot>();
        public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<RecommendationLog> RecommendationLogs { get; set; } = new List<RecommendationLog>();
        public ICollection<ActivityType> ActivityTypes { get; set; } = new List<ActivityType>();
        public ICollection<TrendingTag> TrendingTags { get; set; } = new List<TrendingTag>();
    }
}
