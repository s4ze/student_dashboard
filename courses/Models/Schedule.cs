using System.ComponentModel.DataAnnotations.Schema;

namespace courses.Models;

public class Schedule
{
    public Guid ScheduleId { get; set; }
    [Column(TypeName = "VARCHAR(10)")] public required string Group { get; set; }
    public LessonDay? Monday { get; set; }
    public LessonDay? Tuesday { get; set; }
    public LessonDay? Wednesday { get; set; }
    public LessonDay? Thursday { get; set; }
    public LessonDay? Friday { get; set; }
    public LessonDay? Saturday { get; set; }
}
