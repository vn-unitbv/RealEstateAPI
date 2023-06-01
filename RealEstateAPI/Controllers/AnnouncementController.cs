using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project.Controllers
{
	[ApiController]
	[Route("announcement")]
	[Authorize]
	public class AnnouncementController : ControllerBase
	{
		private readonly AnnouncementService _announcementService;

		public AnnouncementController(AnnouncementService announcementService)
		{
			_announcementService = announcementService;
		}

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
		[Authorize(Roles = "User")]
		public async Task<IActionResult> Add([FromBody] AddAnnouncementDto data)
		{
			Guid.TryParse(User.FindFirst("userId")?.Value, out var userId);

			var id = await _announcementService.AddAnnouncement(data, userId);

			return Ok(new { announcementId = id });
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

		[HttpGet("{id}/comments")]
		public IActionResult GetComments()
		{
			throw new NotImplementedException();
		}

		[HttpPost("{id}/comments")]
		public IActionResult AddComment()
		{
			throw new NotImplementedException();
		}

		[HttpDelete("{id}/comments/{commentId}")]
		public IActionResult DeleteComment()
		{
			throw new NotImplementedException();
		}
	}
}
