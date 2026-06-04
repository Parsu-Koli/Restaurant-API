using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
namespace DAL.Implementations
{
    public class OrderItemsRepository(AppDbContext context) : IOrderItemRepository
    {
        private readonly AppDbContext _context = context;

        public void AddOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
        }

        public void DeleteOrderItem(int id)
        {
            var orderItem = _context.OrderItems.Find(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"OrderItem with id {id} not found.");
            }
        }

        public List<OrderItem> GetAllOrderItems()
        {
            return [.._context.OrderItems];
        }

        public OrderItem GetOrderItemById(int id)
        {
            var result = _context.OrderItems.Find(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception($"OrderItem with id {id} not found.");
            }
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            var existingOrderItem = _context.OrderItems.Find(orderItem.Id);
            if (existingOrderItem != null)
            {
                existingOrderItem.OrderId = orderItem.OrderId;
                existingOrderItem.MenuItemId = orderItem.MenuItemId;
                existingOrderItem.Quantity = orderItem.Quantity;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"OrderItem with id {orderItem.Id} not found.");
            }
        }
    }
}
