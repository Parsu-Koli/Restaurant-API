namespace DAL.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int TableId { get; set; }

        public decimal TotalPrice { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

       
        public Customer? Customer { get; set; }

    
        public RestaurantTable? RestaurantTable { get; set; }

    
        public ICollection<OrderItem> OrderItems { get; set; } = [];

 
        public ICollection<Payment> Payments { get; set; } = [];
    }
}