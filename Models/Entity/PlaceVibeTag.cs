using CairoGo.Models.ENums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{

    public class PlaceVibeTag
    {
        [Key]
        public Guid PlaceVibeTagId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Vibe tag value is required")]
        [StringLength(50, MinimumLength = 3)]
        public TravelVibe Value { get; set; }

        [Required]
        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        // Navigation Properties
        public Place Place { get; set; } = default!;
    }
}
