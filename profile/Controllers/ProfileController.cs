using System.Net;
using Microsoft.AspNetCore.Mvc;
using messages.Contracts;
using messages.Services;

namespace messages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController(ProfileService profileService) : ControllerBase
    {
        private readonly ProfileService _profileService = profileService;
        [HttpGet]
        [Route("user/{userId}")]
        public IActionResult GetUser([FromRoute] string userId)
        {
            try
            {
                if (_profileService.CheckIfUserExists(new Guid(userId)))
                {
                    var user = _profileService.GetUser(new Guid(userId));
                    var result = new
                    {
                        user.Email,
                        user.FirstName,
                        user.LastName,
                        user.Role,
                        user.PhotoUrl,
                        user.Contact,
                        user.Group,
                        user.CreatedAt
                    };
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] CreateUserRequest data)
        {
            try
            {
                var user = _profileService.CreateUser(data);
                if (user == null) return Conflict("Email already taken");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("user/{userId}")]
        public IActionResult EditUser([FromRoute] string userId, [FromBody] EditUserRequest data)
        {
            try
            {
                if (_profileService.CheckIfUserExists(new Guid(userId)))
                {
                    var user = _profileService.EditUser(new Guid(userId), data);
                    return Ok(user);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("user/{userId}")]
        public IActionResult RemoveUser([FromRoute] string userId)
        {
            try
            {
                if (_profileService.CheckIfUserExists(new Guid(userId)))
                {
                    _profileService.RemoveUser(new Guid(userId));
                    return Ok();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("userexists/{userId}")]
        public IActionResult CheckIfUserExists([FromRoute] string userId)
        {
            try
            {
                return Ok(_profileService.CheckIfUserExists(new Guid(userId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
