using payments.Contracts;
using payments.Data;
using payments.Models;

namespace payments.Services;

public class PaymentsService(DataContext context)
{
    private readonly DataContext _context = context;
    public List<Payment> GetPayments(Guid userId)
    {
        return _context.Payments.Where(p => p.UserId == userId && p.Status == 'o').ToList();
    }
    public PayRequest PayPayment(Guid paymentId, double amount)
    {
        var payment = GetPayment(paymentId);
        if (payment.Amount <= amount || payment.Status == 'c')
        {
            payment.Status = 'c';
            payment.Amount = 0;
        }
        else
            payment.Amount -= amount;
        _context.SaveChanges();
        return new PayRequest() { Amount = payment.Amount };
    }
    public bool CheckIfPaymentExists(Guid paymentId)
    {
        return _context.Payments.Any(p => p.PaymentId == paymentId);
    }
    public Payment GetPayment(Guid paymentId)
    {
        return _context.Payments.First(p => p.PaymentId == paymentId);
    }
    public Payment CreatePayment(CreatePaymentRequest data)
    {
        var payment = new Payment()
        {
            PaymentId = new Guid(),
            UserId = new Guid(data.UserId),
            Amount = data.Amount,
            Status = 'o',
            PaymentDate = DateTime.Now.ToString("MM-dd-yyyy HH:mmK")
        };
        _context.Payments.Add(payment);
        _context.SaveChanges();
        return payment;
    }
}
