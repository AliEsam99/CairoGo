using CairoGo.DTOs.TripPlaneDTO;
using CairoGo.Mappings;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripPlaneController : ControllerBase
    {
        private readonly ITripPlaneRepo _tripRepo;
        public TripPlaneController(ITripPlaneRepo tripRepo)
        {
            _tripRepo = tripRepo;
        }
        // api/tripPlane/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateTripPlan([FromBody] TripPlanCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tripPlan = dto.ToEntity();
            await _tripRepo.AddAsync(tripPlan);
            return Ok(tripPlan.ToDto());
        }
        // api/tripPlane/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserTrips(Guid userId)
        {
            var trips = await _tripRepo.GetTripPlansByUserIdAsync(userId);
            var tripsDto = trips.Select(t => t.ToDto());
            return Ok(tripsDto);
        }
        // api/tripPlane/{tripId}
        [HttpGet("{tripId}")]
        public async Task<IActionResult> GetTrip(Guid tripId)
        {
            var trip = await _tripRepo.GetWithDaysAndSlotsAsync(tripId);
            if (trip == null)
                return NotFound("Trip plan not found.");
            return Ok(trip.ToDto());
        }
        // api/tripPlane/{tripId}/day
        [HttpPost("day/{tripId}")]
        public async Task<IActionResult> AddTripDay(Guid tripId, [FromBody] TripDay tripDay)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _tripRepo.AddDayAsync(tripId,tripDay);
            return Ok(tripDay);
        }
        // api/tripPlane/day/{dayId}/slot
        [HttpPost("day/{dayId}/slot")]
        public async Task<IActionResult> AddTripSlot(Guid dayId, [FromBody] TripSlot tripSlot)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _tripRepo.AddSlotAsync(dayId, tripSlot);
            return Ok(tripSlot);
        }
        [HttpDelete("{tripId}")]
        public async Task<IActionResult> DeleteTripPlan(Guid tripId)
        {
            try
            {
                await _tripRepo.DeletePlanAsync(tripId);
                return Ok("Trip plan deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
