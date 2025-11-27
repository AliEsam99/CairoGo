using CairoGo.Models.ENums;
using System.ComponentModel.DataAnnotations;

namespace CairoGo.DTOs.PreferenceDTO
{
    public class CreatePreferenceDto
    {
     
        public Guid UserId { get; set; }

    
        public TravelVibe TravelVibe { get; set; }

     
        public decimal Budget { get; set; }

      
        public WeatherPref WeatherPref { get; set; }

      
        public int TripDays { get; set; }

        public List<Guid> ActivityTypeIds { get; set; } = new();
    }
}
