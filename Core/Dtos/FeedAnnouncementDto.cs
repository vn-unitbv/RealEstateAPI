using DataAccess.Entities;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class FeedAnnouncementDto
    {
        public Guid Id { get; set; }
        #region RealEstate
        public RealEstateType RealEstateType { get; set; }
        public int? RoomCount { get; set; }
        public float? UsableSurfaceArea { get; set; }
        public int? Floor { get; set; }
        public int? ConstructionYear { get; set; }
        public string? Address { get; set; }
        #endregion
        public TransactionType TransactionType { get; set; }
        public decimal? Price { get; set; }
        public string PosterName { get; set; }
        public Guid PosterId { get; set; }
        public string PostTitle { get; set; }
    }
}
