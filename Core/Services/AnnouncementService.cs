using Core.Dtos;
using Core.Extensions;
using DataAccess.Entities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Core.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Services
{
	public class AnnouncementService
	{
		private readonly UnitOfWork _unitOfWork;

		public AnnouncementService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
        public async Task<List<FeedAnnouncementDto>> GetFeed(AnnouncementFilterDto filter)
        {
            var results = (await _unitOfWork.Announcements.GetAll())
                .ToFeedAnnouncementDtos()
                .ToList();

            if (filter.TransactionType.HasValue)
            {
                results = (List<FeedAnnouncementDto>)results.Where(a => a.TransactionType == filter.TransactionType);
            }
            if (filter.MaxPrice.HasValue)
            {
                results = (List<FeedAnnouncementDto>)results.Where(a => a.Price <= filter.MaxPrice.Value);
            }
            if (filter.MinPrice.HasValue)
            {
                results = (List<FeedAnnouncementDto>)results.Where(a => a.Price >= filter.MinPrice.Value);
            }
            if (filter.MinUsableSurfaceArea.HasValue)
            {
                results = (List<FeedAnnouncementDto>)results.Where(a => a.UsableSurfaceArea >= filter.MinUsableSurfaceArea.Value);
            }
            if (filter.MaxUsableSurfaceArea.HasValue)
            {
                results = (List<FeedAnnouncementDto>)results.Where(a => a.UsableSurfaceArea <= filter.MaxUsableSurfaceArea.Value);
            }
            if (filter.MaxRoomCount.HasValue)
            {
                results = (List<FeedAnnouncementDto>)results.Where(a => a.RoomCount <= filter.MaxRoomCount);
            }
            if (filter.MinRoomCount.HasValue)
            {
                results = (List<FeedAnnouncementDto>)results.Where(a => a.RoomCount >= filter.MinRoomCount);
            }
            return results;
        }

		public async Task<Guid> AddAnnouncement(AddAnnouncementDto data, Guid userId)
		{
			var announcement = data.ToAnnouncement();
			announcement.Poster = new User() { Id = userId };
			var currentDateTime = DateTime.UtcNow;
			announcement.CreatedDate = currentDateTime;
			announcement.ModifiedDate = currentDateTime;
			announcement.RealEstate.Id = new Guid();

			_unitOfWork.Announcements.Add(announcement);
			await _unitOfWork.SaveChangesAsync();

			return announcement.Id;
		}

        public async Task DeleteAnnouncement(Guid id)
        {
            Announcement announcement = new() { Id = id };
            await DeleteAnnouncement(announcement);
        }

        public async Task DeleteAnnouncement(Announcement announcement)
        {
            try
            {
                _unitOfWork.Announcements.Delete(announcement);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ResourceMissingException($"Announcement with id {announcement.Id} not found");
            }
        }
        public async Task UpdateAnnouncement(Guid id, UpdateAnnouncementDto data)
        {
            Announcement announcement = await GetAnnouncement(id);
            var updatedAnnouncement = data.ToAnnouncement();
            updatedAnnouncement.Poster = announcement.Poster;
            updatedAnnouncement.ModifiedDate = DateTime.UtcNow;
            updatedAnnouncement.CreatedDate = announcement.CreatedDate;
            updatedAnnouncement.Id = id;
            if (updatedAnnouncement.Price == 0)
            {
                updatedAnnouncement.Price = announcement.Price;
            }
            if (updatedAnnouncement.PostTitle == "string")
            {
                updatedAnnouncement.PostTitle = announcement.PostTitle;
            }
            if (updatedAnnouncement.PostDescription == "string")
            {
                updatedAnnouncement.PostDescription = announcement.PostDescription;
            }
            try
            {
                _unitOfWork.Announcements.Update(updatedAnnouncement);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ResourceMissingException($"Announcement with id {id} not found");
            }
        }
        public async Task<Announcement> GetAnnouncement(Guid id)
        {
            var announcement = await _unitOfWork.Announcements.Get(id);

            if (announcement == null)
                throw new ResourceMissingException($"Announcement with id {id} not found");

			return announcement;
        }

        public async Task<DetailedAnnouncementDto> GetDetailedAnnouncement(Guid id)
        {
	        var announcement = (await GetAnnouncement(id)).ToDetailedAnnouncementDto();

            return announcement;
        }

        public async Task<Guid> AddComment(AddCommentDto commentDto, Announcement announcement, Guid userId)
        {
	        var comment = commentDto.ToComment();
	        comment.Poster = new User { Id = userId };
	        comment.Announcement = announcement;
	        var currentDateTime = DateTime.UtcNow;
	        comment.CreateDate = currentDateTime;

			_unitOfWork.Comments.Add(comment);
            await _unitOfWork.SaveChangesAsync();

            return comment.Id;
        }

        public async Task<List<CommentDto>> GetComments(Guid id)
        {
            // I am calling this function so that it throws if no announcement is found
	        var announcement = await GetAnnouncement(id);

	        var results = (await _unitOfWork.Comments.GetCommentsByAnnouncement(id))
		        .ToCommentDtos()
		        .ToList();

            return results;
        }
	}
}
