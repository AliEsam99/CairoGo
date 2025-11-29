using CairoGo.DTOs.ExperimentAssignmentDTO;
using CairoGo.Mappings;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperimentAssignmentController : ControllerBase
    {
        private readonly IExperimentAssignmentRepo _repo;

        public ExperimentAssignmentController(IExperimentAssignmentRepo repo)
        {
            _repo = repo;
        }
        [HttpPost("assign")]
        public async Task<IActionResult> AssignExperiment([FromBody] ExperimentAssignmentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var assignment = dto.ToEntity();
            await _repo.AddAsync(assignment);
            return Ok(assignment.ToDto());
        }
        [HttpPut("update/{assignmentId}")]
        public async Task<IActionResult> Update(Guid assignmentId, [FromBody] ExperimentAssignmentUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(assignmentId);
            if (entity == null)
                return NotFound("Experiment assignment not found");
            entity.UpdateEntity(dto);
            await _repo.UpdateAsync(entity);
            return Ok(entity.ToDto());
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserAssignments(Guid userId)
        {
            var list = await _repo.GetAssignmentsByUserIdAsync(userId);
            return Ok(list);
        }
        [HttpDelete("{assignmentId}")]
        public async Task<IActionResult> Delete(Guid assignmentId)
        {
            try
            {
                await _repo.DeleteAsync(assignmentId);
                return Ok("Assignment deleted.");
            }
            catch
            {
                return NotFound("Assignment not found.");
            }
        }
    }
}
