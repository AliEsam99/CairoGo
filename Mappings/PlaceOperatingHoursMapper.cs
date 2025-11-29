using CairoGo.DTOs.PlaceOperatingHoursDTO;
using CairoGo.Models.Entity;

namespace CairoGo.Mappings
{
    public static class PlaceOperatingHoursMapper
    {
        public static PlaceOperatingHours ToEntity(CreatePlaceOperatingHoursDto dto)
        {
            return new PlaceOperatingHours
            {
                PlaceId = dto.PlaceId,
                DayOfWeek = dto.DayOfWeek,
                OpenTime = dto.OpenTime,
                CloseTime = dto.CloseTime,
                IsClosed = dto.IsClosed
            };
        }
        public static PlaceOperatingHoursResponseDto ToResponse(PlaceOperatingHours entity)
        {
            return new PlaceOperatingHoursResponseDto
            {
                PlaceOperatingHoursId = entity.PlaceOperatingHoursId,
                PlaceId = entity.PlaceId,
                DayOfWeek = entity.DayOfWeek,
                OpenTime = entity.OpenTime,
                CloseTime = entity.CloseTime,
                IsClosed = entity.IsClosed
            };
        }
        public static void ApplyUpdate(PlaceOperatingHours entity,UpdatePlaceOperatingHoursDto dto)
        {
            entity.DayOfWeek = dto.DayOfWeek;

            entity.IsClosed = dto.IsClosed;
            if (dto.IsClosed)
            {
                entity.OpenTime = null;
                entity.CloseTime = null;
            }
            else
            {
                if (dto.OpenTime != null) entity.OpenTime = dto.OpenTime;
                if (dto.CloseTime != null) entity.CloseTime = dto.CloseTime;
            }

        }

    }
}
