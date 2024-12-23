using System.ComponentModel.DataAnnotations.Schema;

namespace messages.Models;

public class Message
{
    public Guid MessageId { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Content { get; set; }
    [Column(TypeName = "VARCHAR(25)")] public string CreatedAt { get; set; }
}
