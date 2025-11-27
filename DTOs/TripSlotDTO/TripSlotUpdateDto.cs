using CairoGo.Models.ENums;
using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.TripSlotDTO
{
    public class TripSlotUpdateDto
    {
        [Required]
        public SlotType SlotType { get; set; }

        [Required]
        public Guid PlaceId { get; set; }

        public Guid? AlternatePlaceId { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Per person cost must be positive.")]
        public decimal? PerPersonCost { get; set; }
    }
}
