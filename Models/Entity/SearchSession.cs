using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CairoGo.Models.Entity
{
    [Index(nameof(UserId), nameof(CreatedAt))]
    public class SearchSession
    {
        [Key]
        public Guid SearchSessionId { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }

        [StringLength(500)]
        public string? SearchQuery { get; set; }    
        public string? SearchParamsJson { get; set; }
        public int ResultCount { get; set; }
        public string? ClickedPlaceIds { get; set; }
        public int? SelectedPosition { get; set; }

        public TimeSpan? TimeToClick { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public UserApplication? User { get; set; }
    }
}
