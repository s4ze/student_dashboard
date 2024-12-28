using System.Net;
using composition.Contracts;
using Flurl.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        /// <summary>
        /// Обновление токенов доступа и обновления
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpGet]
        [Route("refresh")]
        [ProducesResponseType(typeof(AccessTokenResponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
                if (refreshToken != null)
                {
                    var response = await "http://localhost:5169/api/Authorization/refresh"
                        .PostJsonAsync(new RefreshRequest()
                        {
                            RefreshToken = refreshToken
                        });
                    if (response.StatusCode == 200)
                    {
                        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions()
                        {
                            HttpOnly = true,
                            Expires = DateTime.UtcNow.AddDays(15),
                        });
                        var result = await response.ResponseMessage.Content.ReadFromJsonAsync<RefreshResponse>();

                        return Ok(new AccessTokenResponse()
                        {
                            AccessToken = result.AccessToken
                        });
                    }
                }
                return Unauthorized("Отсутствует или некорректен токен обновления");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
