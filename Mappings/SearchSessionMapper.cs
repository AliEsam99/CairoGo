using CairoGo.DTOs.SearchSessionDTO;
using CairoGo.Models.Entity;
using System.Text.Json;

namespace CairoGo.Mappings
{
    public static class SearchSessionMapper
    {
        public static SearchSession ToEntity(CreateSearchSessionDto dto)
        {
            return new SearchSession
            {
                UserId = dto.UserId,
                SearchQuery = dto.SearchQuery?.Trim(),
                SearchParamsJson = dto.SearchParamsJson,
                ResultCount = dto.ResultCount,
                ClickedPlaceIds = null, // Will be set when clicks are added
                CreatedAt = DateTime.UtcNow,
            };
        }
        public static SearchSessionResponseDto ToResponse(SearchSession entity)
        {
            List<Guid>? clickedPlaceIds = null;

            // Parse ClickedPlaceIds JSON array
            if (!string.IsNullOrEmpty(entity.ClickedPlaceIds))
            {
                try
                {
                    clickedPlaceIds = JsonSerializer.Deserialize<List<Guid>>(entity.ClickedPlaceIds);
                }
                catch (JsonException)
                {
                    // If parsing fails, return null
                    clickedPlaceIds = null;
                }
            }

            return new SearchSessionResponseDto
            {
                SearchSessionId = entity.SearchSessionId,
                UserId = entity.UserId,
                SearchQuery = entity.SearchQuery,
                SearchParamsJson = entity.SearchParamsJson,
                ResultCount = entity.ResultCount,
                ClickedPlaceIds = clickedPlaceIds,
                SelectedPosition = entity.SelectedPosition,
                TimeToClick = entity.TimeToClick,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
