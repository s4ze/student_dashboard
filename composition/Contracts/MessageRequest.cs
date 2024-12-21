namespace composition.Contracts;

public record class MessageRequest
{
    public required string Content { get; set; }
    public required string SentAt { get; set; }
}
