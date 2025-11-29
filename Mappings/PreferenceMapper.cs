using CairoGo.DTOs.PreferenceDTO;
using CairoGo.Models.Entity;

namespace CairoGo.Mappings
{
    public static class PreferenceMapper
    {
        // Map CreatePreferenceDto to PreferenceProfile entity
        public static PreferenceProfile ToEntity(CreatePreferenceDto dto)
        {
            return new PreferenceProfile
            {
                UserId = dto.UserId,
                TravelVibe = dto.TravelVibe,
                Budget = dto.Budget,
                WeatherPref = dto.WeatherPref,
                TripDays = dto.TripDays,
                //ActivityTypes = dto.ActivityTypeIds?.Select(id => new ActivityType { ActivityTypeId = id }).ToList() ?? new List<ActivityType>(),
                LastQuizTakenAt = DateTime.UtcNow
            };
        }
        // Map PreferenceProfile entity to PreferenceResponseDto
        public static PreferenceResponseDto ToResponse(PreferenceProfile profile)
        {
            return new PreferenceResponseDto
            {
                ProfileId = profile.ProfileId,
                UserId = profile.UserId,
                TravelVibe = profile.TravelVibe,
                Budget = profile.Budget,
                WeatherPref = profile.WeatherPref,
                TripDays = profile.TripDays,
                LastQuizTakenAt = profile.LastQuizTakenAt,
                Activities = profile.ActivityTypes?.Select(at => new ActivityTypeDto
                {
                    ActivityTypeId = at.ActivityTypeId,
                    Name = at.Name.ToString()
                }).ToList() ?? new List<ActivityTypeDto>()
            };
        }
        public static void UpdateEntity(PreferenceProfile profile, UpdatePreferenceDto dto)
        {
            if (dto.TravelVibe.HasValue)
                profile.TravelVibe = dto.TravelVibe.Value;

            if (dto.Budget.HasValue)
                profile.Budget = dto.Budget.Value;

            if (dto.WeatherPref.HasValue)
                profile.WeatherPref = dto.WeatherPref.Value;

            if (dto.TripDays.HasValue)
                profile.TripDays = dto.TripDays.Value;

            profile.LastQuizTakenAt = DateTime.UtcNow;
        }
    }
}
