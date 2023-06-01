using DataAccess.Entities;
using DataLayer;

namespace DataAccess.Repositories;

public class AnnouncementRepository : RepositoryBase<Announcement>
{
    public AnnouncementRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}