using CairoGo.Models.ENums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(TripDayId), nameof(SlotType), IsUnique = true)]
    public class TripSlot
    {
        [Key]
        public Guid TripSlotId { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey(nameof(TripDay))]
        public Guid TripDayId { get; set; }

        [Required(ErrorMessage = "Slot type is required")]
        public SlotType SlotType { get; set; }

        [Required]
        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        [ForeignKey(nameof(AlternatePlace))]
        public Guid? AlternatePlaceId { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        // Ai must compute it
        public decimal? PerPersonCost { get; set; }

        // Navigation Properties
        public TripDay TripDay { get; set; } = default!;

        [ForeignKey(nameof(PlaceId))]
        public Place Place { get; set; } = default!;

        [ForeignKey(nameof(AlternatePlaceId))]
        public Place? AlternatePlace { get; set; }
    }
}
