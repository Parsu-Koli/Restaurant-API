using BLL.DTOs;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserServices services, IConfiguration configuration) : ControllerBase
    {
        private readonly UserServices _services = services;
        private readonly IConfiguration _configuration = configuration;

        // =============================
        // LOGIN (JWT TOKEN)
        // =============================
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _services.GetAllUsers()
                .FirstOrDefault(u =>
                    u.Email == request.Email &&
                    u.Password == request.Password);

            if (user == null)
                return Unauthorized("Invalid email or password");

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                userId = user.Id,
                name = user.FullName,
                email = user.Email,
                roleId = user.RoleId
            });
        }

        // =============================
        // GENERATE JWT TOKEN
        // =============================
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim("RoleId", user.RoleId.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // =============================
        // GET ALL USERS
        // =============================
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _services.GetAllUsers();
            return Ok(users);
        }

        // =============================
        // GET USER BY ID
        // =============================
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _services.GetUserById(id);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        // =============================
        // CREATE USER
        // =============================
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            _services.AddUser(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // =============================
        // UPDATE USER
        // =============================
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest("ID mismatch");

            _services.UpdateUser(user);

            return NoContent();
        }

        // =============================
        // DELETE USER
        // =============================
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _services.DeleteUser(id);

            return NoContent();
        }
    }
}