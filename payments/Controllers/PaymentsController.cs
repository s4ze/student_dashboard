using Microsoft.AspNetCore.Mvc;
using payments.Contracts;
using payments.Services;

namespace payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(PaymentsService paymentsService) : ControllerBase
    {
        private readonly PaymentsService _paymentsService = paymentsService;
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetPayments([FromRoute] string userId)
        {
            return Ok(_paymentsService.GetPayments(new Guid(userId)));
        }
        [HttpPost]
        [Route("{paymentId}")]
        public IActionResult PayPayment([FromRoute] string paymentId, [FromBody] PayRequest data)
        {
            if (_paymentsService.CheckIfPaymentExists(new Guid(paymentId)))
            {
                return Ok(_paymentsService.PayPayment(new Guid(paymentId), data.Amount));
            }
            return NoContent();
        }
        [HttpPost]
        [Route("bill")]
        public IActionResult CreatePayment([FromBody] CreatePaymentRequest data)
        {
            return Ok(_paymentsService.CreatePayment(data));
        }
    }
}
