using Arizona.APIs.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arizona.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] //to un document this controller
    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            if (code == 400)
                return BadRequest(new ApiResponse(400));
            else if (code == 401)
                return NotFound(new ApiResponse(401));
            else if (code == 404)
                return NotFound(new ApiResponse(code));
            else
                return StatusCode(code);

        }
    }
}
