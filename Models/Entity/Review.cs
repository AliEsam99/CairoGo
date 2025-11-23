using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(UserId), nameof(PlaceId), IsUnique = true)]
    [Index(nameof(PlaceId), nameof(Rating))]
    public class Review
    {
        [Key]
        public Guid ReviewId { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public float Rating { get; set; }

        [StringLength(2000)]
        public string? Comment { get; set; }

        // Optional Enhancements
        public bool IsVerified { get; set; } = false;

        [Column(TypeName = "nvarchar(max)")]
        public string? PhotosJson { get; set; }

        //Ai must compute it

        public int HelpfulCount { get; set; } = 0;

        [DataType(DataType.Date)]
        public DateTime? VisitDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public UserApplication User { get; set; } = default!;
        public Place Place { get; set; } = default!;
    }
}
