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
using FreelancerInvoicing.Tools.Exceptions;

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
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new BusinessException("Email can't be null or empty.", 400);
            }
            else if (!Regex.IsMatch(email, emailPattern))
            {
                throw new BusinessException("Please insert a correct email address.", 400);
            }
            else if (await _userRepository.FindUserByEmailAsync(email) != null)
            {
                throw new BusinessException("This email adress is already used.", 409);
            }
        }

        private void VerifyIfPasswordIsCorrect(String password) 
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                throw new BusinessException("Password is too short.",400);
            }
            else if (password.All(char.IsLetterOrDigit))
            {
                throw new BusinessException("Password must contain at least one special caracter.", 400);
            }
            else if (password.Any(c => char.IsWhiteSpace(c)))
            {
                throw new BusinessException("Space(s) are forbidden in password.", 400);
            }
        }

        private async Task VerifySiretAsync(String siret)
        {
            if (string.IsNullOrWhiteSpace(siret))
            {
                throw new BusinessException("Siret can't be null or empty.", 400);
            }
            else if (siret.Length != 14)
            {
                throw new BusinessException("Siret must contain 14 characters", 400);
            }
            else if (await _userRepository.FindUserBySiretAsync(siret) != null)
            {
                throw new BusinessException("This Siret is already used.", 409);
            }
        }
    }
}
