using CairoGo.DTOs.SearchSessionDTO;
using CairoGo.Mappings;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchSessionController : ControllerBase
    {
        private readonly ISearchSessionRepo _searchRepo;

        public SearchSessionController(ISearchSessionRepo searchRepo)
        {
            _searchRepo = searchRepo;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateSearchSessionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = SearchSessionMapper.ToEntity(dto);
            await _searchRepo.AddAsync(entity);

            return CreatedAtAction(
                nameof(GetById),
                new { id = entity.SearchSessionId },
                SearchSessionMapper.ToResponse(entity)
            );
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(
           Guid userId,
           [FromQuery] int page = 1,
           [FromQuery] int pageSize = 20)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 20;

            var sessions = await _searchRepo.GetByUserIdPaginatedAsync(userId, page, pageSize);
            var totalCount = await _searchRepo.CountByUserIdAsync(userId);

            var response = new PaginatedResponseDto<SearchSessionResponseDto>
            {
                Data = sessions.Select(s => SearchSessionMapper.ToResponse(s)).ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var session = await _searchRepo.GetByIdAsync(id);
            if (session == null )
                return NotFound(new { Message = "Search session not found" });

            return Ok(SearchSessionMapper.ToResponse(session));
        }
        [HttpPost("{id}/add-click")]
        public async Task<IActionResult> AddClick(Guid id, [FromBody] AddClickDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var session = await _searchRepo.GetByIdAsync(id);
            if (session == null )
                return NotFound(new { Message = "Search session not found" });

            // Parse existing clicked places
            List<Guid> clickedPlaces;
            if (string.IsNullOrEmpty(session.ClickedPlaceIds))
            {
                clickedPlaces = new List<Guid>();
            }
            else
            {
                try
                {
                    clickedPlaces = JsonSerializer.Deserialize<List<Guid>>(session.ClickedPlaceIds) ?? new List<Guid>();
                }
                catch (JsonException)
                {
                    clickedPlaces = new List<Guid>();
                }
            }

            // Check if already clicked
            if (clickedPlaces.Contains(dto.PlaceId))
                return BadRequest(new { Message = "Place already clicked in this session" });

            // Add the new click
            clickedPlaces.Add(dto.PlaceId);
            session.ClickedPlaceIds = JsonSerializer.Serialize(clickedPlaces);

            // Update SelectedPosition and TimeToClick if first click
            if (!session.SelectedPosition.HasValue && dto.Position.HasValue)
            {
                session.SelectedPosition = dto.Position.Value;
                session.TimeToClick = DateTime.UtcNow - session.CreatedAt;
            }

            await _searchRepo.UpdateAsync(session);

            return Ok(SearchSessionMapper.ToResponse(session));
        }
        [HttpDelete("{id}/anonymize")]
        public async Task<IActionResult> Anonymize(Guid id)
        {
            var session = await _searchRepo.GetByIdAsync(id);
            if (session == null)
                return NotFound(new { Message = "Search session not found" });

            // Anonymize user data
            session.UserId = null;
            session.SearchQuery = "[REDACTED]";
            session.SearchParamsJson = null;
            session.ClickedPlaceIds = null;
            await _searchRepo.UpdateAsync(session);
            return NoContent();
        }
        [HttpGet("analytics/popular-places")]
        public async Task<IActionResult> GetPopularPlaces(
            [FromQuery] int days = 7,
            [FromQuery] int limit = 10)
        {
            if (days < 1) days = 7;
            if (limit < 1 || limit > 50) limit = 10;

            var placeCounts = await _searchRepo.GetClickedPlacesCountAsync(days);

            var result = placeCounts
                .OrderByDescending(kvp => kvp.Value)
                .Take(limit)
                .Select(kvp => new
                {
                    PlaceId = kvp.Key,
                    ClickCount = kvp.Value
                });

            return Ok(result);
        }
        [HttpGet("analytics/avg-time-to-click")]
        public async Task<IActionResult> GetAvgTimeToClick([FromQuery] int days = 7)
        {
            if (days < 1) days = 7;

            var avgSeconds = await _searchRepo.GetAverageTimeToClickAsync(days);

            if (!avgSeconds.HasValue)
                return Ok(new
                {
                    Message = "No data available for the specified period",
                    AverageSeconds = (double?)null,
                    Period = $"Last {days} days"
                });

            return Ok(new
            {
                AverageSeconds = Math.Round(avgSeconds.Value, 2),
                AverageMinutes = Math.Round(avgSeconds.Value / 60, 2),
                Period = $"Last {days} days"
            });
        }
    }
}
