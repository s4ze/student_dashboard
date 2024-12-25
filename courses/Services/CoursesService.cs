using courses.Contracts;
using courses.Data;
using courses.Models;

namespace courses.Services;

public class CoursesService(DataContext context)
{
    private readonly DataContext _context = context;
    public Course GetCourse(Guid courseId)
    {
        var course = _context.Courses.First(c => c.CourseId == courseId);
        return course;
    }
    public List<object> GetCourses(Guid userId)
    {
        var enrollments = _context.Enrollments.Where(e => e.UserId == userId);
        var enrollmentsResult = new List<object>();
        foreach (Enrollment e in enrollments)
        {
            enrollmentsResult.Add(new
            {
                e.Course.CourseId,
                e.Course.Title,
                e.Course.Description,
                e.EnrollmentDate,
                e.Grade
            });
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
    public void SubscribeToCourse(Guid courseId, Guid userId)
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
    }
    public Course CreateCourse(CreateCourseRequest data)
    {
        var course = new Course()
        {
            CourseId = new Guid(),
            Title = data.Title,
            Description = data.Description,
            CreatedAt = DateTime.Now.ToString("MM-dd-yyyy HH:mmK"),
        };
        return course;
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
}
