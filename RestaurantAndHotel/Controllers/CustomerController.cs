using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController(CustomerServices services) : ControllerBase
    {
        private readonly CustomerServices _service = services;

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = _service.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                var customer = _service.GetCustomerById(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            try
            {
                _service.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }
            try
            {
                _service.UpdateCustomer(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                _service.DeleteCustomer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}