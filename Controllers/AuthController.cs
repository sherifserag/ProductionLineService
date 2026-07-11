using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductionLineService.Services;

namespace ProductionLineService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ILoginService _loginService;

		public AuthController(ILoginService loginService)
		{
			_loginService = loginService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			try
			{
				string token = await _loginService.LoginAsync(request.Username, request.Password);
				return Ok(new { token = token });
			}
			catch (UnauthorizedAccessException ex)
			{
				return Unauthorized(new { message = ex.Message });
			}
		}
	}

	public class LoginRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}