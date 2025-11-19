using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;

namespace FreelancerInvoicing.Services.Users
{
    public interface IUsersService : IBaseService<User>
    {
        Task<User> GetObjByIdService(int id);
        Task AddObjService(User user);
        Task ModifyObjService(User user);
        Task DeletObjService(int id);
    }
}
