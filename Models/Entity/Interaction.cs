using CairoGo.Models.ENums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    public class Interaction
    {
        [Key]
        public Guid InteractionId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Interaction type is required")]
        public InteractionType Type { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? ContextJson { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        // Navigation Properties
        public UserApplication User { get; set; } = default!;
        public Place Place { get; set; } = default!;
    }
}
