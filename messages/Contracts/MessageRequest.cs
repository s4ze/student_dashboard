namespace profile.Contracts;

public record class MessageRequest
{
    public required string SenderId { get; set; }
    public required string Content { get; set; }
}
