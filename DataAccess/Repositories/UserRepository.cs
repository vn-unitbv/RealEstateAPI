using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataLayer;

namespace DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
