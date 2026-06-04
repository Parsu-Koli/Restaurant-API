using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAndHotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllPayments()
        {
            return Ok(_service.GetAllPayments());
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id)
        {
            return Ok(_service.GetPaymentById(id));
        }

        [HttpPost]
        public IActionResult AddPayment([FromBody] CreatePaymentDto dto)
        {
            _service.AddPayment(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, [FromBody] UpdatePaymentDto dto)
        {
            _service.UpdatePayment(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            _service.DeletePayment(id);
            return NoContent();
        }
    }
}
