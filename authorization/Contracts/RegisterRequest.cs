using Microsoft.AspNetCore.Mvc;

namespace authorization.Contracts;

public record class RegisterRequest
{
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }

}
