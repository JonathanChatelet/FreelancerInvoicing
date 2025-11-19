using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Models.Interfaces;
using FreelancerInvoicing.Repositories.Repositories;

namespace FreelancerInvoicing.Services.Users
{
    public class UsersService : BaseService<User>, IUsersService
    {
        public UsersService(IObjectRepository<User> userRepository) : base(userRepository)
        {
        }
    }
}
