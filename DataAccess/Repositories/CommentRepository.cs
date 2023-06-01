using DataAccess.Entities;
using DataAccess;

namespace DataAccess.Repositories;

public class CommentRepository : RepositoryBase<Announcement>
{
    public CommentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}