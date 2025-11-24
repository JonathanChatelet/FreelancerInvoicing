using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Repositories
{
    public class UserRepository : ObjectRepository<User>, IUserRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(DbContext context) : base(context) 
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public async Task<User> FindUserByEmailAsync (String email) 
        {
            User result;
            try
            {
                result = await _dbSet.SingleOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException($"error : many users with same email ({email}) in the database");
            }
            return result;
        }
        public async Task<User> FindUserBySiretAsync(String siret)
        {
            User result;
            try
            {
                result = await _dbSet.SingleOrDefaultAsync(user => user.Siret.ToLower() == siret.ToLower());
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"error : many users with same SIRET ({siret}) in the database");
            }
            return result;
        }

        public async Task<IEnumerable<User>> FindUsersByNameAsync(String name)
        {
            IEnumerable<User> results;
            
            results = await _dbSet.Where(user => user.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            
            return results;
        }
    }

}
