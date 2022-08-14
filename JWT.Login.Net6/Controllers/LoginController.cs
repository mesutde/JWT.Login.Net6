using JWT.Login.Net6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Login.Net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("uSer Not found");
        }

        private string Generate(UserModel user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Email,user.EmailAdress),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.Role,user.Role),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],

                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(UserLogin userLogin)
        {
            var currentuser = UserConstants.Users.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower()
            && o.Password.ToLower() == userLogin.Password.ToLower());

            if (currentuser != null)
            {
                return currentuser;
            }

            return null;
        }
    }
}