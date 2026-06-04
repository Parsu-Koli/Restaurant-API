using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class RestaurantTable
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public string? LocationZone { get; set; }
        public string? Status { get; set; } = "Available";
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; } = [];

        [JsonIgnore]
        public ICollection<ReservationTable>? Reservations { get; set; } = [];

    }
}
