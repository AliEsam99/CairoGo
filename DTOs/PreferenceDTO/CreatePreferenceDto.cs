using CairoGo.Models.ENums;
using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.PreferenceDTO
{
    public class CreatePreferenceDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Travel vibe is required")]
        public TravelVibe TravelVibe { get; set; }

        [Required(ErrorMessage = "Budget is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Budget must be greater than 0")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Weather preference is required")]
        public WeatherPref WeatherPref { get; set; }

        [Required(ErrorMessage = "Trip days is required")]
        [Range(1, 4, ErrorMessage = "Trip days must be between 1 and 4")]
        public int TripDays { get; set; }
        [Required(ErrorMessage = "At least one activity type is required")]
        [MinLength(1, ErrorMessage = "Select at least one activity")]
        public List<Guid> ActivityTypeIds { get; set; } = new();
    }
}
