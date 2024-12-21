namespace composition.Contracts;

public record class CreatePaymentRequest
{
    public required string UserId { get; set; }
    public required string Amount { get; set; }
}
