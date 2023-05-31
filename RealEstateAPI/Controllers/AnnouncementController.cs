using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("announcement")]
    public class AnnouncementController : ControllerBase
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

        [HttpPost]
        public IActionResult Add()
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{id}")]
        public IActionResult Update()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}
