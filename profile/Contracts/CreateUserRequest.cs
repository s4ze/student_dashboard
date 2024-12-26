namespace profile.Contracts;

public record class CreateUserRequest
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Contact { get; set; }
    public string? Group { get; set; }
    public required string Password { get; set; }
}
