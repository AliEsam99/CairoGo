using CairoGo.Models.Entity;

namespace CairoGo.Repository.Interfaces
{
    public interface ITripDayRepo:IBaseRepo<TripDay>
    {
        Task<List<TripDay>> GetDaysByTripIdAsync(Guid tripPlanId);
        Task AddDayToTripAsync(Guid tripPlanId, TripDay day);
        public  Task DeleteDayAsync(Guid dayId);
    }
}
