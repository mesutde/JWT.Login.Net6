using JWT.Login.Net6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWT.Login.Net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("Admins")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminsEndpoint()
        {
            var currentuser = GetCurrentuser();

            if (currentuser.Username != null)
            {
                return Ok($"hi {currentuser.Name}, you are an {currentuser.Role}");
            }

            return Unauthorized();
        }

        [HttpGet("Sellers")]
        [Authorize(Roles = "Seller")]
        public IActionResult SellersEndpoint()
        {
            var currentuser = GetCurrentuser();

            if (currentuser.Username != null)
            {
                return Ok($"hi {currentuser.Name}, you are  {currentuser.Role}");
            }

            return Unauthorized();
        }

        [HttpGet("AdminsAndSellers")]
        [Authorize(Roles = "Administrator,Seller")]
        public IActionResult AdminsAndSellersEndpoint()
        {
            var currentuser = GetCurrentuser();

            if (currentuser.Username != null)
            {
                return Ok($"hi {currentuser.Name}, you are  {currentuser.Role}");
            }

            return Unauthorized();
        }

        private UserModel GetCurrentuser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAdress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                };
            }

            return null;
        }
    }
}