using courses.Contracts;
using courses.Services;
using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController(ScheduleService scheduleService) : ControllerBase
    {
        private readonly ScheduleService _scheduleService = scheduleService;
        [HttpPost]
        [Route("group/{number}")]
        public IActionResult AddSchedule([FromRoute] string number, [FromBody] GroupScheduleRequest data)
        {
            try
            {
                if (!_scheduleService.CheckIfScheduleExists(number))
                {
                    var schedule = _scheduleService.CreateGroupSchedule(number, data);
                    return Ok(schedule);
                }
                return BadRequest("Schedule for this group already exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("group/{number}")]
        public IActionResult GetGroupSchedule([FromRoute] string number)
        {
            try
            {
                if (_scheduleService.CheckIfScheduleExists(number))
                {
                    var schedule = _scheduleService.GetGroupSchedule(number);
                    // TODO: remove if works; var result = _scheduleService.ConvertScheduleToResponse(schedule);
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
        public IActionResult EditGroupSchedule([FromRoute] string number, [FromBody] GroupScheduleRequest data)
        {
            try
            {
                if (_scheduleService.CheckIfScheduleExists(number))
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
        [HttpDelete]
        [Route("group/{number}")]
        public IActionResult RemoveGroupSchedule([FromRoute] string number)
        {
            try
            {
                if (_scheduleService.CheckIfScheduleExists(number))
                {
                    _scheduleService.RemoveGroupSchedule(number);
                    return Ok();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Сделать через Composition
        /* [HttpPost]
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
        } */
    }
}
