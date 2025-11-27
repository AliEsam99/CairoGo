using CairoGo.DTOs.TripDayDTO;
using CairoGo.Mappings;
using CairoGo.Models.Entity;
using CairoGo.Repository.Implementations;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripDayController : ControllerBase
    {
        private readonly ITripDayRepo _repo;

        public TripDayController(ITripDayRepo repo)
        {
            _repo = repo;
        }
        [HttpPost("trip/{tripId}")]
        public async Task<IActionResult> AddDay(Guid tripId, [FromBody] TripDayCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tripDay = dto.ToEntity();
            await _repo.AddDayToTripAsync(tripId, tripDay);
            return Ok(tripDay.ToDto());
        }
        [HttpPut("{dayId}/update")]
        public async Task<IActionResult> UpdateTripDay(Guid dayId, [FromBody] TripDayUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tripDay = await _repo.GetByIdAsync(dayId);
            if (tripDay == null)
                return NotFound("Trip day not found.");

            tripDay.UpdateEntity(dto);
            await _repo.UpdateAsync(tripDay);

            return Ok(tripDay.ToDto());
        }
        [HttpGet("trip/{tripId}")]
        public async Task<IActionResult> GetDays(Guid tripId)
        {
            var days = (await _repo.GetDaysByTripIdAsync(tripId)).Select(d => d.ToDto()).ToList();
            return Ok(days);
        }

        [HttpGet("{tripId}")]
        public async Task<IActionResult> GetDayById(Guid tripId)
        {
            var day = (await _repo.GetByIdAsync(tripId));
            return Ok(day.ToDto());
        }

        [HttpDelete("{dayId}")]
        public async Task<IActionResult> DeleteDay(Guid dayId)
        {
            try
            {
                await _repo.DeleteDayAsync(dayId);
                return Ok("Day deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
