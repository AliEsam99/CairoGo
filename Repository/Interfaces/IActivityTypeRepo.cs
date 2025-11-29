using CairoGo.Models.Entity;

namespace CairoGo.Repository.Interfaces
{
    public interface IActivityTypeRepo:IBaseRepo<ActivityType>
    {
        // return all activity Types
        Task<List<ActivityType>> GetAllActivityTypeAsync();
        Task DeleteActivittyTypeAsync(Guid activittyTypeId);

    }
}
