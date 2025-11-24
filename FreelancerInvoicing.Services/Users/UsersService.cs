using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Repositories;
using FreelancerInvoicing.Repositories.Interfaces;
using FreelancerInvoicing.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Services.Users
{
    public class UsersService : BaseService<User>, IUsersService
    {
        private IUserRepository _userRepository;
        public UsersService(IUserRepository userRepository) : base(userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<User> FindUserByEmailServiceAsync(String email)
        {
            User result;
            result = await _userRepository.FindUserByEmailAsync(email);
            return result;
        }
        public async Task<User> FindUserBySiretServiceAsync(String siret)
        {
            User result;
            result = await _userRepository.FindUserBySiretAsync(siret);
            return result;
        }

        public async Task<IEnumerable<User>> FindUsersByNameServiceAsync(String name)
        {
            IEnumerable<User> results;
            results = await _userRepository.FindUsersByNameAsync(name);
            return results;
        }
    }
}
