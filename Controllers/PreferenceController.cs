using CairoGo.DTOs.PreferenceDTO;
using CairoGo.Mappings;
using CairoGo.Models.Entity;
using CairoGo.Repository.Implementations;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceController : Controller
    {
        private readonly IPreferenceRepo preferenceRepo;

        public PreferenceController(IPreferenceRepo preferenceRepo)
        {
            this.preferenceRepo = preferenceRepo;
        }
        // api/preference/create
        [HttpPost("create")]
        public async Task<IActionResult> CreatePreferenceProfile([FromBody] CreatePreferenceDto dto)
        {
            try
            {
                if(!ModelState.IsValid)return BadRequest(ModelState);
                if(await preferenceRepo.HasProfileAsync(dto.UserId))
                    return BadRequest("User already has a preference profile.");
                // Map DTO → Entity
                var profile = PreferenceMapper.ToEntity(dto);
                // Add activities
                await preferenceRepo.SetActivityTypesAsync(profile, dto.ActivityTypeIds);
                await preferenceRepo.AddAsync(profile);
                // Map Entity → Response
                var response = PreferenceMapper.ToResponse(profile);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        // api/preference/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProfile(Guid userId)
        {
            try
            {
                var profile = await preferenceRepo.GetPreferencesByUserIdAsync(userId);
                if (profile == null)
                    return NotFound("Preference profile not found.");
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        // api/preference/{userId}/activity/{activityId}
        [HttpPost("{userId}/activity/{activityId}")]
        public async Task<IActionResult> AddActivity(Guid userId, Guid activityId)
        {
            try
            {
                await preferenceRepo.AddActivityTypeAsync(userId, activityId);
                return Ok("Activity type added to preference profile.");
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        // api/preference/{profileId}/activity/{activityId}
        [HttpDelete("{profileId}/activity/{activityId}")]
        public async Task<IActionResult> RemoveActivity(Guid profileId, Guid activityId)
        {
            try
            {
                await preferenceRepo.RemoveActivityTypeAsync(profileId, activityId);
                return Ok("Activity type removed from preference profile.");
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        // api/preference/{profileId}/activities
        [HttpGet("{profileId}/activities")]
        public async Task<IActionResult> GetActivities(Guid profileId)
        {
            try
            {
                var activities = await preferenceRepo.GetActivityTypesAsync(profileId);
                return Ok(activities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
