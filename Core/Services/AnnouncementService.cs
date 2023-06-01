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

namespace Core.Services
{
	public class AnnouncementService
	{
		private readonly UnitOfWork _unitOfWork;

		public AnnouncementService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
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

        public async Task<Announcement> GetAnnouncement(Guid id)
        {
            var announcement = await _unitOfWork.Announcements.Get(id);

            if (announcement == null)
                throw new ResourceMissingException($"Announcement with id {id} not found");

			return announcement;
        }
	}
}
