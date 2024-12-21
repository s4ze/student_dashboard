
using Microsoft.AspNetCore.Mvc;

namespace authorization.Contracts;

public record class UserIdRequest
{
    [FromRoute] public required string Id { get; set; }
}
