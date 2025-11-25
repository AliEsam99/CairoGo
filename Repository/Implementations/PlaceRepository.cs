using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Models.ENums;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CairoGo.Repository.Implementations
{
    public class PlaceRepository : Repository<Place>, IPlaceRepository
    {
        public PlaceRepository(CairoGoDbContext _Db) : base(_Db)
        {
        }

        public async Task<List<Place>> SearchByNameAsync(string name)
        {
           return await _DbSet.Where(p => p.Name.Contains(name)).ToListAsync();
        }
        public async Task<List<Place>> SearchByDistractAsync(string distract)
        {
            return await _DbSet.Where(p => p.District.Contains(distract)).ToListAsync();
        }

        public async Task<List<Place>> SearchByCostTierAsync(CostTier cost)
        {
            return await _DbSet.Where(p => p.CostTier == cost).ToListAsync();
        }
        public async Task<Place?> GetDetailsOfPlaceAsync(Guid PlaceId)
        {
            return await _DbSet.Include(p => p.VibeTags)
                               .Include(p => p.Reviews)
                               .Include(p => p.OperatingHours)
                               .Include(p => p.Category)
                               .Include(p => p.CostTier)
                               .Include(p =>p.IndoorOutdoor)
                               .Include(p => p.FullDescription)
                               .FirstOrDefaultAsync(p => p.PlaceId == PlaceId);
        }
        public async Task<List<Place>> SearchByCategoryAsync(PlaceCategory category)
        {
            return await _DbSet.Where(p => p.Category == category).ToListAsync();
        }

        public async Task<List<Place>> GetTrendingPlacesAsync(int count)
        {
            return await _DbSet.OrderByDescending(p=> p.TrendingTags.Count).ToListAsync();
        }

        public async Task<List<Place>> SearchByVibesAsync(TravelVibe vibes)
        {
            return await _DbSet.Include(p => p.VibeTags).Where(p => p.VibeTags.Any(v => v.Value == vibes)).ToListAsync();
        }

        public async Task<List<Place>> SearchByWeatherPref(WeatherPref IndoorOutdoor)
        {
            return await _DbSet.Where(p => p.IndoorOutdoor == IndoorOutdoor).ToListAsync();
        }
    }
}
