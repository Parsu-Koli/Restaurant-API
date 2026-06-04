using System.Security.Principal;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public Role? Role { get; set; }

       

    }
}
