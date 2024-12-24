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
                    accessToken = _authorizationService.GenerateAccessToken(user.UserId, user.Role == "Admin"),
                    refreshToken = _authorizationService.GenerateRefreshToken(user.UserId),
                });
            }

            return Unauthorized();
        }
    }
}
