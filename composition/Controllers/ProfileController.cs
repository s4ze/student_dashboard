using composition.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        /// <summary>
        /// Get user's data
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/{UserId}")]
        public IActionResult GetUser([FromRoute] string userId)
        {
            // send request to Authorization and Profile Services to get credentials and personal data of user with this userId
            // return (user data (except password)) in response

            return Ok();
        }
        /// <summary>
        /// Register an account
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterRequest data)
        {
            // send request to Authorization Service to create new account
            // return (userId and email) variable in response

            return Ok();
        }
        /// <summary>
        /// Edit user data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("user/{userId}")]
        public IActionResult EditUser([FromRoute] string userId, [FromBody] EditUserRequest data)
        {
            // check what type of data request has - credentials or/and personal
            // send request to according to types of data to Authorization/Profile Service to edit user with this userId
            // return (user data (except password)) in response

            return Ok();
        }
        /// <summary>
        /// Remove user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("user/{userId}")]
        public IActionResult RemoveUser([FromRoute] string userId)
        {
            // send request to Authorization and Profile Service to validate Admin role of sender or if it's the same user with this userId
            // no data in response

            return Ok();
        }
        /// <summary>
        /// Remove list of users
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Set a role for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
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
