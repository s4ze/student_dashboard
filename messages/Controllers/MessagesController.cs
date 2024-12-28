using messages.Contracts;
using messages.Services;
using Microsoft.AspNetCore.Mvc;

namespace messages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController(MessagesService messagesService) : ControllerBase
    {
        private readonly MessagesService _messagesService = messagesService;
        [HttpGet]
        [Route("one/{messageId}")]
        public IActionResult GetMessage([FromRoute] string messageId)
        {
            try
            {
                if (_messagesService.CheckIfMessageExists(new Guid(messageId)))
                {
                    return Ok(_messagesService.GetMessage(new Guid(messageId)));
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{receiverId}")]
        public IActionResult GetMessages([FromRoute] string receiverId)
        {
            try
            {
                var result = _messagesService.GetMessages(new Guid(receiverId));
                if (result.Count > 0) return Ok(result);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("{receiverId}")]
        public IActionResult SendMessage([FromRoute] string receiverId, [FromBody] MessageRequest data)
        {
            try
            {
                var message = _messagesService.CreateMessage(new Guid(receiverId), data);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("{messageId}")]
        public IActionResult EditMessage([FromRoute] string messageId, [FromBody] EditMessageRequest data)
        {
            try
            {
                if (_messagesService.CheckIfMessageExists(new Guid(messageId)))
                {
                    var message = _messagesService.EditMessage(new Guid(messageId), data.Content);
                    return Ok(message);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{messageId}")]
        public IActionResult RemoveMessage([FromRoute] string messageId)
        {
            try
            {
                var result = _messagesService.RemoveMessage(new Guid(messageId));
                return result ? Ok() : NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
