using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Services.Users;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            ActionResult<User> result;
            User user = await _userService.GetObjByIdService(id);
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

            await _userService.AddObjService(user);

            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (user == null || user.UserId != id)
                return BadRequest();

            var existingUser = await _userService.GetObjByIdService(id);
            if (existingUser == null)
                return NotFound();

            await _userService.ModifyObjService(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetObjByIdService(id);
            if (user == null)
                return NotFound();

            await _userService.DeletObjService(id);
            return NoContent();
        }
    }
}
