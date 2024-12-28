using System.IdentityModel.Tokens.Jwt;
using System.Net;
using composition.Contracts;
using composition.Data;
using Confluent.Kafka;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        /// <summary>
        /// Получение платежей
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="204">Отсутствуют платежи</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(PaymentResponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(Null), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Null), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetPaymentsAsync([FromRoute] string userId)
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
                var claimUserId = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.UserIdClaimName).Value;

                if (responseValidateToken.StatusCode == (int)HttpStatusCode.OK && (claimUserId == userId || role == "admin"))
                {
                    var responsePayment = await "http://localhost:5177/api/Payments/"
                        .AppendPathSegment(userId)
                        .GetAsync();
                    var resultPayment = await responsePayment.GetJsonAsync<PaymentResponse>();
                    if (responsePayment.StatusCode == (int)HttpStatusCode.OK)
                        return Ok(resultPayment);
                    else if (responsePayment.StatusCode == (int)HttpStatusCode.NoContent)
                        return NoContent();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Оплата платежа по paymentId
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="204">Не найдено</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpPost]
        [Route("{paymentId}")]
        [ProducesResponseType(typeof(PaymentResponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(Null), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> PayAsync([FromRoute] string paymentId, [FromBody] PayRequest data)
        {
            try
            {
                var accessToken = Request.Headers.Authorization[0][7..];
                var responseValidateToken = await "http://localhost:5169/api/Authorization/validatetoken"
                    .PostJsonAsync(new TokenRequest()
                    {
                        Token = accessToken
                    });

                if (responseValidateToken.StatusCode == (int)HttpStatusCode.OK)
                {
                    var responsePayment = await "http://localhost:5177/api/Payments/"
                        .AppendPathSegment(paymentId)
                        .PostJsonAsync(data);
                    var resultPayment = await responsePayment.GetJsonAsync<PaymentResponse>();
                    if (responsePayment.StatusCode == (int)HttpStatusCode.OK)
                        return Ok(resultPayment);
                    else if (responsePayment.StatusCode == (int)HttpStatusCode.NoContent)
                        return NoContent();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Создание платежа для пользователя
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpPost]
        [Route("bill")]
        [ProducesResponseType(typeof(List<GetCoursesReponse>), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreatePaymentAsync([FromBody] CreatePaymentRequest data)
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

                if (responseValidateToken.StatusCode == (int)HttpStatusCode.OK && role == "admin")
                {
                    var responsePayment = await "http://localhost:5177/api/Payments/bill"
                        .PostJsonAsync(data);
                    var resultPayment = await responsePayment.GetJsonAsync<PaymentResponse>();
                    if (responsePayment.StatusCode == (int)HttpStatusCode.OK)
                        return Ok(resultPayment);
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
