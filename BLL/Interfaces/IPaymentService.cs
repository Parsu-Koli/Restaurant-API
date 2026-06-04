 using BLL.DTOs;

namespace BLL.Interfaces
{
     

    public interface IPaymentService
    {
        List<PaymentDto> GetAllPayments();
        PaymentDto GetPaymentById(int id);
        void AddPayment(CreatePaymentDto dto);
        void UpdatePayment(int id, UpdatePaymentDto dto);
        void DeletePayment(int id);
    }
}
