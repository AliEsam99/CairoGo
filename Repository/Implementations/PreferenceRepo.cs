using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CairoGo.Repository.Implementations
{
    public class PreferenceRepo : Repository<PreferenceProfile>, IPreferenceRepo
    {
        public PreferenceRepo(CairoGoDbContext Db) : base(Db)
        {
        }
        // Add an activity type to a user's preference profile
        public async Task AddActivityTypeAsync(Guid userId, Guid activityTypeId)
        {
            var profile = await GetPreferencesByUserIdAsync(userId);
            if (profile == null)
            {
                throw new KeyNotFoundException($"Preference profile for User ID {userId} not found");
            }
            var activity = await _db.ActivityTypes.FindAsync(activityTypeId);
            if (activity == null)
            {
                throw new KeyNotFoundException($"Activity Type with ID {activityTypeId} not found");
            }
            if (!profile.ActivityTypes.Contains(activity))
            {
                profile.ActivityTypes.Add(activity);
                await _db.SaveChangesAsync();
            }
        }
        // Remove an activity type from a user's preference profile
        public async Task RemoveActivityTypeAsync(Guid profileId, Guid activityTypeId)
        {
            var profile = await GetPreferencesByUserIdAsync(profileId);
            if (profile == null)
            {
                throw new KeyNotFoundException($"Preference profile for ID {profileId} not found");
            }
            var activity = await _db.ActivityTypes.FindAsync(activityTypeId);
            if (activity != null )
            {
                profile.ActivityTypes.Remove(activity);
                await _db.SaveChangesAsync();
            } 
        }
        // Get all activity types associated with a user's preference profile
        public Task<List<ActivityType>> GetActivityTypesAsync(Guid profileId)
        {
            return _db.ActivityTypes
                .Where(a => a.PreferenceProfiles.Any(p => p.ProfileId == profileId))
                .ToListAsync();
        }
        // Get preference profile by user ID
        public async Task<PreferenceProfile?> GetPreferencesByUserIdAsync(Guid userId)
        {
            return await _db.PreferenceProfiles
                .Include(p => p.ActivityTypes)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }
        // Check if a user has a preference profile

        public async Task<bool> HasProfileAsync(Guid userId)
        {
            return await _db.PreferenceProfiles.AnyAsync(p => p.UserId == userId);
        }
        // Set activity types for a preference profile
        public async Task SetActivityTypesAsync(PreferenceProfile profile, List<Guid> activityIds)
        {
            var activities = await _db.ActivityTypes
                .Where(a => activityIds.Contains(a.ActivityTypeId))
                .ToListAsync();

            profile.ActivityTypes = activities;
        }

    }
}
