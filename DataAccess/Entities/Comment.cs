using DataAccess.Enums;

namespace DataAccess.Entities
{
    public class Comment : BaseEntity
    {
        public User Poster { get; set; }
        public Announcement Announcement { get; set; }
        public string Message { get; set; }
    }
}
