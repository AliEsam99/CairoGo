using CairoGo.DTOs.PlaceVibeDTO;
using CairoGo.Models.Entity;

namespace CairoGo.Mappings
{
    public static class PlaceVibeTagMapper
    {
        public static PlaceVibeTag ToEntity(CreatePlaceVibeTagDto dto)
        {
            return new PlaceVibeTag
            {
                PlaceId = dto.PlaceId,
                Value = dto.Value,
            };
        }

        public static PlaceVibeTagResponseDto ToResponse(PlaceVibeTag entity)
        {
            return new PlaceVibeTagResponseDto
            {
                PlaceVibeTagId = entity.PlaceVibeTagId,
                PlaceId = entity.PlaceId,
                Value = entity.Value
            };
        }

        public static void ApplyUpdate(PlaceVibeTag entity, UpdatePlaceVibeTagDto dto)
        {
            if (dto.Value != null)
                entity.Value = dto.Value.Value;
        }
    }
}
