using System.Net;
using composition.Contracts;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    /// <summary>
    /// Аутентификация пользователя
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(UserTokenResponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest data)
        {
            try
            {
                var response = await "http://localhost:5169/api/Authentication/login"
                    .PostJsonAsync(data);
                if (response.StatusCode == 200)
                {
                    var result = await response.ResponseMessage.Content.ReadFromJsonAsync<LoginServiceResponse>();
                    Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions()
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddDays(15),
                    });

                    return Ok(new UserTokenResponse()
                    {
                        User = new UserTokenResponse.UserModel()
                        {
                            UserId = result.User.UserId,
                            Email = result.User.Email,
                            FirstName = result.User.FirstName,
                            LastName = result.User.LastName,
                            PhotoUrl = result.User.PhotoUrl,
                            Contact = result.User.Contact,
                            Group = result.User.Group,
                            CreatedAt = result.User.CreatedAt
                        },
                        AccessToken = result.AccessToken
                    });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
