using BLL.DTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class PaymentServices(
        IPaymentRepository paymentRepo,
        IOrderRepository orderRepo,
        IRestaurantTableRepository tableRepo
    ) : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo = paymentRepo
            ?? throw new ArgumentNullException(nameof(paymentRepo));

        private readonly IOrderRepository _orderRepo = orderRepo
            ?? throw new ArgumentNullException(nameof(orderRepo));

        private readonly IRestaurantTableRepository _tableRepo = tableRepo
            ?? throw new ArgumentNullException(nameof(tableRepo));

        public List<PaymentDto> GetAllPayments()
        {
            return [.. _paymentRepo.GetAllPayments()
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    OrderId = p.OrderId,
                    CustomerId = p.CustomerId,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                    PaidAt = p.PaidAt
                })];
        }

        public PaymentDto GetPaymentById(int id)
        {
            var p = _paymentRepo.GetPaymentById(id)
                ?? throw new Exception($"Payment with id {id} not found.");

            return new PaymentDto
            {
                Id = p.Id,
                OrderId = p.OrderId,
                CustomerId = p.CustomerId,
                Amount = p.Amount,
                PaymentMethod = p.PaymentMethod,
                PaidAt = p.PaidAt
            };
        }

        public void AddPayment(CreatePaymentDto dto)
        {
            var order = _orderRepo.GetOrderById(dto.OrderId)
                ?? throw new Exception($"Order with id {dto.OrderId} not found.");

            if (order.Status == "Completed")
                throw new Exception("Order already completed.");

            // Get already paid amount from repository
            var totalPaid = _paymentRepo
                .GetAllPayments()
                .Where(p => p.OrderId == order.Id)
                .Sum(p => p.Amount);

            var remainingAmount = order.TotalPrice - totalPaid;

            if (remainingAmount <= 0)
                throw new Exception("Order is already fully paid.");

            var payment = new Payment
            {
                OrderId = order.Id,
                CustomerId = dto.CustomerId,
                Amount = remainingAmount,
                PaymentMethod = dto.PaymentMethod,
                PaidAt = DateTime.UtcNow
            };

            _paymentRepo.AddPayment(payment);

            // Mark order completed
            order.Status = "Completed";
            _orderRepo.UpdateOrder(order);

            // Free the table
            var table = _tableRepo.GetRestaurantTableById(order.TableId);
            if (table != null)
            {
                table.Status = "Available";
                _tableRepo.UpdateRestaurantTable(table);
            }
        }

        public void UpdatePayment(int id, UpdatePaymentDto dto)
        {
            var existingPayment = _paymentRepo.GetPaymentById(id)
                ?? throw new Exception($"Payment with id {id} not found.");

            existingPayment.OrderId = dto.OrderId;
            existingPayment.CustomerId = dto.CustomerId;
            existingPayment.Amount = dto.Amount;
            existingPayment.PaymentMethod = dto.PaymentMethod;
            existingPayment.PaidAt = dto.PaidAt;

            _paymentRepo.UpdatePayment(existingPayment);
        }

        public void DeletePayment(int id)
        {
            _paymentRepo.DeletePayment(id);
        }

        
    }
}