using CairoGo.Models.Entity;

namespace CairoGo.Repository.Interfaces
{
    public interface ISearchSessionRepo : IBaseRepo<SearchSession>
    {
        Task<List<SearchSession>> GetByUserIdPaginatedAsync(Guid userId, int page, int pageSize);
        Task<int> CountByUserIdAsync(Guid userId);
        Task<Dictionary<Guid, int>> GetClickedPlacesCountAsync(int days);
        Task<double?> GetAverageTimeToClickAsync(int days);

    }
}
