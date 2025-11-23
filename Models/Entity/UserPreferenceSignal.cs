using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(UserId), nameof(SignalType), nameof(Key), IsUnique = true)]
    [Index(nameof(UserId), nameof(SignalType))]
    public class UserPreferenceSignal
    {
        [Key]
        public Guid SignalId { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Signal type is required")]
        [StringLength(100)]
        public string SignalType { get; set; } = default!;

        [Required(ErrorMessage = "Signal key is required")]
        [StringLength(100)]
        public string Key { get; set; } = default!;

        public float Weight { get; set; }

        public int ObservationCount { get; set; } = 0;

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public UserApplication User { get; set; } = default!;
    }
}
