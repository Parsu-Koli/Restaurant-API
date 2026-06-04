using BLL.DTOs;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController(IOrderServices service) : ControllerBase
    {
        private readonly IOrderServices _service = service;

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _service.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                var order = _service.GetOrderById(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] CreateOrderDto dto)
        {
            try
            {
                _service.AddOrder(dto);
                return Ok(new { message = "Order created successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, UpdateOrderDto dto)
        {
            if (id != dto.OrderId)
                return BadRequest("Order ID mismatch");

            _service.UpdateOrder(dto);

            return Ok(new { message = "Order updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _service.DeleteOrder(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("table-status/{tableId}")]
        public IActionResult GetTableStatus(int tableId)
        {
            var status = _service.GetTableStatus(tableId);
            return Ok(status);
        }

        [HttpGet("by-table/{tableId}")]
        public IActionResult GetOrderByTableId(int tableId)
        {
            var order = _service.GetOrderByTableId(tableId);
            if (order == null)
                return NotFound($"No active order found for table {tableId}.");
            return Ok(order);
        }
    }
}