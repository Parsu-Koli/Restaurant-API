using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockItemController(StockItemServices services) : ControllerBase
    {
        private readonly StockItemServices _services = services ?? throw new ArgumentNullException(nameof(services));

        [HttpGet]
        public IActionResult GetAllStockItems()
        {
            var stockItems = _services.GetAllStockItems();
            return Ok(stockItems);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockItemById(int id)
        {
            try
            {
                var stockItem = _services.GetStockItemById(id);
                return Ok(stockItem);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateStockItem([FromBody] DAL.Models.StockItem stockItem)
        {
            _services.AddStockItem(stockItem);
            return CreatedAtAction(nameof(GetStockItemById), new { id = stockItem.StockItemId }, stockItem);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStockItem(int id, [FromBody] DAL.Models.StockItem stockItem)
        {
            if (id != stockItem.StockItemId)
            {
                return BadRequest("ID mismatch");
            }
            try
            {
                _services.UpdateStockItem(stockItem);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStockItem(int id)
        {
            try
            {
                _services.DeleteStockItem(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
