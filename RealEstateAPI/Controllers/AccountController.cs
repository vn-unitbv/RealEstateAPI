using Core.Dtos;
using Core.Services;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
	[ApiController]
	[Route("account")]
	[Authorize]
	public class AccountController : ControllerBase
	{
		private readonly UserService _userService;

		public AccountController(UserService userService)
		{
			_userService = userService;
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerData)
		{
			await _userService.Register(registerData);
			return Ok();
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginDto loginData)
		{
			var jwtToken = await _userService.Validate(loginData);

			if (jwtToken == null)
				throw new ForbiddenException("Invalid login credentials");

			return Ok(new { token = jwtToken });
		}

		[HttpPost("logout")]
		public IActionResult Logout()
		{
			throw new NotImplementedException();
		}
	}
}
