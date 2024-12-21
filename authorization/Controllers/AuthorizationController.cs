using authorization.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginRequest data)
        {
            // send request to Authorization Service to authenticate
            // return (accessToken in response and set-(httpOnly)cookie header with refreshToken) in response

            return Ok();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterRequest data)
        {
            // send request to Authorization Service to create new account
            // return (userId and email) variable in response

            return Ok();
        }
        [HttpGet]
        [Route("refresh")]
        public IActionResult RefreshToken()
        {
            // take refreshToken from cookie
            // send request to Authorization Service to validate and get new token
            // return (accessToken in response and set-(httpOnly)cookie header with refreshToken) in response

            return Ok();
        }
    }
}
