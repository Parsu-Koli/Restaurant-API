using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }          
        public string? FullName { get; set; }       
        public string? Email { get; set; }             
        public string? PhoneNumber { get; set; }      
        public string? Address { get; set; }         
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; } =[];
        [JsonIgnore]
        public ICollection<ReservationTable>? Reservations { get; set; } =[];
        [JsonIgnore]
        public ICollection<Payment>? Payments { get; set; } = [];
    }
}
