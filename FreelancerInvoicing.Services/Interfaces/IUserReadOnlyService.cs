using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;

namespace FreelancerInvoicing.Services.Interfaces
{
    public interface IUserReadOnlyService
    {
        Task<User?> FindUserByEmailServiceAsync(String email);
    }
}
