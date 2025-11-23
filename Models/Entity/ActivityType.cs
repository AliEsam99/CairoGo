using CairoGo.Models.ENums;
using System.ComponentModel.DataAnnotations;

namespace CairoGo.Models.Entity
{
    public class ActivityType
    {
        [Key]
        public Guid ActivityTypeId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Activity name is required")]
        public ActivityKind Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        // Navigation Properties
        public ICollection<Place> Places { get; set; } = new List<Place>();
        public ICollection<PreferenceProfile> PreferenceProfiles { get; set; } = new List<PreferenceProfile>();
    }
}
