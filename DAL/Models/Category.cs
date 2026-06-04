using System.Text.Json.Serialization;
namespace DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public ICollection<MenuItem>? MenuItems { get; set; } = [];
    }
}
