using DAL.Models;
 

namespace DAL.Interfaces
{
    public interface IPaymentRepository
    {
        List<Payment> GetAllPayments();
            Payment GetPaymentById(int id);
            void AddPayment(Payment payment);
            void UpdatePayment(Payment payment);
            void DeletePayment(int id);
    }
}
