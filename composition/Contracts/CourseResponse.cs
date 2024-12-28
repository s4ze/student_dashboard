namespace composition.Contracts;

public record class GetCoursesReponse
{
    public required string CourseId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string EnrollmentDate { get; set; }
    public required float Grade { get; set; }
}