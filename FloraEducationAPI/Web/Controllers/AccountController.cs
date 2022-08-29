using FloraEducationAPI.Domain.DTO.Authentication;
using FloraEducationAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IJWTManagerService jWTManagerService;
        private readonly IUserService userService;

        public AccountController(IJWTManagerService jWTManagerService, IUserService userService)
        {
            this.jWTManagerService = jWTManagerService;
            this.userService = userService;
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
