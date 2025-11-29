using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CairoGo.Repository.Implementations
{
    public class SearchSessionRepo : Repository<SearchSession>, ISearchSessionRepo
    {
        public SearchSessionRepo(CairoGoDbContext db) : base(db) { }
        public async Task<List<SearchSession>> GetByUserIdPaginatedAsync(Guid userId, int page, int pageSize)
        {
            return await _db.SearchSessions
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountByUserIdAsync(Guid userId)
        {
            return await _db.SearchSessions
                .Where(s => s.UserId == userId)
                .CountAsync();
        }
        public async Task<double?> GetAverageTimeToClickAsync(int days)
        {
            var since = DateTime.UtcNow.AddDays(-days);

            var sessions = await _db.SearchSessions
                .Where(s => s.CreatedAt >= since  && s.TimeToClick.HasValue)
                .Select(s => s.TimeToClick!.Value.TotalSeconds)
                .ToListAsync();

            return sessions.Any() ? sessions.Average() : null;
        }

        public async Task<Dictionary<Guid, int>> GetClickedPlacesCountAsync(int days)
        {
            var since = DateTime.UtcNow.AddDays(-days);

            var sessions = await _db.SearchSessions
                .Where(s => s.CreatedAt >= since && s.ClickedPlaceIds != null)
                .Select(s => s.ClickedPlaceIds)
                .ToListAsync();

            var placeClickCounts = new Dictionary<Guid, int>();

            foreach (var clickedPlaceIdsJson in sessions)
            {
                if (string.IsNullOrEmpty(clickedPlaceIdsJson)) continue;

                try
                {
                    var placeIds = JsonSerializer.Deserialize<List<Guid>>(clickedPlaceIdsJson);
                    if (placeIds == null) continue;

                    foreach (var placeId in placeIds)
                    {
                        if (placeClickCounts.ContainsKey(placeId))
                            placeClickCounts[placeId]++;
                        else
                            placeClickCounts[placeId] = 1;
                    }
                }
                catch (JsonException)
                {
                    // Skip invalid JSON
                    continue;
                }
            }
            return placeClickCounts;
        }
    }
}
