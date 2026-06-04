using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ReservationWithTableDto
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public string? Status { get; set; }
        public DateTime ReservedDate { get; set; }
        public int TableNumber { get; set; }
    }
}
