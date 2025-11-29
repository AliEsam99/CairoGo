using CairoGo.DTOs.ActivityTypeDTO;
using CairoGo.Mappings;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityTypeController : ControllerBase
    {
        private readonly IActivityTypeRepo _repo;

        public ActivityTypeController(IActivityTypeRepo repo)
        {
            _repo = repo;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateActivityType([FromBody] ActivityTypeCreateDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var activity = dto.ToEntity();
            await _repo.AddAsync(activity);
            return Ok(activity.ToDto());
        }
        [HttpPost("update/{typeId}")]
        public async Task<IActionResult> UpdateActivityType(Guid typeId, [FromBody] ActivityTypeUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var activity = await _repo.GetByIdAsync(typeId);
            if (activity == null) return NotFound("Activity type not found.");
            activity.UpdateEntity(dto);
            await _repo.UpdateAsync(activity);
            return Ok(activity.ToDto());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await _repo.GetAllActivityTypeAsync();
            return Ok(types);
        }
        [HttpDelete("{typeId}")]
        public async Task<IActionResult> DeleteType(Guid typeId)
        {
            try
            {
                await _repo.DeleteActivittyTypeAsync(typeId);
                return Ok("Activity type deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
