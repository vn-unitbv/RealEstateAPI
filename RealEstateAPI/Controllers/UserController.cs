using Core.Services;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var user = await _userService.GetUserById(id);

            return Ok(user);
        }


        /// <summary>
        /// This function retrieves all the announcements of the logged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet("announcements")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAnnouncements()
        {
			Guid.TryParse(User.FindFirst("userId")?.Value, out var userId);

			var announcements = await _userService.GetAnnouncements(userId);

            return Ok(announcements);
        }

        [HttpGet("{id}/comments")]
        public IActionResult GetComments()
        {
            throw new NotImplementedException();
        }
    }
}
