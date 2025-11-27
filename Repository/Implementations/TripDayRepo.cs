using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CairoGo.Repository.Implementations
{
    public class TripDayRepo : Repository<TripDay>, ITripDayRepo
    {
        public TripDayRepo(CairoGoDbContext Db) : base(Db) { }
 
        public async Task<List<TripDay>> GetDaysByTripIdAsync(Guid tripPlanId)
        {
            return await _DbSet.Where(d => d.TripPlanId == tripPlanId)
                               .OrderBy(d => d.DayNumber)
                               .ToListAsync();
        }

        public async Task AddDayToTripAsync(Guid tripPlanId, TripDay day)
        {
            day.TripPlanId = tripPlanId;
            await _DbSet.AddAsync(day);
        }

        // حذف يوم
        public async Task DeleteDayAsync(Guid dayId)
        {
            var day = await _DbSet.FindAsync(dayId);
            if (day == null)
                throw new KeyNotFoundException($"TripDay with ID {dayId} not found.");

            _DbSet.Remove(day);
        }
    }
}
