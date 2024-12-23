using composition.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        /// <summary>
        /// Log in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest data)
        {
            // send request to Authorization Service to authenticate
            // return (accessToken in response and set-(httpOnly)cookie header with refreshToken) in response

            return Ok();
        }
        /// <summary>
        /// Get new access and refresh tokens
        /// </summary>
        /// <returns></returns>
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
