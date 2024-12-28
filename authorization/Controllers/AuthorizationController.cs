using System.IdentityModel.Tokens.Jwt;
using authorization.Contracts;
using authorization.Data;
using authorization.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController(AuthorizationService authorizationService, AuthenticationService authenticationService) : ControllerBase
    {
        private readonly AuthorizationService _authorizationService = authorizationService;
        private readonly AuthenticationService _authenticationService = authenticationService;
        [HttpPost]
        [Route("refresh")]
        public IActionResult RefreshToken([FromBody] RefreshRequest data)
        {
            if (data.RefreshToken != null && _authorizationService.ValidateToken(data.RefreshToken))
            {
                var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(data.RefreshToken);

                var userId = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.UserIdClaimName).Value;
                var user = _authenticationService.GetUserById(new Guid(userId));

                return Ok(new RefreshResponse()
                {
                    User = new RefreshResponse.EditedUser
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.Role,
                        PhotoUrl = user.PhotoUrl,
                        Contact = user.Contact,
                        Group = user.Group,
                        CreatedAt = user.CreatedAt
                    },
                    AccessToken = _authorizationService.GenerateToken(new Guid(userId), user.Role),
                    RefreshToken = _authorizationService.GenerateToken(new Guid(userId), user.Role, true)
                });
            }

            return Unauthorized();
        }
        [HttpGet]
        [Route("validaterefresh")]
        public IActionResult ValidateRefreshToken()
        {
            try
            {
                Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
                if (refreshToken != null)
                {
                    var result = _authorizationService.ValidateToken(refreshToken);
                    if (result) return Ok();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("validateaccess")]
        public IActionResult ValidateAccessToken()
        {
            try
            {
                var accessToken = Request.Headers.Authorization[0][7..];
                if (accessToken != null)
                {
                    var result = _authorizationService.ValidateToken(accessToken);
                    if (result) return Ok();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("role/{userId}")]
        public IActionResult GetRole([FromRoute] string userId)
        {
            try
            {
                if (_authenticationService.CheckIfUserExistsById(new Guid(userId)))
                {
                    var user = _authenticationService.GetUserById(new Guid(userId));
                    return Ok(new RoleResponse()
                    {
                        Role = user.Role
                    });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
