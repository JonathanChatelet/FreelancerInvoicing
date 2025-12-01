using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Repositories;
using FreelancerInvoicing.Repositories.Interfaces;
using FreelancerInvoicing.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace FreelancerInvoicing.Services.Users
{
    public class UsersService : BaseService<User>, IUsersService
    {
        private IUserRepository _userRepository;
        private IAuthentificationService _authentificationService;
        private readonly PasswordHasher<User> _passwordHasher = new();
        public UsersService(IUserRepository userRepository, IAuthentificationService authentificationService) : base(userRepository) 
        {
            _userRepository = userRepository;
            _authentificationService = authentificationService;
        }

        public async Task<User> FindUserByEmailServiceAsync(String email)
        {
            User result;
            result = await _userRepository.FindUserByEmailAsync(email);
            return result;
        }
        public async Task<User> FindUserBySiretServiceAsync(String siret)
        {
            User result;
            result = await _userRepository.FindUserBySiretAsync(siret);
            return result;
        }

        public async Task<IEnumerable<User>> FindUsersByNameServiceAsync(String name)
        {
            IEnumerable<User> results;
            results = await _userRepository.FindUsersByNameAsync(name);
            return results;
        }

        public async Task<User> CreateUserAsync(String email, String password)
        {
            await VerifyIfEmailIsCorrectAsync(email);
            VerifyIfPasswordIsCorrect(password);
            User user = new User();
            user.Email = email;
            user.PasswordHash = _authentificationService.HashPassword(user, password);
            await _userRepository.AddObjectAsync(user);
            return user;
        }
        private async Task VerifyIfEmailIsCorrectAsync(String email)
        {
            string emailPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$";
            if (email == null)
            {
                throw new InvalidOperationException("Email can't be null.");
            }
            else if (!Regex.IsMatch(email, emailPattern))
            {
                throw new InvalidOperationException("Please insert a correct email adress.");
            }
            else if (await _userRepository.FindUserByEmailAsync(email) != null)
            {
                throw new InvalidOperationException("This email adress is already used.");
            }
        }

        private void VerifyIfPasswordIsCorrect(String password) 
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                throw new InvalidOperationException("Password is to short.");
            }
            else if (password.All(char.IsLetterOrDigit))
            {
                throw new InvalidOperationException("Password must contain at least one special caracter.");
            }
            else if (password.Any(c => char.IsWhiteSpace(c)))
            {
                throw new InvalidOperationException("Space(s) are forbidden in password.");
            }
        }
    }
}
