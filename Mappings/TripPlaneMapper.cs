using CairoGo.DTOs.TripPlaneDTO;
using CairoGo.Models.Entity;

namespace CairoGo.Mappings
{
    public static class TripPlaneMapper
    {
        // ---- من DTO لـ Entity ----
        public static TripPlan ToEntity(this TripPlanCreateDto dto)
        {
            return new TripPlan
            {
                UserId = dto.UserId,
                Title = dto.Title,
                StartDate = dto.StartDate ?? DateTime.UtcNow,
                TripDays = dto.TripDays,
                EstimatedCostPerPersonPerDay = dto.EstimatedCostPerPersonPerDay,
                AlignmentScore = dto.AlignmentScore,
                ScoreBreakdownJson = dto.ScoreBreakdownJson,
                ParentPlanId = dto.ParentPlanId,
                VariationNumber = dto.VariationNumber,
                PreferenceProfileId = dto.PreferenceProfileId
            };
        }
       // 
        public static void UpdateEntity(this TripPlanUpdateDto dto, TripPlan entity)
        {
            entity.Title = dto.Title;
            entity.StartDate = dto.StartDate ?? entity.StartDate;
            entity.TripDays = dto.TripDays;
            entity.EstimatedCostPerPersonPerDay = dto.EstimatedCostPerPersonPerDay;
            entity.AlignmentScore = dto.AlignmentScore;
            entity.ScoreBreakdownJson = dto.ScoreBreakdownJson;
            entity.ParentPlanId = dto.ParentPlanId;
            entity.VariationNumber = dto.VariationNumber;
            entity.PreferenceProfileId = dto.PreferenceProfileId;
            entity.UpdatedAt = DateTime.UtcNow; // تحديث التاريخ تلقائي
        }
        public static TripPlanCreateDto ToDto(this TripPlan entity)
        {
            return new TripPlanCreateDto
            {
                UserId = entity.UserId,
                Title = entity.Title,
                StartDate = entity.StartDate,
                TripDays = entity.TripDays,
                EstimatedCostPerPersonPerDay = entity.EstimatedCostPerPersonPerDay,
                AlignmentScore = entity.AlignmentScore,
                ScoreBreakdownJson = entity.ScoreBreakdownJson,
                ParentPlanId = entity.ParentPlanId,
                VariationNumber = entity.VariationNumber,
                PreferenceProfileId = entity.PreferenceProfileId
            };
        }
    }
}
