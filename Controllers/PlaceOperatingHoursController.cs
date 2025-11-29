using CairoGo.DTOs.PlaceOperatingHoursDTO;
using CairoGo.Mappings;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceOperatingHoursController : ControllerBase
    {
        private readonly IPlaceOperatingHoursRepo repo;
        public PlaceOperatingHoursController(IPlaceOperatingHoursRepo repo)
        {
            this.repo = repo;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePlaceOperatingHoursDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exists = await repo.GetByPlaceAndDayAsync(dto.PlaceId, dto.DayOfWeek);
            if (exists != null)
                return BadRequest("Operating hours for this day already exist (unique constraint).");

            var entity = PlaceOperatingHoursMapper.ToEntity(dto);
            await repo.AddAsync(entity);

            return Ok(PlaceOperatingHoursMapper.ToResponse(entity));
        }
        [HttpGet("place/{placeId}")]
        public async Task<IActionResult> GetByPlace(Guid placeId)
        {
            var list = await repo.GetByPlaceIdAsync(placeId);
            return Ok(list.Select(PlaceOperatingHoursMapper.ToResponse));
        }
        [HttpGet("place/{placeId}/day/{dayOfWeek}")]
        public async Task<IActionResult> GetByPlaceAndDay(Guid placeId, DayOfWeek dayOfWeek)
        {
            var hours = await repo.GetByPlaceAndDayAsync(placeId, dayOfWeek);
            if (hours == null)
                return NotFound($"No operating hours found for {dayOfWeek}");

            return Ok(PlaceOperatingHoursMapper.ToResponse(hours));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePlaceOperatingHoursDto dto)
        {
            var hours = await repo.GetByIdAsync(id);
            if (hours == null)
                return NotFound("Operating hours not found.");
            PlaceOperatingHoursMapper.ApplyUpdate(hours, dto);
            await repo.UpdateAsync(hours);

            return Ok(PlaceOperatingHoursMapper.ToResponse(hours));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await repo.DeleteAsync(id);
                return NoContent(); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Operating hours with ID {id} not found");
            }
        }
    }
}
