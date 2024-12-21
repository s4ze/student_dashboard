namespace composition.Contracts;

public record class EditGroupScheduleRequest
{
    public class LessonDay
    {
        public Lesson? Lesson1 { get; set; }
        public Lesson? Lesson2 { get; set; }
        public Lesson? Lesson3 { get; set; }
        public Lesson? Lesson4 { get; set; }
        public Lesson? Lesson5 { get; set; }
    }

    public class Lesson
    {
        public required string CourseName { get; set; }
        public required string ProfessorFullName { get; set; }
        public required string Auditorium { get; set; }
    }
    public LessonDay? Monday { get; set; }
    public LessonDay? Tuesday { get; set; }
    public LessonDay? Wednesday { get; set; }
    public LessonDay? Thursday { get; set; }
    public LessonDay? Friday { get; set; }
    public LessonDay? Saturday { get; set; }
}