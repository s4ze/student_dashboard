namespace composition.Contracts;

public record class CreateMessageRequest
{
    public required string SenderId { get; set; }
    public required string Content { get; set; }
}
