using Microsoft.AspNetCore.Http;
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
        [Route("{userId}")]
        public IActionResult PayPayment([FromRoute] string userId, [FromBody] PayRequest data)
        {
            _paymentsService.PayPayment(new Guid(userId), data);
            return Ok();
        }
        [HttpPost]
        [Route("bill")]
        public IActionResult CreatePayment([FromBody] CreatePaymentRequest data)
        {
            return Ok(_paymentsService.CreatePayment(data));
        }
    }
}
