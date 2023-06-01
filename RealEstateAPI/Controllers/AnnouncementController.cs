using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DataAccess.Enums;
using System;
using Infrastructure.Exceptions;

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
        [AllowAnonymous]
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
        [Authorize(Roles = "User,Administrator")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == null)
                return BadRequest();

            var announcement = await _announcementService.GetAnnouncement(id);

            if (role == UserRole.User.ToString())
            {
                Guid.TryParse(User.FindFirst("userId")?.Value, out var userId);

                if (announcement.Poster.Id != userId)
                    throw new ForbiddenException($"Unauthorized to delete announcement with id {id}");
            }

            await _announcementService.DeleteAnnouncement(announcement);

            return Ok();
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
