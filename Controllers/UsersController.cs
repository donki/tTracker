using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using tTrackerWeb.Database;
using tTrackerWeb.Helpers;
using tTrackerWeb.Model;
using tTrackerWeb.Services;

namespace tTrackerWeb.Controllers
{
    [Route("api/users")]
    [ApiController]
    [TypeFilter(typeof(TokenAuthorizationFilter))]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(SqliteDbContext dbContext)
        {
            userService = new UserService(dbContext); // Instancia de la API de usuarios
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            List<User> users = userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            List<User> users = userService.GetUsers();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            userService.AddUser(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest();
            }

            var existingUser = userService.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Id = updatedUser.Id;

            userService.UpdateUser(existingUser);

            return Ok(existingUser);
        }

    }
}
