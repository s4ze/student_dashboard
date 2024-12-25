using System.ComponentModel.DataAnnotations.Schema;

namespace authorization.Models;

public class User
{
    public Guid UserId { get; set; }
    [Column(TypeName = "VARCHAR(513)")] public string Email { get; set; }
    [Column(TypeName = "VARCHAR(16)")] public string Role { get; set; }
    [Column(TypeName = "VARCHAR(256)")] public string FirstName { get; set; }
    [Column(TypeName = "VARCHAR(256)")] public string LastName { get; set; }
    [Column(TypeName = "VARCHAR(512)")] public string PhotoUrl { get; set; }
    [Column(TypeName = "TEXT")] public string Contact { get; set; }
    [Column(TypeName = "VARCHAR(10)")] public string Group { get; set; }
    [Column(TypeName = "VARCHAR(64)")] public string Password { get; set; }
    [Column(TypeName = "VARCHAR(128)")] public string? RefreshToken { get; set; }
    [Column(TypeName = "VARCHAR(25)")] public required string CreatedAt { get; set; }
}
