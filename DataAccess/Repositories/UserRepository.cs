using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataLayer;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _dbSet.FirstOrDefaultAsync(e => e.Email == email);
            if (user == null)
                throw new ResourceMissingException("There is no user with the given email address");
            
            return user;
        }
    }
}
