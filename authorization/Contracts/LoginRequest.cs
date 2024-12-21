using Microsoft.AspNetCore.Mvc;

namespace authorization.Contracts;

public record class LoginRequest
{
    [FromBody] public required string Email { get; set; }
    [FromBody] public required string Password { get; set; }
}
