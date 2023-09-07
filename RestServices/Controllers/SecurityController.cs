using Microsoft.AspNetCore.Mvc;
using REST.Config;

namespace REST.Controllers
{
    [ApiController]
    public class SecurityController : ControllerBase
    {
        IConfiguration config;
        SecurityService service;
        public SecurityController(IConfiguration _config, SecurityService service)
        {
            config = _config;
            this.service = service;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(AppUserCredentalsModel model)
        {
            TokenAndRole? tokenAndRole = service.AuthenticateUserAndGetToken(model);
            if (tokenAndRole == null)
            {
                return BadRequest("Invalid UserName or Password..");
            }
            else
                return Ok(tokenAndRole);
        }
    }
}