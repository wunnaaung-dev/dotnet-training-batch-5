using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WNADotNetCore.SnakeLadderGame.Domain.Model;

namespace WNADotNetCore.SnakeLadderGame.REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult Execute<T>(Result<T> model)
        {
            if (model.IsValidationError)
            {
                return BadRequest(model);
            }

            if (model.IsNotFoundError)
            {
                return NotFound(model);
            }

            if (model.IsDatabaseError)
            {
                return StatusCode(500, "Database Error Occured");
            }

            return Ok(model);
        }
    }
}
