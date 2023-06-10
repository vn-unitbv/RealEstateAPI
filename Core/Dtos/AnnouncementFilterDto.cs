using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Dtos
{
    public class AnnouncementFilterDto
    {
        public TransactionType? TransactionType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public RealEstateType? Type { get; set; }
        public int? MinRoomCount { get; set; }
        public int? MaxRoomCount { get; set; }
        public int? MinUsableSurfaceArea { get; set; }
        public int? MaxUsableSurfaceArea { get; set; }
    }
}