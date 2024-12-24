using Microsoft.AspNetCore.Mvc;

namespace authorization.Contracts;

public record class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
