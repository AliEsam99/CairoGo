using CairoGo.Models.Entity;

namespace CairoGo.Repository.Interfaces
{
    public interface IUserPreferenceSignalRepository
    {
        Task<UserPreferenceSignal?> GetByIdAsync(Guid signalId);
        Task<List<UserPreferenceSignal>> GetByUserAsync(Guid userId);
        Task<UserPreferenceSignal> AddAsync(UserPreferenceSignal signal);
        Task<UserPreferenceSignal> UpdateAsync(UserPreferenceSignal signal);
        Task DeleteAsync(Guid signalId);
        // helper: find by user + type + key
        Task<UserPreferenceSignal?> GetByUserTypeKeyAsync(Guid userId, string signalType, string key);
    }
}
