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

namespace FreelancerInvoicing.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadUserDTO>>> GetAllAsync()
        {
            if (await CurrentUserIsAdminAsync())
            {
                ActionResult<IEnumerable<ReadUserDTO>> result;
                try
                {
                    IEnumerable<User> users = await _userService.GetAllObjectServiceAsync();

                    if (!users.Any())
                    {
                        result = NotFound("No users found");
                    }
                    else
                    {
                        var readUsers = _mapper.Map<IEnumerable<ReadUserDTO>>(users);
                        result = Ok(readUsers);
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { error = ex.Message });
                }
            }
            else
            {
                return Forbid("You must be an admin");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUserDTO>> GetByIdAsync(int id)
        {
            if (await CurrentUserIsAdminAsync())
            {
                ActionResult<ReadUserDTO> result;
                try
                {
                    User user = await _userService.GetObjectByIdServiceAsync(id);

                    if (user == null)
                    {
                        result = NotFound();
                    }
                    else
                    {
                        result = Ok(_mapper.Map<ReadUserDTO>(user));
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erreur serveur : {ex.Message}");
                }
            }
            else
            {
                return Forbid("You must be an admin");
            }
        }
    
        [HttpGet("{email}")]
        public async Task<ActionResult<ReadUserDTO>> GetByEmailAsync(String email)
        {
            if (await CurrentUserIsAdminAsync())
            {
                ActionResult<ReadUserDTO> result;
                try
                {
                    User user = await _userService.FindUserByEmailServiceAsync(email);
                    if (user == null)
                    {
                        result = NotFound();
                    }
                    else
                    {
                        result = Ok(_mapper.Map<ReadUserDTO>(user));
                    }
                    return result;
                }
                catch (InvalidOperationException iOpEx)
                {
                    result = Conflict(iOpEx.Message);
                    return result;
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erreur serveur : {ex.Message}");
                }
            }
            else
            {
                return Forbid("You must be an admin");
            }
        }

        [HttpGet("{siret}")]
        public async Task<ActionResult<ReadUserDTO>> GetBySiretAsync(String siret)
        {
            if (await CurrentUserIsAdminAsync())
            {
                ActionResult<ReadUserDTO> result;
                try
                {
                    User user = await _userService.FindUserBySiretServiceAsync(siret);
                    if (user == null)
                    {
                        result = NotFound();
                    }
                    else
                    {
                        result = Ok(_mapper.Map<ReadUserDTO>(user));
                    }
                    return result;
                }
                catch (InvalidOperationException iOpEx)
                {
                    return Conflict(iOpEx.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erreur serveur : {ex.Message}");
                }
            }
            else
            {
                return Forbid("You must be an admin");
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<ReadUserDTO>>> GetByNameAsync(String name)
        {
            if (await CurrentUserIsAdminAsync())
            {
                ActionResult<IEnumerable<ReadUserDTO>> result;
                try
                {
                    IEnumerable<User> users = await _userService.FindUsersByNameServiceAsync(name);

                    if (!users.Any())
                    {
                        result = NotFound("No users found");
                    }
                    else
                    {
                        IEnumerable<ReadUserDTO> readUsers = _mapper.Map<IEnumerable<ReadUserDTO>>(users);
                        result = Ok(readUsers);
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { error = ex.Message });
                }
            }
            else
            {
                return Forbid("You must be an admin");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto createUserDTO)
        {
            ActionResult result;
            try
            {
                if (createUserDTO == null)
                {
                    result = BadRequest();
                }
                else
                {
                    await _userService.AddObjectServiceAsync(_mapper.Map<User>(createUserDTO));
                    result =  CreatedAtAction(nameof(GetByIdAsync), new { id = createUserDTO.UserId }, createUserDTO);
                }
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            if (await CurrentUserIsAdminAsync())
            {
                ActionResult result;
                try
                {
                    if (updateUserDTO == null)
                    { 
                        result = BadRequest();
                    }
                    else
                    {
                        User existingUser = await _userService.GetObjectByIdServiceAsync(id);
                        if (existingUser == null)
                        {
                            result = NotFound();
                        }
                        else
                        {
                            await _userService.ModifyObjectServiceAsync(_mapper.Map<User>(updateUserDTO));
                            result = NoContent();
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erreur serveur : {ex.Message}");
                }
            }
            else
            {
                return Forbid("You must be an admin");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await CurrentUserIsAdminAsync())
            {
                ActionResult result;
                try
                {
                    var user = await _userService.GetObjectByIdServiceAsync(id);
                    if (user == null)
                    {
                        result = NotFound();
                    }
                    else
                    {
                        await _userService.DeletObjectServiceAsync(id);
                        result = NoContent();
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erreur serveur : {ex.Message}");
                }
            }
            else
            {
                return Forbid("You must be an admin");
            }
        }

        [HttpPut]
        public async Task<IActionResult> SoftDeleteAsync(int id)
        {
            if (await CurrentUserIsAdminAsync())
            {
                ActionResult result;
                try
                {
                    var user = await _userService.GetObjectByIdServiceAsync(id);
                    if (user == null)
                    {
                        result = NotFound();
                    }
                    else
                    {
                        user.IsDeleted = true;
                        await _userService.ModifyObjectServiceAsync(user);
                        result = NoContent();
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erreur serveur : {ex.Message}");
                }
            }
            else
            {
                return Forbid("You must be an admin");
            }
        }

        [HttpGet]
        private async Task<ActionResult<ReadUserDTO>> GetMyInfoAsync() 
        {
            ActionResult<ReadUserDTO> result;
            User? user = await GetCurrentUserAsync();
            if(user == null)
            {
                result = NotFound("No users connected");
            }
            else
            {
                ReadUserDTO readUser = _mapper.Map<ReadUserDTO>(user);
                result = Ok(readUser);
            }
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMyInfoAsync([FromBody] UpdateUserDTO updateUserDTO)
        {
            ActionResult result;
            int currentId = (await GetCurrentUserAsync()).UserId;
            try
            {
                if (updateUserDTO == null)
                {
                    result = BadRequest();
                }
                else
                {
                    await _userService.ModifyObjectServiceAsync(_mapper.Map<User>(updateUserDTO));
                    result = NoContent();
                }
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
            return result;
        }

        private async Task<User?> GetCurrentUserAsync() 
        {
            User user;
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                user = null;
            }
            else
            {
                user = await _userService.GetObjectByIdServiceAsync(userId.Value);
            }
            return user;
        }

        private async Task<bool> CurrentUserIsAdminAsync()
        {
            User? currentUser = await GetCurrentUserAsync();
            bool result;
            if (currentUser == null)
            {
                result = false;
            }
            else
            {
                result = currentUser.IsAdmin;
            }
            return result;
        }

    }
}
