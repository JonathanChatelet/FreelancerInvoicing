using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreelancerInvoicing.DTO.Users;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Services.Interfaces;
using System.Drawing.Text;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FreelancerInvoicing.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        private int currentUserId => int.Parse(User.FindFirstValue("id") ?? "0");
        private bool currentUserIsAdmin => bool.Parse(User.FindFirstValue("isAdmin") ?? "false");


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadUserDTO>>> GetAllAsync()
        {
            if (!currentUserIsAdmin)
            {
                return StatusCode(403, "You must be an admin");
            }
            IEnumerable<User> users = await _userService.GetAllObjectServiceAsync();
            return !users.Any() ? NotFound() : Ok(_mapper.Map<IEnumerable<ReadUserDTO>>(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUserDTO>> GetByIdAsync(int id)
        {
            if (!currentUserIsAdmin)
            {
                return StatusCode(403, "You must be an admin");
            }
            User user = await _userService.GetObjectByIdServiceAsync(id);
            return user == null ? NotFound() : Ok(_mapper.Map<ReadUserDTO>(user));
        }

        [HttpGet("by_email")]
        public async Task<ActionResult<ReadUserDTO>> GetByEmailAsync([FromQuery] String email)
        {
            if (!currentUserIsAdmin)
            {
                return StatusCode(403, "You must be an admin");
            }
            User user = await _userService.FindUserByEmailServiceAsync(email);
            return user == null ? NotFound() : Ok(_mapper.Map<ReadUserDTO>(user));
        }

        [HttpGet("by_siret")]
        public async Task<ActionResult<ReadUserDTO>> GetBySiretAsync([FromQuery] String siret)
        {
            if (!currentUserIsAdmin)
            {
                return StatusCode(403, "You must be an admin");
            }
            User user = await _userService.FindUserBySiretServiceAsync(siret);
            return user == null ? NotFound() : Ok(_mapper.Map<ReadUserDTO>(user));
        }

        [HttpGet("by_name")]
        public async Task<ActionResult<IEnumerable<ReadUserDTO>>> GetByNameAsync([FromQuery] String name)
        {
            if (!currentUserIsAdmin)
            {
                return StatusCode(403, "You must be an admin");
            }
            IEnumerable<User> users = await _userService.FindUsersByNameServiceAsync(name);
            return !users.Any() ? NotFound() : Ok(_mapper.Map<IEnumerable<ReadUserDTO>>(users));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto createUserDTO)
        {
            User user = await _userService.CreateUserAsync(createUserDTO.Email, createUserDTO.Password, createUserDTO.Siret);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = user.UserId }, _mapper.Map<ReadUserDTO>(user));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            if (!currentUserIsAdmin)
            {
                return StatusCode(403, "You must be an admin");
            }
            User user = await _userService.GetObjectByIdServiceAsync(id);
            if (user == null) 
            {
                return NotFound();
            }
            _mapper.Map(updateUserDTO, user);
            await _userService.ModifyUserServiceAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!currentUserIsAdmin)
            {
                return StatusCode(403, "You must be an admin");
            }
            bool result = await _userService.DeletObjectServiceAsync(id);
            return result == false ? NotFound() : NoContent();
        }

        [HttpGet("me")]
        public async Task<ActionResult<ReadMyInfoDTO>> GetMyInfoAsync()
        {
            User user = await _userService.GetObjectByIdServiceAsync(currentUserId);
            return user == null ? NotFound() : Ok(_mapper.Map<ReadMyInfoDTO>(user));
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyInfoAsync([FromBody] UpdateMyInfoDTO updateMyInfoDTO)
        {
            User user = await _userService.GetObjectByIdServiceAsync(currentUserId);
            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(updateMyInfoDTO, user);
            await _userService.ModifyUserServiceAsync(user);
            return NoContent();
        }
    }
}
