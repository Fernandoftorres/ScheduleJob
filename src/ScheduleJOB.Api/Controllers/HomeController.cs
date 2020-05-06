using Microsoft.AspNetCore.Mvc;

namespace ScheduleJOB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("Home")]
        public IActionResult Get()
        {
            return new OkResult();
        }
    }
}