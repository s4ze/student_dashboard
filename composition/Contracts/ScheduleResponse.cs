using composition.Models;

namespace composition.Contracts;

public record class ScheduleResponse
{
    public Guid ScheduleId { get; set; }
    public required string Group { get; set; }
    public LessonDay? Monday { get; set; }
    public LessonDay? Tuesday { get; set; }
    public LessonDay? Wednesday { get; set; }
    public LessonDay? Thursday { get; set; }
    public LessonDay? Friday { get; set; }
    public LessonDay? Saturday { get; set; }
}