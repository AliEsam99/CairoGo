using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(PlaceId), nameof(DayOfWeek), IsUnique = true)]
    public class PlaceOperatingHours
    {
        [Key]
        public Guid PlaceOperatingHoursId { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        [Required(ErrorMessage = "Day of week is required")]
        public DayOfWeek DayOfWeek { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? OpenTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? CloseTime { get; set; }

        public bool IsClosed { get; set; } = false;

        // Navigation Properties
        public Place Place { get; set; } = default!;
    }
}
