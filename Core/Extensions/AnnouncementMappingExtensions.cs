using Core.Dtos;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
    }
}
