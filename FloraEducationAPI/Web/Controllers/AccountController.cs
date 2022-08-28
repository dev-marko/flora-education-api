using FloraEducationAPI.Domain.DTO.Authentication;
using FloraEducationAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IJWTManagerService jWTManagerService;
        private readonly IUserService userService;

        public AccountController(IConfiguration configuration, IJWTManagerService jWTManagerService, IUserService userService)
        {
            this.configuration = configuration;
            this.jWTManagerService = jWTManagerService;
            this.userService = userService;
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult LoginPage()
        {
            return Ok("Login page");
        }

        [HttpPost("login")]
        public IActionResult Login([Bind("Username, Password")] UserLoginDTO userLoginDTO)
        {
            var user = userService.Authenticate(userLoginDTO);

            if (user != null)
            {
                var token = jWTManagerService.GenerateToken(user);
                return Ok(token.Token);
            }

            return NotFound("User not found");
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            
            if (userService.UserExists(userRegisterDTO))
            {
                return BadRequest("User already exists");
            }

            return Ok(userService.Register(userRegisterDTO));
        }
    }
}
