using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Services.Interfaces;
using System.Drawing.Text;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FreelancerInvoicing.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll(int id)
        {
            ActionResult<IEnumerable<User>> result;
            try
            {
                IEnumerable<User> users = await _userService.GetAllObjectServiceAsync();
                if (users == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(users);
                }

                return result;
            }
            catch (Exception ex)
            {
                result = StatusCode(500, $"Erreur serveur : {ex.Message}");
                return result;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            ActionResult<User> result;
            try
            {
                User user = await _userService.GetObjectByIdServiceAsync(id);
                if (user == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(user);
                }

                return result;
            }
            catch (Exception ex)
            {
                result = StatusCode(500, $"Erreur serveur : {ex.Message}");
                return result;
            }
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetByEmail(String email)
        {
            ActionResult<User> result;
            try
            {
                User user = await _userService.FindUserByEmailServiceAsync(email);
                if (user == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(user);
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
                result = StatusCode(500, $"Erreur serveur : {ex.Message}");
                return result;
            }
        }

        [HttpGet("{siret}")]
        public async Task<ActionResult<User>> GetBySiret(String siret)
        {
            ActionResult<User> result;
            try
            {
                User user = await _userService.FindUserBySiretServiceAsync(siret);
                if (user == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(user);
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
                result = StatusCode(500, $"Erreur serveur : {ex.Message}");
                return result;
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<User>>> GetByName(String name)
        {
            ActionResult<IEnumerable<User>> result;
            try
            {
                IEnumerable<User> users = await _userService.FindUsersByNameServiceAsync(name);
                if (!users.Any())
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(users);
                }
                
                return result;

            }
            catch (Exception ex)
            {
                result = StatusCode(500, $"Erreur serveur : {ex.Message}");
                return result;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                await _userService.AddObjectServiceAsync(user);

                return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            try
            {
                if (user == null || user.UserId != id)
                    return BadRequest();

                var existingUser = await _userService.GetObjectByIdServiceAsync(id);
                if (existingUser == null)
                    return NotFound();

                await _userService.ModifyObjectServiceAsync(user);
                return NoContent();
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
