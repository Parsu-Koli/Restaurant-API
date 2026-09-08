using Microsoft.AspNetCore.Mvc;

namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Health()
        {
            return Ok("Healthy");
        }
    }
}
