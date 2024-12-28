using Microsoft.AspNetCore.Mvc;

namespace composition.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Обработка ошибок
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
