using System;
using Microsoft.AspNetCore.Mvc;

namespace composition.Contracts;

public record class EditUserRequest
{
    [FromRoute] public required string Id { get; set; }
    [FromBody] public string? Email { get; set; }
    [FromBody] public string? FirstName { get; set; }
    [FromBody] public string? LastName { get; set; }
    [FromBody] public string? Password { get; set; }
}
