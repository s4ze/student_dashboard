using composition.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    /// <summary>
    /// Сервис сообщений
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        /// <summary>
        /// Get user's messages
        /// </summary>
        /// <param name="receiverId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{receiverId}")]
        public IActionResult GetMessages([FromRoute] string receiverId)
        {
            // send req to message service
            // return message

            return Ok();
        }
        /// <summary>
        /// Send a message to user
        /// </summary>
        /// <param name="receiverId">Продукт</param>
        /// <returns>HJjkjl</returns>
        [HttpPost]
        [Route("{receiverId}")]
        public IActionResult SendMessage([FromRoute] string receiverId, [FromBody] MessageRequest data)
        {
            // send req to Message service
            // return message

            return Ok();
        }
        /// <summary>
        /// Edit message
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{messageId}")]
        public IActionResult EditMessage([FromRoute] string messageId, [FromBody] string content)
        {
            // send req to get mesage and,
            // check if message's senderId is equal to JWT's or Admin
            // send req to Message service
            // return message

            return Ok();
        }
        /// <summary>
        /// Remove messages
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{messageId}")]
        public IActionResult RemoveMessage([FromRoute] string messageId)
        {
            // check if user is sender or Admin on auth service
            // send req to message service
            // return message

            return Ok();
        }
    }
}
