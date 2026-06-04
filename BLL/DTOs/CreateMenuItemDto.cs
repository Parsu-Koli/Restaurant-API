using Microsoft.AspNetCore.Http;
namespace BLL.DTOs
{
    public class CreateMenuItemDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        public IFormFile? Image { get; set; }  
    }
}
