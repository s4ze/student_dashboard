using composition.Models;

namespace composition.Contracts;

public record class LoginServiceResponse
{
    public required ResponseUser User { get; set; }
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
