using DAL.Models;
namespace DAL.Interfaces
{
    public interface IOrderItemRepository
    {
        List<OrderItem> GetAllOrderItems();
        OrderItem GetOrderItemById(int id);
        void AddOrderItem(OrderItem orderItem);
        void UpdateOrderItem(OrderItem orderItem);
        void DeleteOrderItem(int id);
    }
}
