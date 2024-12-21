namespace composition.Contracts;

public record class PayRequest
{
    public required string PaymentId { get; set; }
    public required string Amount { get; set; }
}
