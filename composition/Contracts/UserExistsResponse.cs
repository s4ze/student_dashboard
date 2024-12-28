namespace composition.Contracts;

public record class UserExistsResponse
{
    public required bool UserExists { get; set; }
}