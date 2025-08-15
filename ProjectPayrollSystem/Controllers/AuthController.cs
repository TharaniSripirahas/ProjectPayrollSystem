using AuthService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.DTOs;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid login request");

            var result = await _authService.LoginAsync(request);

            return StatusCode(result.ResponseCode == 1 ? 200 : 400, result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid registration request");

            var result = await _authService.RegisterAsync(request);

            return StatusCode(result.ResponseCode == 1 ? 200 : 400, result);
        }

    }
}
