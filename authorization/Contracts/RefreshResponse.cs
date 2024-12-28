using authorization.Models;

namespace authorization.Contracts;

public record class RefreshResponse
{
    public ResponseUser User { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public class ResponseUser
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Contact { get; set; }
        public string? Group { get; set; }
        public string CreatedAt { get; set; }

        public static implicit operator ResponseUser(User v)
        {
            throw new NotImplementedException();
        }
    };
}
