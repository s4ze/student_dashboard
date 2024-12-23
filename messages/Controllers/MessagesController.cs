using System.Net;
using profile.Contracts;
using profile.Data;
using profile.Models;
using profile.Services;
using Microsoft.AspNetCore.Mvc;

namespace profile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController(MessagesService messagesService) : ControllerBase
    {
        private readonly MessagesService _messagesService = messagesService;
        private static readonly HttpClient client = new();
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
        public async Task<IActionResult> SendMessage([FromRoute] string receiverId, [FromBody] MessageRequest data)
        {
            // send req to profile and check if users are existingreturn NoContent();

            var responseReceiver = await (await client.GetAsync(string.Format("http://localhost:ProfileServicePort/api/Profile/userexists/{0}", receiverId))).Content.ReadAsStringAsync();
            var responseSender = await (await client.GetAsync(string.Format("http://localhost:ProfileServicePort/api/Profile/userexists/{0}", data.SenderId))).Content.ReadAsStringAsync();
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
