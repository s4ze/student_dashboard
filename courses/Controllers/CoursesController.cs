using System.Net;
using courses.Contracts;
using courses.Data;
using courses.Models;
using courses.Services;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;

namespace courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(CoursesService coursesService) : ControllerBase
    {
        private readonly CoursesService _coursesService = coursesService;
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetCourses([FromRoute] string userId)
        {
            try
            {
                var courses = _coursesService.GetCourses(new Guid(userId));
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("all")]
        public IActionResult GetAllCourses()
        {
            try
            {
                var courses = _coursesService.GetAllCourses();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("grade/{courseId}/{userId}")]
        public IActionResult GetGrade([FromRoute] string courseId, [FromRoute] string userId)
        {
            try
            {
                if (_coursesService.CheckIfCourseExists(new Guid(courseId)))
                {
                    return Ok(new GradeReponse()
                    {
                        Grade = _coursesService.GetGrade(new Guid(courseId), new Guid(userId))
                    });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("subscribe/{courseId}/{userId}")]
        public IActionResult SubscribeToCourse([FromRoute] string courseId, [FromRoute] string userId)
        {
            try
            {
                var result = _coursesService.SubscribeToCourse(new Guid(courseId), new Guid(userId));
                return result ? Ok() : BadRequest("Already subscribed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult CreateCourses([FromBody] List<CreateCourseRequest> data)
        {
            var courses = _coursesService.CreateCourses(data);
            return Ok(courses);
        }
        [HttpPut]
        [Route("{courseId}")]
        public IActionResult EditCourse([FromRoute] string courseId, [FromBody] CreateCourseRequest data)
        {
            try
            {
                if (_coursesService.CheckIfCourseExists(new Guid(courseId)))
                {
                    var course = _coursesService.EditCourse(new Guid(courseId), data);
                    return Ok(course);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{courseId}")]
        public IActionResult RemoveCourse([FromRoute] string courseId)
        {
            try
            {
                if (_coursesService.CheckIfCourseExists(new Guid(courseId)))
                {
                    _coursesService.RemoveCourse(new Guid(courseId));
                    return Ok();
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
