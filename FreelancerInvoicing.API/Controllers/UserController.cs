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

namespace FreelancerInvoicing.API.Controllers
{
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
        public async Task<ActionResult<IEnumerable<ReadUserDTO>>> GetAll()
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUserDTO>> GetById(int id)
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

        [HttpGet("{email}")]
        public async Task<ActionResult<ReadUserDTO>> GetByEmail(String email)
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

        [HttpGet("{siret}")]
        public async Task<ActionResult<ReadUserDTO>> GetBySiret(String siret)
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

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<ReadUserDTO>>> GetByName(String name)
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDTO)
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
                    result =  CreatedAtAction(nameof(GetById), new { id = createUserDTO.UserId }, createUserDTO);
                }
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDTO updateUserDTO)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userService.GetObjectByIdServiceAsync(id);
                if (user == null)
                    return NotFound();

                await _userService.DeletObjectServiceAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }
        
    }
}
