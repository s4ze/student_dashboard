using composition.Contracts;
using Microsoft.AspNetCore.Mvc;
using Flurl.Http;
using Flurl;
using System.Net;

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
        public async Task<IActionResult> GetCourses([FromRoute] string userId)
        {
            // check if userId from request data is same with route userId -> access
            // (else if) or check for admin role of user in Authorization service -> access
            // if accessed send req to Courses service to get by userId user's enrollments and it's courses data
            var authorization = (string)Request.Headers.Authorization;
            var authResponse = await "http://localhost:5149/api/Authorization/role/".WithHeader("Authorization", authorization).GetStringAsync();
            if (authResponse == "admin")
            {
                var courseResponse = await "http://localhost:5107/api/Courses/".AppendPathSegment(userId).GetJsonAsync<object>();
                if (courseResponse != null)
                {
                    return Ok(courseResponse);
                }
                return NoContent();
            }
            return Forbid();

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
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCourses()
        {
            // check if userId from request data is same with route userId -> access
            // (else if) or check for admin role of user in Authorization service -> access
            // if accessed send req to Courses service to get by userId user's enrollments and it's courses data
            // send req to Auth service to check Admin role
            var accessToken = Request.Headers.Authorization[0][7..];

            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            var role = jwtSecurityToken.Claims.First(claim => claim.Type == JwtClaims.RoleClaimName).Value;

            if (role != "admin") return Forbid();

            var response = "http://localhost:5149/api/Authorization/validateaccess/".WithOAuthBearerToken(accessToken).GetAsync();
            if (response.Result.StatusCode == (int)HttpStatusCode.OK)
            {
                var courses = _coursesService.GetAllCourses();
                return Ok(courses);
            }
            return Unauthorized();

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
            try
            {
                var accessToken = ((string)Request.Headers.Authorization)[8..];
                var response = await "http://localhost:5149/api/Authorization/role/".WithHeader("accessToken", accessToken).GetStringAsync();
                if (response == "admin")
                {
                    _coursesService.SubscribeToCourse(new Guid(courseId), new Guid(userId));
                    return Ok();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
