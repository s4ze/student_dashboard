namespace payments.Contracts;

public record class PayRequest
{
    public required double Amount { get; set; }
}
