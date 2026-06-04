using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CreatePaymentDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string? PaymentMethod { get; set; }
    }

    public class PaymentDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime PaidAt { get; set; }
    }

    public class UpdatePaymentDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
