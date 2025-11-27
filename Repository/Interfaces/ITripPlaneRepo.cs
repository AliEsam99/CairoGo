using CairoGo.Models.Entity;

namespace CairoGo.Repository.Interfaces
{
    public interface ITripPlaneRepo:IBaseRepo<TripPlan>
    {
        // Get trip plans by user ID
        Task<List<TripPlan>> GetTripPlansByUserIdAsync(Guid userId);
        // Get trip plan with days and slots
        Task<TripPlan?> GetWithDaysAndSlotsAsync(Guid tripPlanId);
        // Get full trip plan by ID
        Task<TripPlan?> GetFullByIdAsync(Guid tripPlanId);
        // Create a new trip plan
        Task<TripPlan> CreateTripPlanAsync(TripPlan plan);
        // Add an alternative trip plan
        Task<TripPlan> AddAlternativeAsync(Guid basePlanId, TripPlan alternative);
        // Add a day to a trip plan
        Task<TripDay> AddDayAsync(Guid tripPlanId, TripDay day);
        // Remove a day from a trip plan
        Task RemoveDayAsync(Guid tripDayId);
        // Add a slot to a trip day
        Task<TripSlot> AddSlotAsync(Guid tripDayId, TripSlot slot);
        // Remove a slot from a trip day
        Task RemoveSlotAsync(Guid tripSlotId);
        // Get current and upcoming trip plans by user ID from a specific date
        Task<List<TripPlan>> GetCurrentAndUpcomingByUserAsync(Guid userId, DateTime fromDate);
        // Update alignment score for a trip plan
        Task UpdateAlignmentScoreAsync(Guid tripPlanId, decimal score, string? breakdownJson = null);

        // Delete a trip plan by ID
        Task DeletePlanAsync(Guid tripPlanId);

    }
}
