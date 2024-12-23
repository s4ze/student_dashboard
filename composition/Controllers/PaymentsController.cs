using composition.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        /// <summary>
        /// Get user's paymentIds
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetPayments([FromRoute] string userId)
        {
            return Ok();
        }
        /// <summary>
        /// Pay for a bill by paymentId
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Pay([FromBody] PayRequest data)
        {
            return Ok();
        }
        /// <summary>
        /// Creates payment for user
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
