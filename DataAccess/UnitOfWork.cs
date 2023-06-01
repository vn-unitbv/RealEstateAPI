
using DataAccess.Repositories;

namespace DataLayer
{
    public class UnitOfWork
    {
        public UserRepository Users { get; }
        public AnnouncementRepository Announcement { get; }
        public CommentRepository Comment { get; }

        private readonly AppDbContext _dbContext;

        public UnitOfWork
        (
            AppDbContext dbContext,
            UserRepository users,
            AnnouncementRepository announcement,
            CommentRepository comment
        )
        {
            _dbContext = dbContext;
            Users = users;
            Announcement = announcement;
            Comment = comment;
        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.Error.WriteLine(errorMessage);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                                   + $"{exception.Message}\n\n"
                                   + $"{exception.InnerException}\n\n"
                                   + $"{exception.StackTrace}\n\n";

                Console.Error.WriteLine(errorMessage);
            }
        }
    }
}
