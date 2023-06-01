using DataAccess.Entities;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class RepositoryBase<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<T?> Get(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public void Add(T entity)
        {
            _dbSet.Attach(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        protected IQueryable<T> GetRecords()
        {
            return _dbSet.AsQueryable<T>();
        }
    }
}
