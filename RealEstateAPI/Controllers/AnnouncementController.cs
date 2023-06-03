﻿using Core.Dtos;
using Core.Services;
using DataAccess.Enums;
using Infrastructure.Exceptions;
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var results = await _announcementService.GetFeed();

            return Ok(results);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
	        var result = await _announcementService.GetDetailedAnnouncement(id);

	        return Ok(result);
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
        [AllowAnonymous]
        public async Task<IActionResult> GetComments([FromRoute] Guid id)
        {
	        var comments = await _announcementService.GetComments(id);

	        return Ok(comments);
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddComment([FromRoute] Guid id, [FromBody] AddCommentDto commentDto)
        {
			Guid.TryParse(User.FindFirst("userId")?.Value, out var userId);

			var announcement = await _announcementService.GetAnnouncement(id);

            var commentId = await _announcementService.AddComment(commentDto, announcement, userId);

            return Ok(new {Id = commentId});
        }

        [HttpDelete("{id}/comments/{commentId}")]
        public IActionResult DeleteComment()
        {
            throw new NotImplementedException();
        }
    }
}
