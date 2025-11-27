using CairoGo.DTOs.TripDayDTO;
using CairoGo.Models.Entity;

namespace CairoGo.Mappings
{
    public static class TripDayMapper
    {
        public static TripDay ToEntity(this TripDayCreateDto dto)
        {
            return new TripDay
            {
                DayNumber = dto.DayNumber,
                Date = dto.Date,
                EstimatedDayCost = dto.EstimatedDayCost
            };
        }
        public static void UpdateEntity(this TripDay entity, TripDayUpdateDto dto)
        {
            entity.DayNumber = dto.DayNumber;
            entity.Date = dto.Date;
            entity.EstimatedDayCost = dto.EstimatedDayCost;
        }
        public static TripDayUpdateDto ToDto(this TripDay entity)
        {
            return new TripDayUpdateDto
            {
                DayNumber = entity.DayNumber,
                Date = entity.Date,
                EstimatedDayCost = entity.EstimatedDayCost
            };
        }
    }
}
