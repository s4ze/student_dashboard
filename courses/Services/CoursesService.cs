using courses.Contracts;
using courses.Data;
using courses.Models;
using Microsoft.EntityFrameworkCore;

namespace courses.Services;

public class CoursesService(DataContext context)
{
    private readonly DataContext _context = context;
    public Course GetCourse(Guid courseId)
    {
        var course = _context.Courses.First(c => c.CourseId == courseId);
        return course;
    }
    public List<GetCoursesReponse> GetCourses(Guid userId)
    {
        var enrollments = _context.Enrollments.Where(e => e.UserId == userId).Include(e => e.Course);
        var enrollmentsResult = new List<GetCoursesReponse>();
        foreach (Enrollment e in enrollments)
        {
            enrollmentsResult.Add(
                new GetCoursesReponse()
                {
                    CourseId = e.Course.CourseId.ToString(),
                    Title = e.Course.Title,
                    Description = e.Course.Description,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e.Grade
                }
            );
        }
        return enrollmentsResult;
    }
    public List<Course> GetAllCourses()
    {
        return _context.Courses.ToList();
    }
    public float GetGrade(Guid courseId, Guid userId)
    {
        return _context.Enrollments.First(e => e.Course.CourseId == courseId && e.UserId == userId).Grade;
    }
    public bool SubscribeToCourse(Guid courseId, Guid userId)
    {
        if (!_context.Enrollments.Any(e => e.Course.CourseId == courseId && e.UserId == userId))
        {
            var course = GetCourse(courseId);
            _context.Enrollments.Add(new Enrollment()
            {
                EnrollmentId = new Guid(),
                UserId = userId,
                Course = course,
                Grade = 0,
                EnrollmentDate = DateTime.Now.ToString("MM-dd-yyyy HH:mmK")
            });
            _context.SaveChanges();
            return true;
        }
        return false;
    }
    public List<Course> CreateCourses(List<CreateCourseRequest> data)
    {
        List<Course> result = [];
        foreach (CreateCourseRequest c in data)
        {
            var course = new Course()
            {
                CourseId = new Guid(),
                Title = c.Title,
                Description = c.Description,
                CreatedAt = DateTime.Now.ToString("MM-dd-yyyy HH:mmK"),
            };
            result.Add(course);
            _context.Courses.Add(course);
        }
        _context.SaveChanges();
        return result;
    }
    public Course EditCourse(Guid courseId, CreateCourseRequest data)
    {
        var course = GetCourse(courseId);
        course.Title = data.Title ?? course.Title;
        course.Description = data.Description ?? course.Description;
        _context.SaveChanges();
        return course;
    }
    public void RemoveCourse(Guid courseId)
    {
        var course = GetCourse(courseId);
        _context.Courses.Remove(course);
        _context.SaveChanges();
    }
    public bool CheckIfCourseExists(Guid courseId)
    {
        return _context.Courses.Any(c => c.CourseId == courseId);
    }
    public bool CheckIfEnrollmentExists(Guid courseId, Guid userId)
    {
        return _context.Enrollments.Any(e => e.Course.CourseId == courseId && e.UserId == userId);
    }
}
