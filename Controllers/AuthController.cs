using CairoGo.DTOs.UserDTO;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CairoGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtTokenRepository _jwtRepo;
        private readonly IConfiguration _config;

        public AuthController(
            IUserRepository userRepo,
            IJwtTokenRepository jwtRepo,
            IConfiguration config)
        {
            _userRepo = userRepo;
            _jwtRepo = jwtRepo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });

            // Check if email already exists
            var existingUser = await _userRepo.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Registration failed",
                    Errors = new List<string> { "Email already registered" }
                });

            var user = new UserApplication
            {
                Email = dto.Email,
                UserName = dto.Email,
                FullName = dto.FullName,
                Age = dto.Age,
                Address = dto.Address,
                JoinDate = DateTime.UtcNow
            };

            var result = await _userRepo.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Registration failed",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "User registered successfully",
                Data = new { UserId = user.Id, Email = user.Email }
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid email or password"
                });

            var valid = await _userRepo.CheckPasswordAsync(user, dto.Password);
            if (!valid)
                return Unauthorized(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid email or password"
                });

            var token = _jwtRepo.GenerateToken(user);
            var expiresInMinutes = int.Parse(_config["Jwt:DurationInMinutes"] ?? "120");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Login successful",
                Data = new
                {
                    Token = token,
                    ExpiresIn = expiresInMinutes * 60, // in seconds
                    User = new
                    {
                        user.Id,
                        user.FullName,
                        user.Email
                    }
                }
            });
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Invalid or expired token"
                    });

                var user = await _userRepo.GetByIdAsync(Guid.Parse(userId));

                if (user == null)
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "User not found"
                    });

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "User retrieved successfully",
                    Data = new
                    {
                        user.Id,
                        user.FullName,
                        user.Email,
                        user.Age,
                        user.Address,
                        user.JoinDate
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving user data",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var user = await _userRepo.GetByIdAsync(Guid.Parse(userId));

            if (user == null)
                return Unauthorized(new ApiResponse<object>
                {
                    Success = false,
                    Message = "User not found"
                });

            var token = _jwtRepo.GenerateToken(user);
            var expiresInMinutes = int.Parse(_config["Jwt:DurationInMinutes"] ?? "120");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Token refreshed successfully",
                Data = new
                {
                    Token = token,
                    ExpiresIn = expiresInMinutes * 60
                }
            });
        }
    }
}
