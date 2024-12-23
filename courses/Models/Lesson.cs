namespace courses.Models;

public class Lesson
{
    public Guid LessonId { get; set; }
    public Guid UserId { get; set; } // for Role = Professor
    public Classroom Classroom { get; set; }
    public char Form { get; set; }
}
