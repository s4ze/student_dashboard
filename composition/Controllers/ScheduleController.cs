using System.IdentityModel.Tokens.Jwt;
using System.Net;
using composition.Contracts;
using composition.Data;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        [HttpPost]
        [Route("group/{number}")]
        [ProducesResponseType(typeof(ScheduleResponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateGroupScheduleAsync([FromRoute] string number, [FromBody] ScheduleRequest data)
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
                    var responseSchedule = await "http://localhost:5107/api/Schedule/group/"
                        .AppendPathSegment(number)
                        .PostJsonAsync(data);
                    if (responseSchedule.StatusCode == (int)HttpStatusCode.OK)
                    {
                        var resultSchedule = await responseSchedule.GetJsonAsync<ScheduleResponse>();
                        return Ok(resultSchedule);
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
        /// Get schedule of group
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{number}")]
        [ProducesResponseType(typeof(ScheduleResponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetGroupScheduleAsync([FromRoute] string number)
        {
            try
            {
                var responseSchedule = await "http://localhost:5107/api/Schedule/group/"
                    .AppendPathSegment(number)
                    .GetAsync();
                if (responseSchedule.StatusCode == (int)HttpStatusCode.OK)
                {
                    var resultSchedule = await responseSchedule.GetJsonAsync<ScheduleResponse>();
                    return Ok(resultSchedule);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Edit group's schedule
        /// </summary>
        /// <param name="number"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("group/{number}")]
        [ProducesResponseType(typeof(ScheduleResponse), (int)HttpStatusCode.OK, "application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> EditGroupScheduleAsync([FromRoute] string number, [FromBody] ScheduleRequest data)
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
                    var responseSchedule = await "http://localhost:5107/api/Schedule/group/"
                        .AppendPathSegment(number)
                        .PutJsonAsync(data);
                    if (responseSchedule.StatusCode == (int)HttpStatusCode.OK)
                    {
                        var resultSchedule = await responseSchedule.GetJsonAsync<ScheduleResponse>();
                        return Ok(resultSchedule);
                    }
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
