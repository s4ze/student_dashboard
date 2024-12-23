namespace profile.Contracts;

public record class EditMessageRequest
{
    public required string Content { get; set; }
}
