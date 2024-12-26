using authorization.Contracts;
using authorization.Services;
using Microsoft.AspNetCore.Mvc;

namespace authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(AuthenticationService authenticationService, AuthorizationService authorizationService) : ControllerBase
    {
        private readonly AuthenticationService _authenticationService = authenticationService;
        private readonly AuthorizationService _authorizationService = authorizationService;
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest data)
        {
            if (_authenticationService.Login(data))
            {
                var user = _authenticationService.GetUserByEmail(data.Email);

                var refreshToken = _authorizationService.GenerateRefreshToken(user.UserId);
                Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(15),
                });
                _authorizationService.SetRefreshToken(user.UserId, refreshToken);

                return Ok(new
                {
                    user = new
                    {
                        user.UserId,
                        user.Email,
                        user.FirstName,
                        user.LastName,
                        user.Role,
                        user.PhotoUrl,
                        user.Contact,
                        user.Group,
                        user.CreatedAt
                    },
                    accessToken = _authorizationService.GenerateAccessToken(user.UserId, user.Role == "admin"),
                });
            }
            return Unauthorized();
        }
    }
}
