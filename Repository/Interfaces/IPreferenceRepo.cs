using CairoGo.Models.Entity;

namespace CairoGo.Repository.Interfaces
{
    public interface IPreferenceRepo:IBaseRepo<PreferenceProfile>
    {
        // Get preference profile by user ID
        Task<PreferenceProfile?> GetPreferencesByUserIdAsync(Guid userId);
        // Add activity type to preference profile
        Task AddActivityTypeAsync(Guid userId, Guid activityType);
        // Remove activity type from preference profile
        Task RemoveActivityTypeAsync(Guid profileId, Guid activityTypeId);
        // Get activity types for a preference profile
        Task<List<ActivityType>> GetActivityTypesAsync(Guid profileId);
        // Check if a user has a preference profile
        Task<bool> HasProfileAsync(Guid userId);
        public  Task SetActivityTypesAsync(PreferenceProfile profile, List<Guid> activityIds);



    }
}
