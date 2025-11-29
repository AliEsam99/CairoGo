using CairoGo.DTOs.PreferenceSignalDTO;
using CairoGo.Mappings;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferenceSignalController : ControllerBase
    {
        private readonly IUserPreferenceSignalRepository _repo;

        public UserPreferenceSignalController(IUserPreferenceSignalRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity.ToDto());
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var list = await _repo.GetByUserAsync(userId);
            return Ok(list.Select(s => s.ToDto()));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserPreferenceSignalDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // if unique constraint exists, try to upsert: update existing or create new
            var existing = await _repo.GetByUserTypeKeyAsync(dto.UserId, dto.SignalType, dto.Key);
            if (existing != null)
            {
                // optional policy: update weight/observation instead of creating duplicate
                existing.Weight = dto.Weight;
                existing.ObservationCount += 1;
                existing.LastUpdated = DateTime.UtcNow;
                var updated = await _repo.UpdateAsync(existing);
                return Ok(updated.ToDto());
            }
            var entity = dto.ToEntity();
            var created = await _repo.AddAsync(entity);
            return Ok(created.ToDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserPreferenceSignalDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return NotFound();

            entity.ApplyUpdate(dto);
            var updated = await _repo.UpdateAsync(entity);
            return Ok(updated.ToDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _repo.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
