using Microsoft.AspNetCore.Mvc;

namespace composition.Contracts;

public record class RegisterRequest
{
    [FromBody] public required string Password { get; set; }
    [FromBody] public required string FirstName { get; set; }
    [FromBody] public required string LastName { get; set; }
    [FromBody] public required string Email { get; set; }

}
