using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Enums;

namespace DataAccess.Entities
{
    public class RealEstate : BaseEntity
    {
        public RealEstateType Type { get; set; }
        public int? RoomCount { get; set; }
        public float? UsableSurfaceArea { get; set; }
        public int? Floor { get; set; }
        public int? ConstructionYear { get; set; }
        public string? Address { get; set; }
    }
}
