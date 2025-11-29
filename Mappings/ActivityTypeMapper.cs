using CairoGo.DTOs.ActivityTypeDTO;
using CairoGo.Models.Entity;

namespace CairoGo.Mappings
{
    public static class ActivityTypeMapper
    {
        public static ActivityType ToEntity(this ActivityTypeCreateDto dto)
        {
            return new ActivityType
            {
                Name = dto.Name,
                Description = dto.Description,
            };
        }
        public static void UpdateEntity(this ActivityType entity, ActivityTypeUpdateDto dto)
        {
            entity.Name = dto.Name;
            entity.Description = dto.Description;
        }
        public static ActivityTypeUpdateDto ToDto(this ActivityType entity)
        {
            return new ActivityTypeUpdateDto
            {
                Name = entity.Name,
                Description = entity.Description,
            };
        }
    }
}
