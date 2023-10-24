using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using tTrackerWeb.Database;
using tTrackerWeb.Helpers;
using tTrackerWeb.Model;
using tTrackerWeb.Services;

namespace tTrackerWeb.Controllers
{

      [Route("api/authentication")]
      [ApiController]
      public class AuthenticationController : ControllerBase
      {

        private TokenManager tokenManager;
        private UserService userService;

        public AuthenticationController(TokenManager tokenManager, SqliteDbContext dbContext)
        {
            this.tokenManager = tokenManager;
            this.userService = new UserService(dbContext);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (IsValidUser(loginRequest.Username, loginRequest.Password))
            {
                string authToken = tokenManager.GenerateToken(loginRequest.Username);

                return Ok(new { Token = authToken });
            }

            return Unauthorized();
        }

        private bool IsValidUser(string username, string password)
        {

          User user = userService.GetUserByUsername(username);

          if (user != null)
          {
              if (user.Password.Equals(password))
              {
                  return true; // Credenciales válidas
              }
          }
          return false;
        }
      }    
}
