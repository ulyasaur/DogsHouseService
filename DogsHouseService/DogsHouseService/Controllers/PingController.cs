using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DogsHouseService.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Ping()
        {
            return Ok("Dogshouseservice.Version1.0.1");
        }
    }
}
