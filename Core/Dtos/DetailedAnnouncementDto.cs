using DataAccess.Entities;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
	public class DetailedAnnouncementDto
	{
		public string TransactionType { get; set; }

		#region RealEstate
		public string RealEstateType { get; set; }
		public int? RoomCount { get; set; }
		public float? UsableSurfaceArea { get; set; }
		public int? Floor { get; set; }
		public int? ConstructionYear { get; set; }
		public string? Address { get; set; }
		#endregion

		public decimal? Price { get; set; }
		public Guid PosterId { get; set; }
		public string PosterName { get; set; }
		public string PosterEmail { get; set; }
		public string CreatedDate { get; set; }
		public string ModifiedDate { get; set; }
		public string PostTitle { get; set; }
		public string PostDescription { get; set; }

		// TODO: Add comments here or in another controller??

		//public virtual IEnumerable<Comment> Comments { get; set; }
	}
}
