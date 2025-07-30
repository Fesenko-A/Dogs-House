using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers {
    [Route("")]
    [ApiController]
    public class UtilityController : ControllerBase {
        [HttpGet("Ping")]
        public ActionResult Get() => Ok("Dogshouseservice.Version1.0.1");
    }
}
