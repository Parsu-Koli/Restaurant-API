namespace DAL.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string PaymentMethod { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }

        public Order? Order { get; set; }
        public Customer? Customer { get; set; }
    }
}
