namespace messages.Contracts;

public record class EditUserRequest
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Contact { get; set; }
    public string? Group { get; set; }
    public string? Password { get; set; }
}
