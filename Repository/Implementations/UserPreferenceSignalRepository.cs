using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CairoGo.Repository.Implementations
{
    public class UserPreferenceSignalRepository: IUserPreferenceSignalRepository
    {
        private readonly CairoGoDbContext _db;
        private readonly DbSet<UserPreferenceSignal> _dbSet;

        public UserPreferenceSignalRepository(CairoGoDbContext db)
        {
            _db = db;
            _dbSet = db.Set<UserPreferenceSignal>();
        }

        public async Task<UserPreferenceSignal> AddAsync(UserPreferenceSignal signal)
        {
            await _dbSet.AddAsync(signal);
            await _db.SaveChangesAsync();
            return signal;
        }

        public async Task DeleteAsync(Guid signalId)
        {
            var entity = await _dbSet.FindAsync(signalId);
            if (entity == null) throw new KeyNotFoundException($"Signal {signalId} not found.");
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<UserPreferenceSignal?> GetByIdAsync(Guid signalId)
            => await _dbSet.FindAsync(signalId);

        public async Task<List<UserPreferenceSignal>> GetByUserAsync(Guid userId)
               => await _dbSet.Where(x => x.UserId == userId).ToListAsync();

        public async Task<UserPreferenceSignal?> GetByUserTypeKeyAsync(Guid userId, string signalType, string key)
               => await _dbSet.FirstOrDefaultAsync(x =>
                   x.UserId == userId &&
                   x.SignalType == signalType &&
                   x.Key == key);
        public async Task<UserPreferenceSignal> UpdateAsync(UserPreferenceSignal signal)
        {
            _dbSet.Update(signal);
            await _db.SaveChangesAsync();
            return signal;
        }
    }
}
