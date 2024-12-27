using System.ComponentModel.DataAnnotations.Schema;

namespace courses.Models;

[NotMapped]
public class LessonDay
{
    public Lesson? Lesson1 { get; set; }
    public Lesson? Lesson2 { get; set; }
    public Lesson? Lesson3 { get; set; }
    public Lesson? Lesson4 { get; set; }
    public Lesson? Lesson5 { get; set; }
}
