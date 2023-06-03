using Core.Dtos;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Extensions
{
    internal static class AnnouncementMappingExtensions
    {
        public static Announcement ToAnnouncement(this AddAnnouncementDto data)
        {
            Announcement announcement = new()
            {
                TransactionType = data.TransactionType,
                RealEstate = data.RealEstate,
                Price = data.Price,
                PostTitle = data.PostTitle,
                PostDescription = data.PostDescription,
            };
            return announcement;
        }

        public static FeedAnnouncementDto ToFeedAnnouncementDto(this Announcement announcement)
        {
            FeedAnnouncementDto dto = new()
            {
                TransactionType = announcement.TransactionType,
                RealEstateType = announcement.RealEstate.Type,
                RoomCount = announcement.RealEstate.RoomCount,
                UsableSurfaceArea = announcement.RealEstate.UsableSurfaceArea,
                Floor = announcement.RealEstate.Floor,
                ConstructionYear = announcement.RealEstate.ConstructionYear,
                Address = announcement.RealEstate.Address,
                Price = announcement.Price,
                PostTitle = announcement.PostTitle,
                PosterName = announcement.Poster.FirstName + " " + announcement.Poster.LastName,
                PosterId = announcement.PosterId,
                Id = announcement.Id
            };
            return dto;
        }

        public static IEnumerable<FeedAnnouncementDto> ToFeedAnnouncementDtos(
            this IEnumerable<Announcement> announcements)
        {
            return announcements.Select(e => e.ToFeedAnnouncementDto());
        }

        public static DetailedAnnouncementDto ToDetailedAnnouncementDto(this Announcement announcement)
        {
	        DetailedAnnouncementDto dto = new()
	        {
		        TransactionType = announcement.TransactionType.ToString(),
		        RealEstateType = announcement.RealEstate.Type.ToString(),
		        RoomCount = announcement.RealEstate.RoomCount,
		        UsableSurfaceArea = announcement.RealEstate.UsableSurfaceArea,
		        Floor = announcement.RealEstate.Floor,
		        ConstructionYear = announcement.RealEstate.ConstructionYear,
		        Address = announcement.RealEstate.Address,
		        Price = announcement.Price,
		        PostTitle = announcement.PostTitle,
		        PostDescription = announcement.PostDescription,
		        CreatedDate = announcement.CreatedDate.ToString("MM/dd/yyyy h:mm tt"),
		        ModifiedDate = announcement.ModifiedDate.ToString("MM/dd/yyyy h:mm tt"),
		        PosterId = announcement.PosterId,
		        PosterName = announcement.Poster.FirstName
		                     + " " + announcement.Poster.LastName,
		        PosterEmail = announcement.Poster.Email
	        };

            return dto;
        }
    }
}
