using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace FreelancerInvoicing.Services.Authentification
{
    public class AuthentificationService
    {
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthentificationService(PasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string enteredPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, enteredPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
