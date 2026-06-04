using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class OrderItemServices(IOrderItemRepository repo) : IOrderItemRepository
    {
        private readonly IOrderItemRepository _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public void AddOrderItem(OrderItem orderItem)
        {
            _repo.AddOrderItem(orderItem);
        }

        public void DeleteOrderItem(int id)
        {
            _repo.DeleteOrderItem(id);
        }

        public List<OrderItem> GetAllOrderItems()
        {
            return _repo.GetAllOrderItems();
        }

        public OrderItem GetOrderItemById(int id)
        {
            return _repo.GetOrderItemById(id);
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            _repo.UpdateOrderItem(orderItem);
        }
    }
}
