using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.PreferenceSignalDTO
{
    public class UpdateUserPreferenceSignalDto
    {
        public float? Weight { get; set; }

        [Range(0, int.MaxValue)]
        public int? ObservationCount { get; set; }
    }
}
