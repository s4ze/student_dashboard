using System.IdentityModel.Tokens.Jwt;
using authorization.Contracts;
using authorization.Data;
using authorization.Services;
using Microsoft.AspNetCore.Mvc;

namespace authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController(AuthorizationService authorizationService, AuthenticationService authenticationService) : ControllerBase
    {
        private readonly AuthorizationService _authorizationService = authorizationService;
        private readonly AuthenticationService _authenticationService = authenticationService;
        [HttpGet]
        [Route("refresh")]
        public IActionResult RefreshToken()
        {
            Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
            if (refreshToken != null && _authorizationService.ValidateToken(refreshToken))
            {
                var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);

                var userId = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.UserIdClaimName).Value;
                var user = _authenticationService.GetUserById(new Guid(userId));

                refreshToken = _authorizationService.GenerateRefreshToken(new Guid(userId));
                Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(15),
                });
                _authorizationService.SetRefreshToken(user.UserId, refreshToken);

                return Ok(new UserAndTokenResponse()
                {
                    User = new UserAndTokenResponse.EditedUser
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
                    AccessToken = _authorizationService.GenerateAccessToken(new Guid(userId), user.Role == "admin")
                });
            }

            return Unauthorized();
        }
        [HttpGet]
        [Route("validaterefresh")]
        public IActionResult ValidateRefreshToken()
        {
            Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
            if (refreshToken != null)
            {
                var result = _authorizationService.ValidateToken(refreshToken);
                if (result == true) return Ok();
            }
            return Unauthorized();
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
                    if (result == true) return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Unauthorized();
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
            return Unauthorized();
        }
    }
}
