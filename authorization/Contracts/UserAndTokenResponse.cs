using authorization.Models;

namespace authorization.Contracts;

public record class UserAndTokenResponse
{
    public EditedUser User { get; set; }
    public string AccessToken { get; set; }
    public class EditedUser
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Role { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Contact { get; set; }
        public string? Group { get; set; }
        public string CreatedAt { get; set; }

        public static implicit operator EditedUser(User v)
        {
            throw new NotImplementedException();
        }
    };
}
