using System.ComponentModel.DataAnnotations;

namespace composition.Models;

public class User
{
    [Required] public Guid UserId { get; set; }
}