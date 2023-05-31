using DataAccess.Enums;

namespace DataAccess.Entities
{
    public class Announcement : BaseEntity
    {
        public TransactionType TransactionType { get; set; }
        public RealEstate RealEstate { get; set; }
        public decimal? Price { get; set; }
        public User Poster { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
