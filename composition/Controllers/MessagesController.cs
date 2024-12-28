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

            // send req to messages and check if users are existingreturn NoContent();

            /*var responseReceiver = await(await client.GetAsync(string.Format("http://localhost:ProfileServicePort/api/Profile/userexists/{0}", receiverId))).Content.ReadAsStringAsync();
            var responseSender = await(await client.GetAsync(string.Format("http://localhost:ProfileServicePort/api/Profile/userexists/{0}", data.SenderId))).Content.ReadAsStringAsync();
            if (responseReceiver != "true" || responseSender != "true") return BadRequest("Receiver or sender doesn't exist");

            try
            {
                var message = _messagesService.CreateMessage(receiverId, data);

                return Ok(message);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    error = ex.Message,
                };

                return BadRequest(result);
            }*/

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
