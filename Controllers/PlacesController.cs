using CairoGo.Models.ENums;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceRepository _placeRepository;

        public PlacesController(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }
        [HttpGet]
        // api/places
        public async Task<IActionResult> GetAllPlaces()
        {
            try
            {
                var places = await _placeRepository.GetAllAsync();
                return Ok(places);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        // api/places/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlacesById(Guid id)
        {
            try
            {
                var place = await _placeRepository.GetByIdAsync(id);
                if (place == null)
                {
                    return NotFound();
                }
                return Ok(place);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
       // api/places/details/{id}
        [HttpGet("details/{id}")]
        public IActionResult GetPlaceDetails(Guid id)
        {
            try
            {
                var place = _placeRepository.GetDetailsOfPlaceAsync(id);
                if (place == null)
                    return NotFound();
                return Ok(place);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        // api/places/search?name=pyramids
        [HttpGet("search")]

        public async Task<IActionResult> SearchByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest("Name parmeter is requierd");
                name = name.Trim().ToLower();
                var place = await _placeRepository.SearchByNameAsync(name);
                if (place == null)
                    return NotFound();
                return Ok(place);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        // api/places/district?name=Cairo
        [HttpGet("distract")]
        public async Task<IActionResult> SearchByDistrict(string district)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(district))
                    return BadRequest("Distract Name Is requierd");
                district = district.Trim().ToLower();
                var place = await _placeRepository.SearchByDistractAsync(district);
                if (place == null)
                    return NotFound();
                return Ok(place);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        // api/places/vibe?name=Romantic
        [HttpGet("vibe")]
        public async Task<IActionResult> SearchByVibe(TravelVibe vibe)
        {
            try
            {
                var places = await _placeRepository.SearchByVibesAsync(vibe);
                return Ok(places);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // api/places/category?category=Museum
        [HttpGet("category")]
        public async Task<IActionResult> SearchByCategory(PlaceCategory category)
        {
            try
            {
                var places = await _placeRepository.SearchByCategoryAsync(category);
                return Ok(places);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        //api/places/cost?tier=Budget
        [HttpGet("cost")]
        public async Task<IActionResult> SearchByCost(CostTier cost)
        {
            try
            {
                var places = await _placeRepository.SearchByCostTierAsync(cost);
                return Ok(places);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        //api/places/trending?count=10
        [HttpGet("trending")]
        public async Task<IActionResult> GetTrending(int count = 10)
        {
            try
            {
                var places = _placeRepository.GetTrendingPlacesAsync(count);
                return Ok(places);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        // GET: api/places/weather?pref=Indoor
        [HttpGet("weather")]
        public async Task<IActionResult> SearchByWeather(WeatherPref pref)
        {
            try
            {
                var places = await _placeRepository.SearchByWeatherPref(pref);
                return Ok(places);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
