using DataAccess.Entities;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
	public class AddAnnouncementDto
	{
		public TransactionType TransactionType { get; set; }
		public RealEstate RealEstate { get; set; }
		public decimal? Price { get; set; }
		public string PostTitle { get; set; }
		public string PostDescription { get; set; }
	}
}
