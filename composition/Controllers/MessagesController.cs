using System.IdentityModel.Tokens.Jwt;
using System.Net;
using composition.Contracts;
using composition.Data;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    /// <summary>
    /// Сервис сообщений
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        /// <summary>
        /// Получить сообщение
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpGet]
        [Route("one/{messageId}")]
        [ProducesResponseType(typeof(MessageResponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetMessageAsync([FromRoute] string messageId)
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
                var receiverUserId = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.UserIdClaimName).Value;

                var responseMessage = await "http://localhost:5163/api/Messages/one"
                        .AppendPathSegment(messageId)
                        .GetAsync();
                var resultMessage = await responseMessage.GetJsonAsync<MessageResponse>();

                if (responseValidateToken.StatusCode == (int)HttpStatusCode.OK && responseMessage.StatusCode == (int)HttpStatusCode.OK
                    && (receiverUserId == resultMessage.ReceiverId || role == "admin"))
                    return Ok(resultMessage);

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Получение сообщений через получателя
        /// </summary>
        /// <param name="receiverId"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpGet]
        [Route("{receiverId}")]
        [ProducesResponseType(typeof(List<MessageResponse>), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetMessagesAsync([FromRoute] string receiverId)
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
                var receiverUserId = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.UserIdClaimName).Value;

                if (responseValidateToken.StatusCode == (int)HttpStatusCode.OK && (receiverUserId == receiverId || role == "admin"))
                {
                    var responseMessages = await "http://localhost:5163/api/Messages/"
                        .AppendPathSegment(receiverId)
                        .GetAsync();
                    if (responseMessages.StatusCode == (int)HttpStatusCode.OK)
                    {
                        var resultMessages = await responseMessages.GetJsonAsync<List<MessageResponse>>();
                        return Ok(resultMessages);
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Отправление сообщение от пользователя пользователю
        /// </summary>
        /// <param name="receiverId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpPost]
        [Route("{receiverId}")]
        [ProducesResponseType(typeof(MessageResponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> SendMessageAsync([FromRoute] string receiverId, [FromBody] CreateMessageRequest data)
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
                var receiverUserId = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.UserIdClaimName).Value;
                var responseSenderExists = await "http://localhost:528/api/Profile/userexists"
                    .AppendPathSegment(data.SenderId)
                    .GetAsync();
                var resultSenderExists = await responseSenderExists.GetJsonAsync<UserExistsResponse>();

                if (responseValidateToken.StatusCode == (int)HttpStatusCode.OK && (receiverUserId == receiverId || role == "admin")
                    && responseSenderExists.StatusCode == (int)HttpStatusCode.OK && resultSenderExists.UserExists == true)
                {
                    var responseMessages = await "http://localhost:5163/api/Messages/"
                        .AppendPathSegment(receiverId)
                        .PostJsonAsync(data);
                    if (responseMessages.StatusCode == (int)HttpStatusCode.OK)
                    {
                        var resultMessages = await responseMessages.GetJsonAsync<MessageResponse>();
                        return Ok(resultMessages);
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Редактирование сообщения
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpPut]
        [Route("{messageId}")]
        [ProducesResponseType(typeof(MessageResponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> EditMessageAsync([FromRoute] string messageId, [FromBody] EditMessageRequest data)
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
                var receiverUserId = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.UserIdClaimName).Value;

                var responseGetMessage = await "http://localhost:5163/api/Messages/one"
                    .AppendPathSegment(messageId)
                    .GetAsync();
                var resultMessage = await responseGetMessage.GetJsonAsync<MessageResponse>();

                if (responseValidateToken.StatusCode == (int)HttpStatusCode.OK && responseGetMessage.StatusCode == (int)HttpStatusCode.OK
                    && (receiverUserId == resultMessage.ReceiverId || role == "admin"))
                {
                    var responseEditMessage = await "http://localhost:5163/api/Messages/"
                        .AppendPathSegment(messageId)
                        .PutJsonAsync(data);
                    var resultEditMessage = await responseEditMessage.GetJsonAsync<MessageResponse>();
                    return Ok(resultEditMessage);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Remove messages
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpDelete]
        [Route("{messageId}")]
        [ProducesResponseType(typeof(UserExistsResponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> RemoveMessageAsync([FromRoute] string messageId)
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
                var receiverUserId = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.UserIdClaimName).Value;

                var responseGetMessage = await "http://localhost:5163/api/Messages/one"
                    .AppendPathSegment(messageId)
                    .GetAsync();
                var resultMessage = await responseGetMessage.GetJsonAsync<MessageResponse>();

                if (responseValidateToken.StatusCode == (int)HttpStatusCode.OK && responseGetMessage.StatusCode == (int)HttpStatusCode.OK
                    && (receiverUserId == resultMessage.ReceiverId || role == "admin"))
                {
                    var responseEditMessage = await "http://localhost:5163/api/Messages/"
                        .AppendPathSegment(messageId)
                        .DeleteAsync();
                    var resultEditMessage = await responseEditMessage.GetJsonAsync<UserExistsResponse>();
                    return Ok(resultEditMessage);
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
