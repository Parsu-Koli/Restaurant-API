using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
namespace DAL.Implementations
{
    public class PaymentRepository(AppDbContext context) : IPaymentRepository
    {
        private readonly AppDbContext _context = context;

        public List<Payment> GetAllPayments()
        {
            return [.. _context.Payments];
        }

        public Payment GetPaymentById(int id)
        {
            return _context.Payments.First(p => p.Id == id);
        }

        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void UpdatePayment(Payment payment)
        {
            var existingPayment = _context.Payments.FirstOrDefault(p => p.Id == payment.Id);
            if (existingPayment != null)
            {
                existingPayment.OrderId = payment.OrderId;
                existingPayment.Amount = payment.Amount;
                existingPayment.PaidAt = payment.PaidAt;
                existingPayment.PaymentMethod = payment.PaymentMethod;
                _context.SaveChanges();
            }
        }

        public void DeletePayment(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
        }
    }
}


