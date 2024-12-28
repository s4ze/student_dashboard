namespace composition.Contracts;

public record class LoginServiceResponse
{
    public required UserModel User { get; set; }
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public class UserModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string PhotoUrl { get; set; }
        public string Contact { get; set; }
        public string Group { get; set; }
        public string CreatedAt { get; set; }
    }
}
