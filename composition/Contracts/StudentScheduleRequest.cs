namespace composition.Contracts;

public record class StudentScheduleRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
