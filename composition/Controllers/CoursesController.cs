using composition.Contracts;
using Microsoft.AspNetCore.Mvc;
using Flurl.Http;
using Flurl;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using composition.Data;
using Confluent.Kafka;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        /// <summary>
        /// Получение списка всех курсов
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(List<GetCoursesReponse>), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllCourses()
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

                if (responseValidateToken.StatusCode == (int)HttpStatusCode.OK && role == "admin")
                {
                    var responseCourses = await "http://localhost:5107/api/Courses/all"
                        .GetAsync();
                    var dataCourses = await responseCourses.GetJsonAsync<List<GetCoursesReponse>>();
                    if (responseCourses.StatusCode == (int)HttpStatusCode.OK)
                        return Ok(dataCourses);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Получение списка курсов, на которые подписан пользователь
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(List<GetCoursesReponse>), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetCourses([FromRoute] string userId)
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
                    var responseCourses = await "http://localhost:5107/api/Courses/"
                        .AppendPathSegment(userId)
                        .GetAsync();
                    var dataCourses = await responseCourses.GetJsonAsync<List<GetCoursesReponse>>();
                    if (responseCourses.StatusCode == (int)HttpStatusCode.OK)
                        return Ok(dataCourses);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Добавление курса
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpPost]
        [ProducesResponseType(typeof(List<GetCoursesReponse>), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateCourseAsync([FromBody] List<CreateCourseRequest> data)
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
                    var responseCourses = await "http://localhost:5107/api/Courses/"
                        .PostJsonAsync(data);
                    var dataCourses = await responseCourses.GetJsonAsync<List<GetCoursesReponse>>();
                    if (responseCourses.StatusCode == (int)HttpStatusCode.OK)
                        return Ok(dataCourses);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Изменение курса по значению courseId
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpPut]
        [Route("{courseId}")]
        [ProducesResponseType(typeof(List<GetCoursesReponse>), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> EditCourseAsync([FromRoute] string courseId, [FromBody] EditCourseRequest data)
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
                    var responseCourses = await "http://localhost:5107/api/Courses/"
                        .AppendPathSegment(courseId)
                        .PutJsonAsync(data);
                    var dataCourses = await responseCourses.GetJsonAsync<List<GetCoursesReponse>>();
                    if (responseCourses.StatusCode == (int)HttpStatusCode.OK)
                        return Ok(dataCourses);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Удаление курса по значению courseId
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpDelete]
        [Route("{courseId}")]
        [ProducesResponseType(typeof(Null), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> RemoveCourseAsync([FromRoute] string courseId)
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
                    var responseCourses = await "http://localhost:5107/api/Courses/"
                        .AppendPathSegment(courseId)
                        .DeleteAsync();
                    if (responseCourses.StatusCode == (int)HttpStatusCode.OK)
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
        /// Получение оценки пользователя на курсе
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpGet]
        [Route("grade/{courseId}/{userId}")]
        [ProducesResponseType(typeof(GradeReponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetCourseGradeAsync([FromRoute] string courseId, [FromRoute] string userId)
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
                    var responseCourses = await "http://localhost:5107/api/Courses/grade/"
                        .AppendPathSegment(courseId)
                        .AppendPathSegment(userId)
                        .GetAsync();
                    if (responseCourses.StatusCode == (int)HttpStatusCode.OK)
                    {
                        var resultCourses = await responseCourses.GetJsonAsync<GradeReponse>();
                        return Ok(resultCourses);
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
        /// Подписание пользователя на курс
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <response code="401">Некорректные данные</response>
        [HttpPost]
        [Route("subscribe/{courseId}/{userId}")]
        [ProducesResponseType(typeof(Null), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> SubscribeOnCourseAsync([FromRoute] string courseId, [FromRoute] string userId)
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
                    var responseCourses = await "http://localhost:5107/api/Courses/subscribe/"
                        .AppendPathSegment(courseId)
                        .AppendPathSegment(userId)
                        .PostAsync();
                    if (responseCourses.StatusCode == (int)HttpStatusCode.OK)
                        return Ok();
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
