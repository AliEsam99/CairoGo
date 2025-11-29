using CairoGo.DTOs.ExperimentAssignmentDTO;
using CairoGo.Models.Entity;

namespace CairoGo.Mappings
{
    public static class ExperimentAssignmentMapper
    {
        public static ExperimentAssignment ToEntity(this ExperimentAssignmentCreateDto dto)
        {
            return new ExperimentAssignment
            {
                UserId = dto.UserId,
                ExperimentName = dto.ExperimentName,
                VariantName = dto.VariantName,
                AssignedAt = DateTime.UtcNow,
            };
        }
        public static void UpdateEntity(this ExperimentAssignment entity,ExperimentAssignmentUpdateDto dto)
        {
            entity.CompletedAt = dto.CompletedAt;
            entity.TripPlansGenerated = dto.TripPlansGenerated;
            entity.PlacesClicked = dto.PlacesClicked;
            entity.ConversionSuccess = dto.ConversionSuccess;
        }
        public static ExperimentAssignmentResponseDto ToDto(this ExperimentAssignment entity)
        {
            return new ExperimentAssignmentResponseDto
            {
                AssignmentId = entity.AssignmentId,
                UserId = entity.UserId,
                ExperimentName = entity.ExperimentName,
                VariantName = entity.VariantName,
                AssignedAt = entity.AssignedAt,
                CompletedAt = entity.CompletedAt,
                TripPlansGenerated = entity.TripPlansGenerated,
                PlacesClicked = entity.PlacesClicked,
                ConversionSuccess = entity.ConversionSuccess
            };
        }
    }
}
