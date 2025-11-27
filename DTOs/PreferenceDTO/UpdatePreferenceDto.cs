using CairoGo.Models.ENums;

namespace CairoGo.DTOs.PreferenceDTO
{
    public class UpdatePreferenceDto
    {
        public TravelVibe? TravelVibe { get; set; }

        public decimal? Budget { get; set; }

        public WeatherPref? WeatherPref { get; set; }

        public int? TripDays { get; set; }

        public List<Guid>? ActivityTypeIds { get; set; }
    }
}
