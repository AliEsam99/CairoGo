using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.TripDayDTO
{
    public class TripDayCreateDto
    {
        [Required]
        [Range(1, 4, ErrorMessage = "Day number must be between 1 and 4")]
        public int DayNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Estimated day cost must be positive.")]
        public decimal? EstimatedDayCost { get; set; }
    }
}
