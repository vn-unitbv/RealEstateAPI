using Core.Dtos;
using Core.Extensions;
using DataAccess.Entities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
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
        public async Task<List<FeedAnnouncementDto>> GetFeed(AnnouncementFilterDto filter, AnnouncementSortDto? sort, int? page)
        {
            var results = await _unitOfWork.Announcements.GetAll() as IEnumerable<Announcement>;

            if (filter.TransactionType.HasValue)
                results = results.Where(a => a.TransactionType == filter.TransactionType);
            if (filter.MaxPrice.HasValue)
                results = results.Where(a => a.Price <= filter.MaxPrice.Value);
            if (filter.MinPrice.HasValue)
                results = results.Where(a => a.Price >= filter.MinPrice.Value);
            if(filter.Type.HasValue)
                results = results.Where(a => a.RealEstate.Type == filter.Type);
            if (filter.MinUsableSurfaceArea.HasValue)
                results = results.Where(a => a.RealEstate.UsableSurfaceArea >= filter.MinUsableSurfaceArea.Value);
            if (filter.MaxUsableSurfaceArea.HasValue)
                results = results.Where(a => a.RealEstate.UsableSurfaceArea <= filter.MaxUsableSurfaceArea.Value);
            if (filter.MaxRoomCount.HasValue)
                results = results.Where(a => a.RealEstate.RoomCount <= filter.MaxRoomCount);
            if (filter.MinRoomCount.HasValue)
                results = results.Where(a => a.RealEstate.RoomCount >= filter.MinRoomCount);

            if (sort?.SortCriterion != null)
            {
                switch (sort.SortCriterion)
                {
                case AnnouncementSortCriterion.Price:
                    results = results.OrderBy(a => a.Price);
                    break;
                case AnnouncementSortCriterion.ConstructionYear:
                    results = results.OrderBy(a => a.RealEstate.ConstructionYear);
                    break;
                case AnnouncementSortCriterion.RoomCount:
                    results = results.OrderBy(a => a.RealEstate.RoomCount);
                    break;
                case AnnouncementSortCriterion.UsableSurfaceArea:
                    results = results.OrderBy(a => a.RealEstate.UsableSurfaceArea);
                    break;
                case AnnouncementSortCriterion.CreatedDate:
                    results = results.OrderBy(a => a.CreatedDate);
                    break;
                default:
                    break;
                }

                if (sort.SortDirection == SortDirection.Descending)
                    results = results.Reverse();
            }

            if (page != null)
            {
                if (page <= 0)
                    page = 1;
                const int pageSize = 3;
                results = results.Skip(((int)page - 1) * pageSize).Take(pageSize);
            }

            return results.ToFeedAnnouncementDtos().ToList();
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
            announcement.ModifiedDate = DateTime.UtcNow;
            if (data.Price != null)
                announcement.Price = data.Price;
            if (data.PostTitle != null)
                announcement.PostTitle = data.PostTitle;
            if (data.PostDescription != null)
                announcement.PostDescription = data.PostDescription;

            _unitOfWork.Announcements.Update(announcement);
            await _unitOfWork.SaveChangesAsync();
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
