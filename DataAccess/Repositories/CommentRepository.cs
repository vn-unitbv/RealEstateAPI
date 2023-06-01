using DataAccess.Entities;
using DataLayer;

namespace DataAccess.Repositories;

public class CommentRepository : RepositoryBase<Announcement>
{
    public CommentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}