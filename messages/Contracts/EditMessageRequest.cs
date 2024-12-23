namespace messages.Contracts;

public record class EditMessageRequest
{
    public required string Content { get; set; }
}
