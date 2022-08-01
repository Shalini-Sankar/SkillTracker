using Microsoft.AspNetCore.Mvc;
using SkillTrackerIdentity.API.Services;
using SkillTrackerIdentity.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SkillTrackerIdentity.API.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService Service)
        {
            _userService = Service;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            var users = _userService.GetUser(user.UserName, user.Password);

            if (users == null)
                return Ok(new { Token = "", Usertype = "0" });
            else
            {
                var claims = new[] { new Claim(ClaimTypes.Name, users.UserName) };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretTokenForJwt"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token), User = users.UserName, Usertype = users.Role });
            }
        }


    }
}
