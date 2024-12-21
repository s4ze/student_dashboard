using composition.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetPayments([FromRoute] string userId)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult Pay([FromBody] PayRequest data)
        {
            return Ok();
        }
        [HttpPost]
        [Route("bill")]
        public IActionResult CreatePayment([FromBody] CreatePaymentRequest data)
        {
            // send req to Admin and authorize
            // create payment in Payments service

            return Ok();
        }
    }
}
