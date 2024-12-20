using composition.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authorization : ControllerBase
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
        [HttpPost]
        [Route("setrole/{id}")]
        public IActionResult SetRole(UserIdRequest data)
        {
            // send request to Authorization Service to validate Admin role of sender and set role to user with this userId
            // return (role) in response

            return Ok();
        }
        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetUser(UserIdRequest data)
        {
            // send request to Authorization Service to validate Admin role of sender and set role to user with this userId
            // return (role) in response

            return Ok();
        }
        [HttpPut]
        [Route("user/{id}")]
        public IActionResult EditUser(EditUserRequest data)
        {
            // send request to Authorization Service to validate Admin role of sender and set role to user with this userId
            // return (role) in response

            return Ok();
        }
        [HttpDelete]
        [Route("user/{id}")]
        public IActionResult RemoveUser(UserIdRequest data)
        {
            // send request to Authorization Service to validate Admin role of sender and set role to user with this userId
            // return (role) in response

            return Ok();
        }
        [HttpDelete]
        [Route("user")]
        public IActionResult RemoveUsersRange(UserIdArrayRequest data)
        {
            // send request to Authorization Service to validate Admin role of sender and set role to user with this userId
            // return (role) in response

            return Ok();
        }
    }
}
