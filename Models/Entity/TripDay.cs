using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(TripPlanId), nameof(DayNumber), IsUnique = true)]
    public class TripDay
    {
        [Key]
        public Guid TripDayId { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey(nameof(TripPlan))]
        public Guid TripPlanId { get; set; }

        [Required(ErrorMessage = "Day number is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Day number must be at least 1")]
        public int DayNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
    
        public decimal? EstimatedDayCost { get; set; }

        // Navigation Properties
        public TripPlan TripPlan { get; set; } = default!;
        public ICollection<TripSlot> TripSlots { get; set; } = new List<TripSlot>();
    }
}
