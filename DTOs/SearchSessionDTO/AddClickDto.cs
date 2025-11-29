using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.SearchSessionDTO
{
    public class AddClickDto
    {
        [Required(ErrorMessage = "PlaceId is required")]
        public Guid PlaceId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Position must be non-negative")]
        public int? Position { get; set; }
    }
}
