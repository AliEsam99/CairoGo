using CairoGo.Models.ENums;
using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.ActivityTypeDTO
{
    public class ActivityTypeUpdateDto
    {

        [Required(ErrorMessage = "Activity type is required")]
        public ActivityKind Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
