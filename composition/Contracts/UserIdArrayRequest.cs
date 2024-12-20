using Microsoft.AspNetCore.Mvc;

namespace composition.Contracts;

public record class UserIdArrayRequest
{
    public required List<string> ids;
}