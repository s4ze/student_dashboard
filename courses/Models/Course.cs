using System.ComponentModel.DataAnnotations.Schema;

namespace courses.Models;

public class Course
{
    public Guid CourseId { get; set; }
    [Column(TypeName = "VARCHAR(256)")] public string Title { get; set; }
    [Column(TypeName = "TEXT")] public string Description { get; set; }
    [Column(TypeName = "VARCHAR(25)")] public string CreatedAt { get; set; }
}
