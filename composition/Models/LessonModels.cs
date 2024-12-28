namespace composition.Models;

public record class LessonDay
{
    public Lesson? Lesson1 { get; set; }
    public Lesson? Lesson2 { get; set; }
    public Lesson? Lesson3 { get; set; }
    public Lesson? Lesson4 { get; set; }
    public Lesson? Lesson5 { get; set; }
}
public record class Lesson
{
    public string Professor { get; set; }
    public string Course { get; set; }
    public string Classroom { get; set; }
    public char Form { get; set; } = 's';
}