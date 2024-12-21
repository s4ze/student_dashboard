namespace composition.Contracts;

public record class EditUserRequest
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Contacts { get; set; }
}