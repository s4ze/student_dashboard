namespace courses.Contracts;

public record class CreateCourseRequest
{
    public required string Title { get; set; }
    public required string Description { get; set; }
}
