using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;

namespace FreelancerInvoicing.Repositories.Interfaces
{
    public interface IUserRepository : IObjectRepository<User>
    {
        Task<User> FindUserByEmailAsync(String email);
        Task<User> FindUserBySiretAsync(String siret);
        Task<IEnumerable<User>> FindUsersByNameAsync(String name);
    }
}
