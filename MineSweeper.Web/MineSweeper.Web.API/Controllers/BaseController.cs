using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MineSweeper.Web.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ObjectResult Error(string message = null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, message);
        }
    }
}
