namespace composition.Contracts;

public record class MessageResponse
{
    public required string MessageId { get; set; }
    public required string SenderId { get; set; }
    public required string ReceiverId { get; set; }
    public required string Content { get; set; }
    public required string CreatedAt { get; set; }
}
