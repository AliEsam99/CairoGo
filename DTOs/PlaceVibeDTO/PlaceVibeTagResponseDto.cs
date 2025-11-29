using CairoGo.Models.ENums;

namespace CairoGo.DTOs.PlaceVibeDTO
{
    public class PlaceVibeTagResponseDto
    {
        public Guid PlaceVibeTagId { get; set; }
        public Guid PlaceId { get; set; }
        public TravelVibe Value { get; set; }
    }
}
