using composition.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        /// <summary>
        /// Get user's subscribed courses
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUsersCourses([FromRoute] string userId)
        {
            // check if userId from request data is same with route userId -> access
            // (else if) or check for admin role of user in Authorization service -> access
            // if accessed send req to Courses service to get by userId user's enrollments and it's courses data


            // return format:
            // [
            //     {
            //         courseId: string,
            //         title: string,
            //         description: string,
            //         enrollmentDate: string,
            //         grade: float
            //     }
            // ]

            return Ok();
        }
        /// <summary>
        /// Create course
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateCourse([FromBody] CreateCourseRequest data)
        {
            // send req to Authorization and Courses service to authorize admin and edit course 

            // return format:
            // {
            //     courseId: uuid,
            //     title: string,
            //     description: string,
            //     createdAt: string
            // }

            return Ok();
        }
        /// <summary>
        /// Edit course by its id
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{courseId}")]
        public IActionResult EditCourse([FromRoute] string courseId, [FromBody] EditCourseRequest data)
        {
            // send req to Authorization and Courses service to authorize admin and edit course 

            // return format:
            // {
            //     courseId: uuid,
            //     title: string,
            //     description: string,
            //     createdAt: string
            // }

            return Ok();
        }
        /// <summary>
        /// Remove course by it's id
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{courseId}")]
        public IActionResult RemoveCourse([FromRoute] string courseId)
        {
            // send req to Authorization and Courses service to authorize admin and edit course 

            // return format:
            // {
            //     courseId: uuid,
            //     title: string,
            //     description: string,
            //     createdAt: string
            // }

            return Ok();
        }
        /// <summary>
        /// Get user's grade on course
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("grade/{courseId}/{userId}")]
        public IActionResult GetCourseGrade([FromRoute] UsersCourseRequest data)
        {
            // check if userId from request data is same with route userId -> access
            // (else if) or check for admin role of user in Authorization service -> access
            // if accessed send req to Courses service to get user's course grade

            // return format:
            // {
            //     courseId: string,
            //     grade: float
            // }

            return Ok();
        }
        /// <summary>
        /// Subscribe user on course
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("subscribe/{courseId}/{userId}")]
        public IActionResult SubscribeOnCourse([FromRoute] UsersCourseRequest data)
        {
            // send req to Authorization and Courses service to authorize admin and get all courses 

            // return format:
            // {
            //     courseId: uuid,
            //     title: string,
            //     description: string,
            //     enrollmentDate: string
            // }

            return Ok();
        }
        /// <summary>
        /// Get a list of all courses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public IActionResult GetCourses()
        {
            // send req to Authorization and Courses service to authorize admin and get all courses 

            // return format:
            // [
            //     {
            //         courseId: uuid,
            //         title: string,
            //         description: string,
            //         createdAt: string,
            //     },
            // ]

            return Ok();
        }

    }
}
