using DataAccess.Entities;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DataAccess.Repositories;

public class CommentRepository : RepositoryBase<Comment>
{
    public CommentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Comment>> GetCommentsByAnnouncement(Guid announcementId)
    {
	    var results = await _dbSet
		    .Where(c => c.Announcement.Id == announcementId)
		    .ToListAsync();

	    return results;
    }
}