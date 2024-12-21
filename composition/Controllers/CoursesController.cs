using composition.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
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
        [HttpPost]
        [Route("")]
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
