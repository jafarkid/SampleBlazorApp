using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApiApp.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ActionResult HandleException(Exception ex, string errorMessage)
        {
            // Log the exception for further investigation (optional)
            // logger.LogError(ex, errorMessage);

            // Return 500 Internal Server Error with the provided error message
            return StatusCode(500, errorMessage);
        }
    }
}
