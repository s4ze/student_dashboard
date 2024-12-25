using courses.Contracts;
using courses.Models;
using courses.Services;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController(ScheduleService scheduleService) : ControllerBase
    {
        private readonly ScheduleService _scheduleService = scheduleService;
        [HttpGet]
        [Route("group/{number}")]
        public IActionResult GetGroupSchedule([FromRoute] string number)
        {
            try
            {
                if (_scheduleService.CheckIfScheduleExists(number))
                {
                    var schedule = _scheduleService.GetGroupSchedule(number);
                    // var result = _scheduleService.ConvertScheduleToResponse(schedule);
                    return Ok(schedule);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("group/{number}")]
        public async Task<IActionResult> EditGroupSchedule([FromRoute] string number, [FromBody] GroupScheduleRequest data)
        {
            try
            {
                var accessToken = ((string)Request.Headers.Authorization)[8..];
                var response = await "http://localhost:5149/api/Authorization/role/".WithHeader("accessToken", accessToken).GetStringAsync();
                if (response == "true" && _scheduleService.CheckIfScheduleExists(number))
                {
                    var schedule = _scheduleService.EditGroupSchedule(number, data);
                    // var result = _scheduleService.ConvertScheduleToResponse(schedule);
                    return Ok(schedule);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /* [HttpGet]
        [Route("professor/{fullname}")]
        public IActionResult GetProfessorSchedule(data)
        {
            // send req to Profileservice to get professor's schedule

        } */
        [HttpPost]
        [Route("student")]
        public async Task<IActionResult> GetStudentSchedule([FromBody] string userId)
        {
            try
            {
                // send req to Profile and Courses service to get student's group schedule
                var response = await "http://localhost:5288/api/Profile/user/".AppendPathSegment(userId).GetJsonAsync<User>();
                if (_scheduleService.CheckIfScheduleExists(response.Group))
                {
                    var schedule = _scheduleService.GetGroupSchedule(response.Group);
                    // var result = _scheduleService.ConvertScheduleToResponse(schedule);
                    return Ok(schedule); ;
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
