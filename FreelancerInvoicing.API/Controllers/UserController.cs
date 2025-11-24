using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Services.Interfaces;

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
            IEnumerable<User> users = await _userService.GetAllServiceAsync();
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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            ActionResult<User> result;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            await _userService.AddObjectServiceAsync(user);

            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (user == null || user.UserId != id)
                return BadRequest();

            var existingUser = await _userService.GetObjectByIdServiceAsync(id);
            if (existingUser == null)
                return NotFound();

            await _userService.ModifyObjectServiceAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetObjectByIdServiceAsync(id);
            if (user == null)
                return NotFound();

            await _userService.DeletObjectServiceAsync(id);
            return NoContent();
        }
    }
}
