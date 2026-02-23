using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Repositories.Interfaces;
using FreelancerInvoicing.Models.Entities;
using Microsoft.EntityFrameworkCore;
using FreelancerInvoicing.DTO.Users;

namespace FreelancerInvoicing.Services.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<User?> FindUserByEmailServiceAsync(String email);
        Task<User?> FindUserBySiretServiceAsync(String siret);
        Task<IEnumerable<User>> FindUsersByNameServiceAsync(String name);
        Task<bool> ModifyUserServiceAsync(User user);
        Task<User> CreateUserAsync(String email, String password, string siret);
    }
}
