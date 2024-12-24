using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [ApiController]
    [Route("/error")]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
