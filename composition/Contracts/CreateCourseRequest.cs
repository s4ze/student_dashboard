namespace composition.Contracts;

public record class CreateCourseRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
}
