using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CairoGo.Repository.Implementations
{
    public class TripPlaneRepo : Repository<TripPlan>, ITripPlaneRepo
    {
        public TripPlaneRepo(CairoGoDbContext Db) : base(Db)
        {
        }
        // Add an alternative trip plan
        public async Task<TripPlan> AddAlternativeAsync(Guid basePlanId, TripPlan alternative)
        {
          using var tx= await _db.Database.BeginTransactionAsync();
            var basePlan = await _db.TripPlans
                .FirstOrDefaultAsync(tp => tp.TripPlanId == basePlanId);
            if (basePlan == null)
            throw new KeyNotFoundException("Base trip plan not found.");
            alternative.ParentPlanId = basePlanId;
            alternative.PreferenceProfileId = basePlan.PreferenceProfileId;
            alternative.UserId = basePlan.UserId;
            alternative.VariationNumber = (await _db.TripPlans
                .CountAsync(tp => tp.ParentPlanId == basePlanId)) + 1;
            await _db.TripPlans.AddAsync(alternative);
            await _db.SaveChangesAsync();
            basePlan.AlternativePlans.Add(alternative);
            _db.TripPlans.Update(basePlan);
            await _db.SaveChangesAsync();

            await tx.CommitAsync();
            return alternative;

        }
        // Add a day to a trip plan

        public async Task<TripDay> AddDayAsync(Guid tripPlanId, TripDay day)
        {
            var plan = await _db.TripPlans
               .Include(tp => tp.TripDaysCollection)
               .FirstOrDefaultAsync(p => p.TripPlanId == tripPlanId);
            if (plan == null)
                throw new KeyNotFoundException("Trip plan not found.");
            if(plan.TripDaysCollection.Any(d=>d.DayNumber == day.DayNumber))
                throw new InvalidOperationException("Day with the same number already exists in the trip plan.");
            day.TripPlanId = tripPlanId;
            await _db.TripDays.AddAsync(day);
            await _db.SaveChangesAsync();
            return day;
        }
        // Remove an alternative trip plan by its ID
        public async Task RemoveDayAsync(Guid tripDayId)
        {
            var day = await _db.TripDays
                .Include(d => d.TripSlots)
                .FirstOrDefaultAsync(d => d.TripDayId == tripDayId);
            if (day == null)
                throw new KeyNotFoundException("Trip day not found.");
            if(day.TripSlots.Any())
                throw new InvalidOperationException("Cannot remove a trip day that has slots assigned.");
            _db.TripDays.Remove(day);
            await _db.SaveChangesAsync();
        }
        // Add a slot to a trip day

        public async Task<TripSlot> AddSlotAsync(Guid tripDayId, TripSlot slot)
        {
            var day = await _db.TripDays
                .Include(d => d.TripSlots)
                .FirstOrDefaultAsync(d => d.TripDayId == tripDayId);
            if (day == null)
                throw new KeyNotFoundException("Trip day not found.");
            if(day.TripSlots.Any(s=>s.SlotType == slot.SlotType && s.PlaceId == slot.PlaceId))
                throw new InvalidOperationException("A slot with the same type and place already exists in this trip day.");
            slot.TripDayId = tripDayId;
            await _db.TripSlots.AddAsync(slot);
            await _db.SaveChangesAsync();
            return slot;
        }
        // Create a new trip plan
        public async Task<TripPlan> CreateTripPlanAsync(TripPlan plan)
        {
           await _db.TripPlans.AddAsync(plan);
              await _db.SaveChangesAsync();
              return plan;
        }
        // Remove a slot from a trip day
        public Task RemoveSlotAsync(Guid tripSlotId)
        {
           var slot =  _db.TripSlots
                .FirstOrDefault(s => s.TripSlotId == tripSlotId);
            if (slot == null)
                throw new KeyNotFoundException("Trip slot not found.");
            _db.TripSlots.Remove(slot);
            return _db.SaveChangesAsync();
        }
        // Delete a trip plan by its ID
        public async Task DeletePlanAsync(Guid tripPlanId)
        {
            var plan = await _db.TripPlans
                 .Include(p => p.TripDaysCollection)
                     .ThenInclude(d => d.TripSlots)
                 .Include(p => p.AlternativePlans)
                 .FirstOrDefaultAsync(p => p.TripPlanId == tripPlanId);
            if (plan == null)
                throw new KeyNotFoundException("Trip plan not found.");
            foreach (var day in plan.TripDaysCollection)
            {
                _db.TripSlots.RemoveRange(day.TripSlots);
            }
            if(plan.TripDaysCollection.Any())
                _db.TripDays.RemoveRange(plan.TripDaysCollection);
            if(plan.AlternativePlans.Any())
                _db.TripPlans.RemoveRange(plan.AlternativePlans);
            _db.TripPlans.Remove(plan);
            await _db.SaveChangesAsync();
        }
        // Get current and upcoming trip plans by user ID from a specific date
        public async Task<List<TripPlan>> GetCurrentAndUpcomingByUserAsync(Guid userId, DateTime fromDate)
        {
            return await _db.TripPlans
                .Where(tp => tp.UserId == userId && (tp.StartDate == null || tp.StartDate >= fromDate))
                .OrderBy(tp => tp.StartDate)
                .ToListAsync();
        }
        // Get full trip plan details by its ID
        public async Task<TripPlan?> GetFullByIdAsync(Guid tripPlanId)
        {
            return await _db.TripPlans
                .Include(tp => tp.PreferenceProfile)
                .ThenInclude(td => td.ActivityTypes)
                .Include(tp=>tp.AlternativePlans)
                .Include(tp => tp.TripDaysCollection)
                .ThenInclude(td => td.TripSlots)
                .FirstOrDefaultAsync(tp => tp.TripPlanId == tripPlanId);
        }
        // Get all trip plans by user ID
        public async Task<List<TripPlan>> GetTripPlansByUserIdAsync(Guid userId)
        {
            return await _db.TripPlans
                .Where(tp=> tp.UserId == userId)
                .OrderByDescending(tp=>tp.CreatedAt).ToListAsync();
        }
        // Get a trip plan with its days and slots by its ID
        public async Task<TripPlan?> GetWithDaysAndSlotsAsync(Guid tripPlanId)
        {
            return await _db.TripPlans
                .Include(tp=> tp.TripDaysCollection)
                .ThenInclude(td => td.TripSlots).ThenInclude(ts => ts.Place)
                .FirstOrDefaultAsync(tp => tp.TripPlanId == tripPlanId);
        }
        // Update alignment score for a trip plan
        public async Task UpdateAlignmentScoreAsync(Guid tripPlanId, decimal score, string? breakdownJson = null)
        {
            var plan = await _db.TripPlans.FirstOrDefaultAsync(tp => tp.TripPlanId == tripPlanId);
            if (plan == null)
                throw new KeyNotFoundException("Trip plan not found.");
            plan.AlignmentScore = score;
            if(breakdownJson != null)
                plan.ScoreBreakdownJson = breakdownJson;
            plan.UpdatedAt = DateTime.UtcNow;
            _db.TripPlans.Update(plan);
            await _db.SaveChangesAsync();
        }
    }
}
