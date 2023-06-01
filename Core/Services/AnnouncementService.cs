using Core.Dtos;
using Core.Extensions;
using DataAccess.Entities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
