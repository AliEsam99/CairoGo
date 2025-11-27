using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CairoGo.Repository.Implementations
{
    public class TripSlotRepo : Repository<TripSlot>, ITripSlotRepo
    {
        public TripSlotRepo(CairoGoDbContext Db) : base(Db)
        {
        }
        public async Task<List<TripSlot>> GetSlotsByDayIdAsync(Guid tripDayId)
        {
            return await _DbSet.Where(s=>s.TripDayId==tripDayId)
                .OrderBy(s=>s.SlotType)
                .ToListAsync();
        }

        public async Task AddSlotToDayAsync(Guid tripDayId, TripSlot slot)
        {
            slot.TripDayId = tripDayId;
             await _DbSet.AddAsync(slot);
        }

        public async Task DeleteSlotAsync(Guid slotId)
        {
            var slot = await _DbSet.FindAsync(slotId);
            if(slot == null)
                throw new KeyNotFoundException($"TripSlot with ID {slotId} not found.");
            _DbSet.Remove(slot);
        }


    }
}
