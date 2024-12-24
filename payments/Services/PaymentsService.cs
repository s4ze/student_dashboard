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
    public bool PayPayment(Guid paymentId, PayRequest data)
    {
        var payment = GetPayment(paymentId);
        if (payment.Amount <= data.Amount) payment.Status = 'c';
        payment.Amount -= data.Amount;
        _context.SaveChanges();
        return payment.Status == 'c';
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
