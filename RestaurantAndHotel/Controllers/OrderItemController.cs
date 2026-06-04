using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderItemController(OrderItemServices services) : ControllerBase
    {
       private readonly OrderItemServices _service = services;


        [HttpGet]
        public IActionResult GetAllOrderItems()
        {
            try
            {
                var orderItems = _service.GetAllOrderItems();
                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetOrderItemById(int id)
        {
            try
            {
                var orderItem = _service.GetOrderItemById(id);
                return Ok(orderItem);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult AddOrderItem([FromBody] OrderItem orderItem)
        {
            try
            {
                _service.AddOrderItem(orderItem);
                return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.Id }, orderItem);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateOrderItem(int id, [FromBody] OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }
            try
            {
                _service.UpdateOrderItem(orderItem);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            try
            {
                _service.DeleteOrderItem(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
