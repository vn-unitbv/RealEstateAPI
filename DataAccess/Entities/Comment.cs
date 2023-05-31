using DataAccess.Enums;

namespace DataAccess.Entities
{
    public class Comment : BaseEntity
    {
        public virtual User? Poster { get; set; }
        public virtual Announcement Announcement { get; set; }
        public string Message { get; set; }
    }
}
