using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerData)
        {
            await _userService.Register(registerData);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            throw new NotImplementedException();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            throw new NotImplementedException();
        }
    }
}
