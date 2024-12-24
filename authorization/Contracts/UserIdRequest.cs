
namespace authorization.Contracts;

public record class UserIdRequest
{
    public required string Id { get; set; }
}
