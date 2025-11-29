using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace CairoGo.Repository.Implementations
{
    public class ActivityTypeRepo : Repository<ActivityType>, IActivityTypeRepo
    {
        public ActivityTypeRepo(CairoGoDbContext Db) : base(Db)
        {

        }
        public async Task<List<ActivityType>> GetAllActivityTypeAsync()
        {
            return await _DbSet.OrderBy(t=>t.Name).ToListAsync();
        }

        public async Task DeleteActivittyTypeAsync(Guid activittyTypeId)
        {
            var activity = await _DbSet.FindAsync(activittyTypeId);
            if (activity == null)
                throw new KeyNotFoundException($"ActivityType with ID {activittyTypeId} not found");
            _DbSet.Remove(activity);
            await _db.SaveChangesAsync();
        }
    }
}
