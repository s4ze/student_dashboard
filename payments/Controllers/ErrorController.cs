using Microsoft.AspNetCore.Mvc;

namespace payments.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
