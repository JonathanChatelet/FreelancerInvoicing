using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Repositories.Interfaces;
using FreelancerInvoicing.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Services.Interfaces
{
    public interface IUsersService : IBaseService<User>
    {
        Task<User> FindUserByEmailServiceAsync(String email);
        Task<User> FindUserBySiretServiceAsync(String siret);
        Task<IEnumerable<User>> FindUsersByNameServiceAsync(String name);
    }
}
