using System.ComponentModel.DataAnnotations.Schema;

namespace courses.Models;

public class Classroom
{
    public Guid ClassromId { get; set; }
    [Column(TypeName = "VARCHAR(10)")] public string Number { get; set; }
}
