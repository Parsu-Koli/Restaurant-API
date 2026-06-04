using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string? ImageUrl { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }
        [JsonIgnore]
        public ICollection<OrderItem>? OrderItems { get; set; } = [];
    }
}
