using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {

        [HttpPost("register")]
        public IActionResult Register()
        {
            throw new NotImplementedException();
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
