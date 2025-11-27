using CairoGo.Models.ENums;

namespace CairoGo.DTOs.PreferenceDTO
{
    public class PreferenceResponseDto
    {
        public Guid ProfileId { get; set; }
        public Guid UserId { get; set; }

        public TravelVibe TravelVibe { get; set; }
        public decimal Budget { get; set; }
        public WeatherPref WeatherPref { get; set; }
        public int TripDays { get; set; }

        public DateTime LastQuizTakenAt { get; set; }

        public List<ActivityTypeDto> Activities { get; set; } = new();
    }
    public class ActivityTypeDto
    {
        public Guid ActivityTypeId { get; set; }
        public string Name { get; set; }
    }
}

