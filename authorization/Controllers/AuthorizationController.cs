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
                    accessToken = _authorizationService.GenerateAccessToken(new Guid(userId), user.Role == "Admin"),
                    refreshToken = _authorizationService.GenerateRefreshToken(new Guid(userId)),
                });
            }

            return Unauthorized();
        }
        [HttpGet]
        [Route("validatetoken")]
        public IActionResult ValidateToken()
        {
            Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
            if (refreshToken == null) return Unauthorized();
            return Ok(_authorizationService.ValidateToken(refreshToken));
        }
        [HttpGet]
        [Route("role")]
        public IActionResult GetRole()
        {
            Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
            if (refreshToken != null && _authorizationService.ValidateToken(refreshToken))
            {
                var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);
                var isAdmin = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.AdminClaimName).Value;
                return Ok(isAdmin);
            }
            return Unauthorized();
        }
    }
}
