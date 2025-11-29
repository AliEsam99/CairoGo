using CairoGo.DTOs.TripSlotDTO;
using CairoGo.Mappings;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripSlotController : ControllerBase
    {
        private readonly ITripSlotRepo _repo;

        public TripSlotController(ITripSlotRepo repo)
        {
            _repo = repo;
        }
        [HttpPost("day/{dayId}")]
        public async Task<IActionResult> AddSlot(Guid dayId, [FromBody] TripSlotCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var slot = dto.ToEntity();
            await _repo.AddSlotToDayAsync(dayId, slot);
            return Ok(slot.ToDto());
        }
        [HttpPut("update/{slotId}")]
        public async Task<IActionResult> UpdateSlot(Guid slotId, [FromBody] TripSlotUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var slot = await _repo.GetByIdAsync(slotId);
            if (slot == null)
                return NotFound("Trip slot not found.");

            slot.UpdateEntity(dto);
            await _repo.UpdateAsync(slot);
            return Ok(slot.ToDto());
        }
        [HttpGet("day/{dayId}")]
        public async Task<IActionResult> GetSlots(Guid dayId)
        {
            var slots = await _repo.GetSlotsByDayIdAsync(dayId);
            return Ok(slots);
        }
        [HttpDelete("{slotId}")]
        public async Task<IActionResult> DeleteSlot(Guid slotId)
        {
            try
            {
                await _repo.DeleteSlotAsync(slotId);
                return Ok("Slot deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
