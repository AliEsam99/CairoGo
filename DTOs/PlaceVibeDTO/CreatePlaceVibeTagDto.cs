using CairoGo.Models.ENums;
using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.PlaceVibeDTO
{
    public class CreatePlaceVibeTagDto
    {
        [Required]
        public Guid PlaceId { get; set; }

        [Required]
        public TravelVibe Value { get; set; }
    }
}
