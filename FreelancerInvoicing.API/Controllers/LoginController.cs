using AutoMapper;
using FreelancerInvoicing.DTO.Users;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerInvoicing.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;

        public LoginController(IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            User? user = await _userService.FindUserByEmailServiceAsync(loginDTO.Email);
            if (user == null || !_authService.VerifyPassword(user, loginDTO.Password))
                return Unauthorized("Invalid email or password");

            var token = _authService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
