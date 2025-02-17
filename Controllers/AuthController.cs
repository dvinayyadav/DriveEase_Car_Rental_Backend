using Car_Rental_Backend_Application.Data.RequestDto_s;
using Car_Rental_Backend_Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Backend_Application.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        // ✅ USER REGISTRATION ENDPOINT
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(registerDto);
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequestLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Login(loginDto);
            return result;
        }

    }
}
