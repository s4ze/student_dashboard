using composition.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        /// <summary>
        /// Get schedule of group
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{number}")]
        public IActionResult GetGroupSchedule([FromRoute] string number)
        {
            // send req to Courses service to get group's schedule

            return Ok();
        }
        /// <summary>
        /// Edit group's schedule
        /// </summary>
        /// <param name="number"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("group/{number}")]
        public IActionResult EditGroupSchedule([FromRoute] string number, [FromBody] EditGroupScheduleRequest data)
        {
            // send req to Authorization and Courses service to authorize admin and edit group's schedule

            return Ok();
        }
        /* [HttpGet]
        [Route("professor/{fullname}")]
        public IActionResult GetProfessorSchedule(data)
        {
            // send req to Profileservice to get professor's schedule

        } */
        /// <summary>
        /// Get schedule by student
        /// </summary>
        /// <param name="studentFullName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("student")]
        public IActionResult GetStudentSchedule([FromBody] string studentFullName)
        {
            // send req to Profile and Courses service to get student's group schedule

            return Ok();
        }
    }
}
