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
using FreelancerInvoicing.DTO.Users;
using AutoMapper;

namespace FreelancerInvoicing.Services.Users
{
    public class UserService : BaseService<User>, IUserService, IUserReadOnlyService
    {
        private IUserRepository _userRepository;
        private IAuthenticationService _authentificationService;
        protected readonly IMapper _mapper;
        private readonly PasswordHasher<User> _passwordHasher = new();
        public UserService(IUserRepository userRepository, IAuthenticationService authentificationService, IMapper mapper) : base(userRepository) 
        {
            _userRepository = userRepository;
            _authentificationService = authentificationService;
            _mapper = mapper;
        }

        public async Task<User?> FindUserByEmailServiceAsync(String email)
        {
            return await _userRepository.FindUserByEmailAsync(email);
        }
        public async Task<User?> FindUserBySiretServiceAsync(String siret)
        {
            return await _userRepository.FindUserBySiretAsync(siret); 
        }

        public async Task<IEnumerable<User>> FindUsersByNameServiceAsync(String name)
        {
            IEnumerable<User> results;
            results = await _userRepository.FindUsersByNameAsync(name);
            return results;
        }

        public async Task<bool> ModifyUserServiceAsync(User user)
        {
            User? userRead = await _userRepository.GetObjectByIdAsync(user.UserId);
            if (userRead == null)
            {
                return false;
            }

            return await _userRepository.ModifyUserAsync(user);
        }

        public async Task<User> CreateUserAsync(String email, String password, String siret)
        {
            await VerifyIfEmailIsCorrectAsync(email);
            VerifyIfPasswordIsCorrect(password);
            await VerifySiretAsync(siret);
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

        private async Task VerifySiretAsync(String siret)
        {
            if(siret.Length != 14)
            {
                throw new InvalidOperationException("Siret must contain 14 caracters");
            }
            else if (await _userRepository.FindUserBySiretAsync(siret) != null)
            {
                throw new InvalidOperationException("This Siret is already used.");
            }
        }
    }
}
