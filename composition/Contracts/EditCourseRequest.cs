namespace composition.Contracts;

public record class EditCourseRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
}
