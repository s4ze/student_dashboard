using Microsoft.AspNetCore.Mvc;
using profile.Contracts;
using profile.Services;

namespace profile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController(ProfileService profileService) : ControllerBase
    {
        private readonly ProfileService _profileService = profileService;
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterRequest data)
        {
            try
            {
                var result = _profileService.CreateUser(data);
                return result ? Ok() : Conflict("Email already taken");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("user/{userId}")]
        public IActionResult GetUser([FromRoute] string userId)
        {
            try
            {
                if (_profileService.CheckIfUserExists(new Guid(userId)))
                {
                    var user = _profileService.GetUser(new Guid(userId));
                    return Ok(new UserResponse()
                    {
                        UserId = user.UserId.ToString(),
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhotoUrl = user.PhotoUrl,
                        Contact = user.Contact,
                        Group = user.Group,
                        CreatedAt = user.CreatedAt
                    });
                }
                return NoContent();
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
                return Ok(new UserExistsResponse()
                {
                    UserExists = _profileService.CheckIfUserExists(new Guid(userId)),
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
