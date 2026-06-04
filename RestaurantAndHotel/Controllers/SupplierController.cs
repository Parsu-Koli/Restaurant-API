using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController(SupplierServices services) : ControllerBase
    {
        private readonly SupplierServices _services = services ?? throw new ArgumentNullException(nameof(services));

        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            var suppliers = _services.GetAllSuppliers();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public IActionResult GetSupplierById(int id)
        {
            try
            {
                var supplier = _services.GetSupplierById(id);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateSupplier([FromBody] Supplier supplier)
        {
            _services.AddSupplier(supplier);
            return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.SupplierId }, supplier);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSupplier(int id, [FromBody] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return BadRequest("ID mismatch");
            }
            try
            {
                _services.UpdateSupplier(supplier);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            try
            {
                _services.DeleteSupplier(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
