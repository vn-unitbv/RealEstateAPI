using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/announcements")]
        public IActionResult GetAnnouncements()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/comments")]
        public IActionResult GetComments()
        {
            throw new NotImplementedException();
        }
    }
}
