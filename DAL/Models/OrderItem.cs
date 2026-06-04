using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }
        [JsonIgnore]
        public MenuItem? MenuItem { get; set; }
    }
}
