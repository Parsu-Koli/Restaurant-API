using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class ReservationTable
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime ReservedDate { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; }
        [JsonIgnore]
        public RestaurantTable? RestaurantTable { get; set; }
    }
}
