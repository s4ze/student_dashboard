using System.ComponentModel.DataAnnotations.Schema;

namespace courses.Models;

public class Enrollment
{
    public required Guid EnrollmentId { get; set; }
    public required Guid UserId { get; set; }
    public required Course Course { get; set; }
    [Column(TypeName = "NUMERIC(5,2)")] public float Grade { get; set; }
    [Column(TypeName = "VARCHAR(25)")] public required string EnrollmentDate { get; set; }
}
