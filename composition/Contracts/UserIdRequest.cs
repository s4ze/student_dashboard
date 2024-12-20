
using Microsoft.AspNetCore.Mvc;

namespace composition.Contracts;

public record class UserIdRequest
{
    [FromRoute] public required string Id { get; set; }
}
