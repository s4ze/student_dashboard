using System.ComponentModel.DataAnnotations.Schema;

namespace courses.Models;

public class Enrollment
{
    public Guid EnrollmentId { get; set; }
    public Guid UserId { get; set; }
    public Course Course { get; set; }
    [Column(TypeName = "NUMERIC(3,2)")] public float Grade { get; set; }
    [Column(TypeName = "VARCHAR(10)")] public string EnrollmentDate { get; set; }
}
