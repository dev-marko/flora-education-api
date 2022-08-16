using FloraEducationAPI.Domain.DTO.Authentication;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Implementations
{
    public class JWTManagerService : IJWTManagerService
    {
        public readonly IUserService userService;
        private readonly IConfiguration configuration;

        public JWTManagerService(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        public Tokens GenerateToken(User user)
        {
            var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname)
            };

            var token = new JwtSecurityToken
                (
                    configuration["JWT:Issuer"],
                    configuration["JWT:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: credentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
