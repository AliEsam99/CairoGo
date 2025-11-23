    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace CairoGo.Models.Entity
    {
        public class UserApplication : IdentityUser<Guid>
        {
            // Basic Info
            [Required(ErrorMessage = "Name is required")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Age is required")]
            [Range(13, 120, ErrorMessage = "Age must be between 13 and 120")]
            public int Age { get; set; }
            public DateTime JoinDate { get; set; } = DateTime.UtcNow;

            // Profile Fields
            [Url(ErrorMessage = "Invalid URL format")]
            [StringLength(500)]
            public string? ProfilePictureUrl { get; set; }


            [Required(ErrorMessage = "Address is required")]
            [StringLength(200, MinimumLength = 5)]
            public string Address { get; set; }

            [StringLength(100)]
            public string? HomeCity { get; set; } = "Cairo";

            [StringLength(10)]
            [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency must be 3 uppercase letters (e.g., EGP, USD)")]
            public string? PreferredCurrency { get; set; } = "EGP";

            // Settings
            //public bool DarkModeEnabled { get; set; } = false;
            //[MaxLength(50)]
            //public string? StartupOption { get; set; } = "home"; // "home", "last-trip", "recommendations"

            //[MaxLength(50)]
            //public string DefaultHomeView { get; set; } = "current-trips"; // "current-trips", "recommendations", "favorites
                                                                       
        
            // Navigation Properties
            public PreferenceProfile? PreferenceProfile { get; set; }
            public ICollection<TripPlan> TripPlans { get; set; } = new List<TripPlan>();
            public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
            public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();
            public ICollection<Review> Reviews { get; set; } = new List<Review>();
            public ICollection<RecommendationLog> RecommendationLogs { get; set; } = new List<RecommendationLog>();
            public ICollection<SearchSession> SearchSessions { get; set; } = new List<SearchSession>();
            public ICollection<ExperimentAssignment> ExperimentAssignments { get; set; } = new List<ExperimentAssignment>();
            public ICollection<UserPreferenceSignal> PreferenceSignals { get; set; } = new List<UserPreferenceSignal>();
        }
    }
