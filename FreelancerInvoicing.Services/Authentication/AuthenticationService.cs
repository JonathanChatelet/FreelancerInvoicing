using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using FreelancerInvoicing.Tools.Settings;
using IAuthenticationServiceAlias = FreelancerInvoicing.Services.Interfaces.IAuthenticationService;
using FreelancerInvoicing.Services.Interfaces;


namespace FreelancerInvoicing.Services.Authentication
{
    public class AuthenticationService : IAuthenticationServiceAlias
    {
        private readonly IUserReadOnlyService _userService; 
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JWTSettings _jwt;

        public AuthenticationService(IUserReadOnlyService userService,IPasswordHasher<User> passwordHasher, IOptions<JWTSettings> jwtOptions)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _jwt = jwtOptions.Value;
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

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("siret", user.Siret),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwt.Key)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiresInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
