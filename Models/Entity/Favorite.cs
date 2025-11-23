using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(UserId), nameof(PlaceId), IsUnique = true)]
    [Index(nameof(UserId), nameof(CreatedAt))]
    public class Favorite
    {
        [Key]
        public Guid FavoriteId { get; set; } = Guid.NewGuid();

        [StringLength(50)]
        public string? UserCategory { get; set; }

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
