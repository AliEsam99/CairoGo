using CairoGo.DTOs.TripSlotDTO;
using CairoGo.Models.Entity;

namespace CairoGo.Mappings
{
    public static class TripSlotMapper
    {
        public static TripSlot ToEntity(this TripSlotCreateDto dto)
        {
            return new TripSlot
            {
                SlotType = dto.SlotType,
                PlaceId = dto.PlaceId,
                AlternatePlaceId = dto.AlternatePlaceId,
                Notes = dto.Notes,
                PerPersonCost = dto.PerPersonCost
            };
        }
        public static void UpdateEntity(this TripSlot entity, TripSlotUpdateDto dto)
        {
            entity.SlotType = dto.SlotType;
            entity.PlaceId = dto.PlaceId;
            entity.AlternatePlaceId = dto.AlternatePlaceId;
            entity.Notes = dto.Notes;
            entity.PerPersonCost = dto.PerPersonCost;
        }
        public static TripSlotUpdateDto ToDto(this TripSlot entity)
        {
            return new TripSlotUpdateDto
            {
                SlotType = entity.SlotType,
                PlaceId = entity.PlaceId,
                AlternatePlaceId = entity.AlternatePlaceId,
                Notes = entity.Notes,
                PerPersonCost = entity.PerPersonCost
            };
        }
    }
}
