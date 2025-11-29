using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CairoGo.Repository.Implementations
{
    public class PlaceOperatingHoursRepo : Repository<PlaceOperatingHours>, IPlaceOperatingHoursRepo
    {
        public PlaceOperatingHoursRepo(CairoGoDbContext Db) : base(Db)
        {
        }

        public async Task<PlaceOperatingHours?> GetByPlaceAndDayAsync(Guid placeId, DayOfWeek day)
        {
            return await _db.PlaceOperatingHours.FirstOrDefaultAsync
                (p => p.PlaceId == placeId && p.DayOfWeek == day);
        }

        public async Task<List<PlaceOperatingHours>> GetByPlaceIdAsync(Guid placeId)
        {
            return await _db.PlaceOperatingHours.Where(p=>p.PlaceId == placeId)
                .ToListAsync();
        }
    }
}
