using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Repositories.Interfaces;
using FreelancerInvoicing.Models.Entities;

namespace FreelancerInvoicing.Services.Interfaces
{
    public interface IUsersService : IBaseService<User>
    {
        Task<IEnumerable<User>> GetAllServiceAsync();
        Task<User> GetObjectByIdServiceAsync(int id);
        Task AddObjectServiceAsync(User user);
        Task ModifyObjectServiceAsync(User user);
        Task DeletObjectServiceAsync(int id);
    }
}
