using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaceLockAuth.API.Controllers
{
    [ApiController]
    [Route("api/secure")]
    public class SecureController : Controller
    {   
        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var email = User.Claims
                .FirstOrDefault(c => c.Type == "email" || c.Type.EndsWith("/emailaddress"))?.Value;

            var fullName = User.Claims
                .FirstOrDefault(c => c.Type == "fullname")?.Value;

            return Ok(new
            {
                message = "You are authorized 🎉",
                email,
                fullName
            });
        }
    }
}
