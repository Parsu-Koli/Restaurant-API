using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class OrderItemResponseDto
    {
        public string? MenuItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
