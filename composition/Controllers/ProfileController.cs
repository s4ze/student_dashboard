using System.IdentityModel.Tokens.Jwt;
using System.Net;
using composition.Contracts;
using composition.Data;
using composition.Models;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        /// <summary>
        /// Получить данные пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpGet]
        [Route("user/{userId}")]
        [ProducesResponseType(typeof(ResponseUser), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetUser([FromRoute] string userId)
        {
            try
            {
                var responseRole = await "http://localhost:5169/api/Authorization/role"
                    .AppendPathSegment(userId)
                    .GetAsync();
                var dataRole = await responseRole.GetJsonAsync<RoleResponse>();
                if (responseRole.StatusCode == (int)HttpStatusCode.OK && dataRole.Role == "admin")
                {
                    var responseGetUser = await "http://localhost:5288/api/Profile/user"
                        .AppendPathSegment(userId)
                        .GetAsync();
                    var dataGetUser = await responseGetUser.GetJsonAsync<ResponseUser>();
                    if (responseGetUser.StatusCode == (int)HttpStatusCode.OK)
                        return Ok(new ResponseUser()
                        {
                            UserId = dataGetUser.UserId,
                            Email = dataGetUser.Email,
                            FirstName = dataGetUser.FirstName,
                            LastName = dataGetUser.LastName,
                            PhotoUrl = dataGetUser.PhotoUrl,
                            Contact = dataGetUser.Contact,
                            Group = dataGetUser.Group,
                            CreatedAt = dataGetUser.CreatedAt
                        });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Изменить данные пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpPut]
        [Route("user/{userId}")]
        [ProducesResponseType(typeof(ResponseUser), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> EditUserAsync([FromRoute] string userId, [FromBody] EditUserRequest data)
        {
            try
            {
                var accessToken = Request.Headers.Authorization[0][7..];
                var responseValidateToken = await "http://localhost:5169/api/Authorization/validatetoken"
                    .PostJsonAsync(new TokenRequest()
                    {
                        Token = accessToken
                    });

                var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                var role = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.RoleClaimName).Value;
                var senderUserId = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.UserIdClaimName).Value;

                if (responseValidateToken.StatusCode == (int)HttpStatusCode.OK && (senderUserId == userId || role == "admin"))
                {
                    var responseEditUser = await "http://localhost:5288/api/Profile/user"
                        .AppendPathSegment(userId)
                        .PutJsonAsync(data);
                    var dataEditUser = await responseEditUser.GetJsonAsync<ResponseUser>();
                    if (responseEditUser.StatusCode == (int)HttpStatusCode.OK)
                        return Ok(new ResponseUser()
                        {
                            UserId = dataEditUser.UserId,
                            Email = dataEditUser.Email,
                            FirstName = dataEditUser.FirstName,
                            LastName = dataEditUser.LastName,
                            PhotoUrl = dataEditUser.PhotoUrl,
                            Contact = dataEditUser.Contact,
                            Group = dataEditUser.Group,
                            CreatedAt = dataEditUser.CreatedAt
                        });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>        
        [HttpDelete]
        [Route("user/{userId}")]
        [ProducesResponseType(typeof(ResponseUser), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> RemoveUserAsync([FromRoute] string userId)
        {
            try
            {
                var accessToken = Request.Headers.Authorization[0][7..];
                var responseValidateToken = await "http://localhost:5169/api/Authorization/validatetoken"
                    .PostJsonAsync(new TokenRequest()
                    {
                        Token = accessToken
                    });

                var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                var role = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.RoleClaimName).Value;
                var senderUserId = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.UserIdClaimName).Value;
                if (responseValidateToken.StatusCode == (int)HttpStatusCode.OK && (senderUserId == userId || role == "admin"))
                {
                    var responseRemoveUser = await "http://localhost:5288/api/Profile/user"
                        .AppendPathSegment(userId)
                        .DeleteAsync();
                    if (responseRemoveUser.StatusCode == (int)HttpStatusCode.OK)
                        return Ok();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Регистрация аккаунта
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(ResponseUser), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest data)
        {
            try
            {
                var response = await "http://localhost:5288/api/Profile/register"
                    .PostJsonAsync(data);
                if (response.StatusCode == 200)
                    return Ok();
                else if (response.StatusCode == 409)
                    return Conflict("Email already taken");
                else
                {
                    var result = await response.ResponseMessage.Content.ReadAsStringAsync();
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
