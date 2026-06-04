using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int customerId { get; set; }

        public string? CustomerName { get; set; }
        public int TableNumber { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidAt { get; set; }

        public List<OrderItemResponseDto>? Items { get; set; }
    }

   
}
