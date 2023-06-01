using DataAccess.Enums;

namespace DataAccess.Entities
{
    public class Announcement : BaseEntity
    {
        public TransactionType TransactionType { get; set; }
        public virtual RealEstate RealEstate { get; set; }
        public decimal? Price { get; set; }
        public virtual User Poster { get; set; }
        public Guid PosterId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
