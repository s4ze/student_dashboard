using composition.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet]
        [Route("user/{UserId}")]
        public IActionResult GetUser([FromRoute] string userId)
        {
            // send request to Authorization and Profile Services to get credentials and personal data of user with this userId
            // return (user data (except password)) in response

            return Ok();
        }
        [HttpPut]
        [Route("user/{userId}")]
        public IActionResult EditUser([FromRoute] string userId, [FromBody] EditUserRequest data)
        {
            // check what type of data request has - credentials or/and personal
            // send request to according to types of data to Authorization/Profile Service to edit user with this userId
            // return (user data (except password)) in response

            return Ok();
        }
        [HttpDelete]
        [Route("user/{userId}")]
        public IActionResult RemoveUser([FromRoute] string userId)
        {
            // send request to Authorization and Profile Service to validate Admin role of sender or if it's the same user with this userId
            // no data in response

            return Ok();
        }
        [HttpDelete]
        [Route("user")]
        public IActionResult RemoveUsersRange([FromBody] List<string> userIds)
        {
            // send request to Authorization Service to validate Admin role of sender and remove users with those userIds
            // send request to Profile Service to remove users with those userIds
            // return 200 if deleted >= 1 user
            // return 400 if deleted < 1 user
            // return 403 if not admin
            // return (userIds of deleted users) in response

            return Ok();
        }
        [HttpPost]
        [Route("setrole/{userId}")]
        public IActionResult SetRole([FromRoute] string userId, [FromBody] string role)
        {
            // send request to Authorization Service to validate Admin role of sender and set role to user with this userId
            // return (user, role) in response

            return Ok();
        }
    }
}
