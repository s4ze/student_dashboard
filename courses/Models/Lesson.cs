using System.ComponentModel.DataAnnotations.Schema;

namespace courses.Models;

[NotMapped]
public class Lesson
{
    public string Professor { get; set; }
    public string Course { get; set; }
    public string Classroom { get; set; }
    public char Form { get; set; } = 's';
}
