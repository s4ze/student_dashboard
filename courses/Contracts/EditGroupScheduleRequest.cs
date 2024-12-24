using courses.Models;

namespace courses.Contracts;

public record class GroupScheduleRequest
{
    public LessonDay? Monday { get; set; }
    public LessonDay? Tuesday { get; set; }
    public LessonDay? Wednesday { get; set; }
    public LessonDay? Thursday { get; set; }
    public LessonDay? Friday { get; set; }
    public LessonDay? Saturday { get; set; }
}