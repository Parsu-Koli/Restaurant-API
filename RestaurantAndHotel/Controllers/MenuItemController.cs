using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuItemController(IMenuItemService service) : ControllerBase
    {
        private readonly IMenuItemService _service = service;

        [HttpGet("search")]
        public async Task<IActionResult> Search(string term)
        {
            var result = await _service.SearchMenuItemsAsync(term);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllMenuItems()
        {
            var result = _service.GetAllMenuItems();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuItemById(int id)
        {
            try
            {
                var result = _service.GetMenuItemById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // ✅ Image upload via Swagger
        [HttpPost]
        public IActionResult CreateMenuItem([FromForm] CreateMenuItemDto dto)
        {
            _service.AddMenuItem(dto);
            return Ok("Menu item created successfully.");
        }

        // ✅ Also allow image update
        [HttpPut("{id}")]
        public IActionResult UpdateMenuItem(int id, [FromForm] CreateMenuItemDto dto)
        {
            try
            {
                _service.UpdateMenuItem(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenuItem(int id)
        {
            try
            {
                _service.DeleteMenuItem(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // ✅ Fixed route
        [HttpGet("category/{categoryId}")]
        public IActionResult GetMenuItemByCategory(int categoryId)
        {
            var result = _service.GetMenuItemByCategory(categoryId);
            return Ok(result);
        }
    }
}