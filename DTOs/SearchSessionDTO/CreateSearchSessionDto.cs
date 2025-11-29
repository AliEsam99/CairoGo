using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CairoGo.DTOs.SearchSessionDTO
{
    public class CreateSearchSessionDto 
    {
        public Guid? UserId { get; set; }

        [StringLength(500, ErrorMessage = "Search query cannot exceed 500 characters")]
        public string? SearchQuery { get; set; }

        public string? SearchParamsJson { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Result count must be non-negative")]
        public int ResultCount { get; set; }
        public string? ClickedPlaceIds { get; set; }
        public int? SelectedPosition { get; set; }
        public TimeSpan? TimeToClick { get; set; }
    }
}

