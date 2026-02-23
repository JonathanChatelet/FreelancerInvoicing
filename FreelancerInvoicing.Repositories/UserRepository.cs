using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Repositories.Context;
using FreelancerInvoicing.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Repositories
{
    public class UserRepository : ObjectRepository<User>, IUserRepository
    {
        private readonly IMapper _mapper;
        public UserRepository(FreelancerInvoicingDbContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public async Task<User?> FindUserByEmailAsync (String email) 
        {
            return await _dbSet.SingleOrDefaultAsync(user => user.Email == email);
        }
        public async Task<User?> FindUserBySiretAsync(String siret)
        {
            return await _dbSet.SingleOrDefaultAsync(user => user.Siret == siret);
        }
        public async Task<IEnumerable<User>> FindUsersByNameAsync(String name)
        {
            return await _dbSet.Where(u => EF.Functions.Like(u.Name.ToLower(), $"%{name.ToLower()}%")).ToListAsync();
        }
        public async Task<bool> ModifyUserAsync(User user)
        {
            User? existingUser = await GetObjectByIdAsync(user.UserId);
            if(existingUser == null) 
            {
                return false;
            }
            _mapper.Map(user, existingUser);
            //_dbSet.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
