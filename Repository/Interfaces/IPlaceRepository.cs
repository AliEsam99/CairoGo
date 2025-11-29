using CairoGo.Models.Entity;
using CairoGo.Models.ENums;

namespace CairoGo.Repository.Interfaces
{
    public interface IPlaceRepository : IBaseRepo<Place>
    {
        Task<List<Place>> SearchByNameAsync(string name);
        Task<List<Place>> SearchByDistractAsync(string distract);
        Task<List<Place>> SearchByVibesAsync(TravelVibe vibes);
        Task<List<Place>> SearchByWeatherPref(WeatherPref IndoorOutdoor);
        Task<List<Place>> SearchByCostTierAsync(CostTier cost);
        Task<Place?> GetDetailsOfPlaceAsync(Guid PlaceId);
        Task<List<Place>> SearchByCategoryAsync(PlaceCategory category);
        Task<List<Place>> GetTrendingPlacesAsync(int count);

    }
}
