using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Models.Interfaces;
using FreelancerInvoicing.Repositories.Repositories;
using FreelancerInvoicing.Services.Users.Tools;

namespace FreelancerInvoicing.Services.Users
{
    public class UsersService : BaseService<User>, IUsersService
    {
        private IObjectRepository<User> _userRepository;
        public UsersService(IObjectRepository<User> userRepository) : base(userRepository) 
        {
            _userRepository = userRepository;
        }
        public virtual async Task AddObjService(User user)
        {
            UserValidator.ValidateUserParameterMaxLength(user);
            UserValidator.ValidateIfUserEmailNotAlreadyUsed(await _userRepository.GetAllAsync(), user.Email);
            await _userRepository.AddObjAsync(user);
        }
        public virtual async Task ModifyObjService(User user)
        {
            UserValidator.ValidateUserParameterMaxLength(user);
            await _userRepository.ModifyObjAsync(user);
        }

        public async Task GetByEmail(String email)
        {
            User resultUser=null;
            IEnumerable<User> users = await _userRepository.GetAllAsync();
            foreach (User user in users) 
            {
                if (user.Email.Equals(email))
                {
                    resultUser = user;
                }
            }
            if (resultUser != null)
            {
                await _userRepository.GetObjByIdAsync(resultUser.UserId);
            }
            else
            {

            }
        }
    }
}
