using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            throw new NotImplementedException();
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
