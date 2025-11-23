using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(Name), nameof(PeriodStart), nameof(PeriodEnd), IsUnique = true)]
    public class TrendingTag
    {
        [Key]
        public Guid TagId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        public float Score { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PeriodStart { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PeriodEnd { get; set; }

        // Navigation Properties
        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}
