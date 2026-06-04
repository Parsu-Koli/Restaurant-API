using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantTableController(RestaurantTableServices services) : ControllerBase
    {
        private readonly RestaurantTableServices _services = services ?? throw new ArgumentNullException(nameof(services));

        [HttpGet]
        public IActionResult GetAllRestaurantTables()
        {
            var tables = _services.GetAllRestaurantTables();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        public IActionResult GetRestaurantTableById(int id)
        {
            try
            {
                var table = _services.GetRestaurantTableById(id);
                return Ok(table);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateRestaurantTable([FromBody] RestaurantTable restaurantTable)
        {
            _services.AddRestaurantTable(restaurantTable);
            return CreatedAtAction(nameof(GetRestaurantTableById), new { id = restaurantTable.TableId }, restaurantTable);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRestaurantTable(int id, [FromBody] RestaurantTable restaurantTable)
        {
            if (id != restaurantTable.TableId)
            {
                return BadRequest("ID mismatch");
            }
            try
            {
                _services.UpdateRestaurantTable(restaurantTable);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurantTable(int id)
        {
            try
            {
                _services.DeleteRestaurantTable(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetTableStatusBy")]
        public async Task<IActionResult> GetTableStatus(int id)
        {
            var status = await _services.GetTableStatus(id);
            return Ok(status);
        }

        [HttpGet("GetStatusByDate")]
        public async Task<IActionResult> GetStatusByDate(int id, DateTime date)
        {
            var status = await _services.GetTableStatusByDate(id, date);
            return Ok(status);
        }

        [HttpGet("current-order/{tableId}")]
        public async Task<IActionResult> GetCurrentOrder(int tableId)
        {
            var result = await _services.GetCurrentOrderByTableId(tableId);

            if (result == null)
                return NotFound("No active order found.");

            return Ok(result);
        }

        [HttpGet("top-6-items")]
        public async Task<IActionResult> GetTopSixItems()
        {
            var result = await _services.GetTopMostOrderedMenuItems(6);
            return Ok(result);
        }
    }
}
