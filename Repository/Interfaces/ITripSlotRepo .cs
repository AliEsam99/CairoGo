using CairoGo.Models.Entity;

namespace CairoGo.Repository.Interfaces
{
    public interface ITripSlotRepo:IBaseRepo<TripSlot>
    {
        Task<List<TripSlot>> GetSlotsByDayIdAsync(Guid tripDayId);
        Task AddSlotToDayAsync(Guid tripDayId, TripSlot slot);
        Task DeleteSlotAsync(Guid slotId);

    }
}
