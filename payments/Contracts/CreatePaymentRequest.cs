namespace payments.Contracts;

public record class CreatePaymentRequest
{
    public required string UserId { get; set; }
    public required double Amount { get; set; }
}
