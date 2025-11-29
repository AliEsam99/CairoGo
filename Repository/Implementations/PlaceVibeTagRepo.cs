using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CairoGo.Repository.Implementations
{
    public class PlaceVibeTagRepo : Repository<PlaceVibeTag>, IPlaceVibeTagRepo
    {
        public PlaceVibeTagRepo(CairoGoDbContext Db) : base(Db)
        {
        }

        public async Task<List<PlaceVibeTag>> GetByPlaceIdAsync(Guid placeId)
        {
            return await _db.PlaceVibeTags
                .Where(p => p.PlaceId == placeId)
                .ToListAsync();
        }
    }
}
