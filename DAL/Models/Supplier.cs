using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string? Name { get; set; }
        public string? ContactInfo { get; set; }

        [JsonIgnore]
        public ICollection<StockItem>? StockItems { get; set; } = [];
    }
}
