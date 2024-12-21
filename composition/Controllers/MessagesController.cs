using composition.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpPost]
        [Route("{userId}")]
        public IActionResult SendMessage([FromRoute] string userId, [FromBody] MessageRequest data)
        {
            // send req to Message service
            // return message

            return Ok();
        }
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetNewMessage([FromRoute] string userId, [FromBody] MessageRequest data)
        {
            // send req to message service
            // return message

            return Ok();
        }
        [HttpDelete]
        [Route("{userId}")]
        public IActionResult RemoveMessage([FromRoute] string userId)
        {
            // send req to message service
            // return message

            return Ok();
        }
    }
}
