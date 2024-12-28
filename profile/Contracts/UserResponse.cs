namespace profile.Contracts;

public record class UserResponse
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhotoUrl { get; set; }
    public string Contact { get; set; }
    public string Group { get; set; }
    public string CreatedAt { get; set; }
}