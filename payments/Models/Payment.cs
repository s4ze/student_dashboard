using System.ComponentModel.DataAnnotations.Schema;

namespace payments.Models;

public class Payment
{
    public Guid PaymentId { get; set; }
    public Guid UserId { get; set; }
    [Column(TypeName = "NUMERIC(9,2)")] public double Amount { get; set; }
    public required char Status { get; set; } = 'o';
    [Column(TypeName = "VARCHAR(25)")] public string PaymentDate { get; set; }
}
