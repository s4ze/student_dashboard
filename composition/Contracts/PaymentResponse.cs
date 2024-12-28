namespace composition.Contracts;

public record class PaymentResponse
{
    public string PaymentId { get; set; }
    public string UserId { get; set; }
    public double Amount { get; set; }
    public char Status { get; set; }
    public string PaymentDate { get; set; }
}
