using System.ComponentModel.DataAnnotations.Schema;

namespace courses.Models;

public class Schedule
{
    public Guid ScheduleId { get; set; }
    [Column(TypeName = "VARCHAR(10)")] public string Group { get; set; }
    [Column(TypeName = "JSONB")] public LessonDay Monday { get; set; } = new();
    [Column(TypeName = "JSONB")] public LessonDay Tuesday { get; set; } = new();
    [Column(TypeName = "JSONB")] public LessonDay Wednesday { get; set; } = new();
    [Column(TypeName = "JSONB")] public LessonDay Thursday { get; set; } = new();
    [Column(TypeName = "JSONB")] public LessonDay Friday { get; set; } = new();
    [Column(TypeName = "JSONB")] public LessonDay Saturday { get; set; } = new();
}
