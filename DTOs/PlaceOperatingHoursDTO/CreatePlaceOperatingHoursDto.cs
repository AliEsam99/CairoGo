using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.PlaceOperatingHoursDTO
{
    public class CreatePlaceOperatingHoursDto : IValidatableObject
    {
        [Required]
        public Guid PlaceId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }

        public bool IsClosed { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsClosed)
            {
                if (OpenTime != null && CloseTime != null)
                    yield return new ValidationResult("OpenTime and CloseTime must be null when IsClosed is true",
                        new[] { nameof(OpenTime), nameof(CloseTime) });
            }
            else
            {
                if(OpenTime == null || CloseTime == null)
                {
                    yield return new ValidationResult("OpenTime and CloseTime are required when IsClosed is false",
                       new[] { nameof(OpenTime), nameof(CloseTime) });
                }
                if (OpenTime >= CloseTime)
                {
                    yield return new ValidationResult("OpenTime Must Be Before CloseTime",
                       new[] { nameof(OpenTime), nameof(CloseTime) });
                }
            }
        }
    }
}
