namespace authorization.Contracts;

public record class TokenRequest
{
    public required string Token { get; set; }

}
