using CairoGo.DTOs.PreferenceSignalDTO;
using CairoGo.Models.Entity;

namespace CairoGo.Mappings
{
    public static class UserPreferenceSignalMapper
    {
        public static UserPreferenceSignal ToEntity(this CreateUserPreferenceSignalDto dto)
        {
            return new UserPreferenceSignal
            {
                SignalId = Guid.NewGuid(),
                UserId = dto.UserId,
                SignalType = dto.SignalType,
                Key = dto.Key,
                Weight = dto.Weight,
                ObservationCount = 0,
                LastUpdated = DateTime.UtcNow
            };

        }
        public static UserPreferenceSignalDto ToDto(this UserPreferenceSignal entity)
        {
            return new UserPreferenceSignalDto
            {
                SignalId = entity.SignalId,
                UserId = entity.UserId,
                SignalType = entity.SignalType,
                Key = entity.Key,
                Weight = entity.Weight,
                ObservationCount = entity.ObservationCount,
                LastUpdated = entity.LastUpdated
            };
        }
        public static void ApplyUpdate(this UserPreferenceSignal entity, UpdateUserPreferenceSignalDto dto)
        {
            if (dto.Weight.HasValue) entity.Weight = dto.Weight.Value;
            if (dto.ObservationCount.HasValue) entity.ObservationCount = dto.ObservationCount.Value;
            entity.LastUpdated = DateTime.UtcNow;
        }
    }
}
