using CairoGo.DTOs.PlaceVibeDTO;
using CairoGo.Mappings;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceVibeTagController : ControllerBase
    {
        private readonly IPlaceVibeTagRepo placeVibeTagRepo;

        public PlaceVibeTagController(IPlaceVibeTagRepo repo)
        {
            placeVibeTagRepo = repo;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePlaceVibeTagDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = PlaceVibeTagMapper.ToEntity(dto);
            await placeVibeTagRepo.AddAsync(entity);
            return Ok(PlaceVibeTagMapper.ToResponse(entity));
        }
        [HttpGet("place/{placeId}")]
        public async Task<IActionResult> GetByPlace(Guid placeId)
        {
            var list = await placeVibeTagRepo.GetByPlaceIdAsync(placeId);
            return Ok(list.Select(PlaceVibeTagMapper.ToResponse));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePlaceVibeTagDto dto)
        {
            var tag = await placeVibeTagRepo.GetByIdAsync(id);
            if (tag == null)
                return NotFound("Tag not found");

            PlaceVibeTagMapper.ApplyUpdate(tag, dto);
            await placeVibeTagRepo.UpdateAsync(tag);

            return Ok(PlaceVibeTagMapper.ToResponse(tag));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await placeVibeTagRepo.DeleteAsync(id);
                return Ok("Deleted successfully");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Tag with ID {id} not found");
            }
        }
    }
}
