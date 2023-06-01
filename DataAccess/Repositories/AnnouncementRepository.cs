using DataAccess.Entities;
using DataAccess;

namespace DataAccess.Repositories;

public class AnnouncementRepository : RepositoryBase<Announcement>
{
    public AnnouncementRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}