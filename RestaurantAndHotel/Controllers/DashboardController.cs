using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController(DashboardServices services) : ControllerBase
    {
        private readonly DashboardServices _services = services;

        [HttpGet("Total-tables")]
        public IActionResult GetTotalTables()
        {
            var totalTables = _services.Total_tables();
            return Ok(totalTables);

        }

        [HttpGet("Occupied-tables")]
        public IActionResult GetOccupiedTables()
        {
            var result = _services.Occupied_table();
            return Ok(result);
        }

        [HttpGet("Todays-Order")]
        public IActionResult Order()
        {
            var result = _services.Todays_orders();
            return Ok(result);
        }

        [HttpGet("Todays-Revenue")]
        public IActionResult Revenue()
        {
            var result = _services.Today_revenue();
            return Ok(result);
        }
         
    }
}
