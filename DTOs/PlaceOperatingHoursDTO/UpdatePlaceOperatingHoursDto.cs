using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.PlaceOperatingHoursDTO
{
    public class UpdatePlaceOperatingHoursDto
    {

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }

        public bool IsClosed { get; set; }

    }
}
