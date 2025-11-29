using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.PreferenceSignalDTO
{
    public class CreateUserPreferenceSignalDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string SignalType { get; set; } = default!;

        [Required]
        [StringLength(100)]
        public string Key { get; set; } = default!;

        public float Weight { get; set; } = 0f;
    }
}
