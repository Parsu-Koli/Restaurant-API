using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController(RoleServices services) : ControllerBase
    {
        private readonly RoleServices _services = services ?? throw new ArgumentNullException(nameof(services));

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _services.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoleById(int id)
        {
            try
            {
                var role = _services.GetRoleById(id);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateRole([FromBody] Role role)
        {
            _services.AddRole(role);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody] Role role)
        {
            if (id != role.Id)
            {
                return BadRequest("ID mismatch");
            }
            try
            {
                _services.UpdateRole(role);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                _services.DeleteRole(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
