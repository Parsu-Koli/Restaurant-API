using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController(CategoryServices services) : ControllerBase
    {
        private readonly CategoryServices _service = services;

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = _service.GetAllCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                var category = _service.GetCategoryById(id);
                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] Category category)
        {
            try
            {
                _service.AddCategory(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }
            try
            {
                _service.UpdateCategory(category);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _service.DeleteCategory(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
