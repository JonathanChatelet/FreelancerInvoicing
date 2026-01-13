using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Repositories.Interfaces;

namespace FreelancerInvoicing.Services.Interfaces
{
    public interface IAuthenticationService 
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string enteredPassword);
        string GenerateToken(User user);
    }
}
