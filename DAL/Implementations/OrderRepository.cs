using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace DAL.Implementations
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));


        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var result = _context.Orders.Find(id);
            if (result != null)
            {
                _context.Orders.Remove(result);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Order with id {id} not found.");
            }
        }

        public List<Order> GetAllOrders()
        {
            return [.._context.Orders
    .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.MenuItem)
    .Include(o => o.Customer)
    .Include(o => o.RestaurantTable)
    .Include(o => o.Payments)
    ];
        }

        public Order GetOrderById(int id)
        {
            var result = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Include(o => o.Customer)
                .Include(o => o.RestaurantTable)
                .Include(o => o.Payments)
                .FirstOrDefault(o => o.Id == id);

            return result ?? throw new Exception($"Order with id {id} not found.");
        }

        public Order? GetOrderByTableId(int tableId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Include(o => o.Customer)
                .Include(o => o.RestaurantTable)
                .Include(o => o.Payments)
                .Where(o => o.TableId == tableId && o.Status == "Active")
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefault();
        }

        public void UpdateOrder(Order order)
        {
            var existingOrder = _context.Orders.Find(order.Id);
            if (existingOrder != null)
            {
                existingOrder.TotalPrice = order.TotalPrice;
                existingOrder.TableId = order.TableId;
                existingOrder.Status = order.Status;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Order with id {order.Id} not found.");
            }
        }

        public string GetTableStatus(int tableId)
        {
            var activeOrder = _context.Orders
                .Where(o => o.TableId == tableId &&
                       (o.Status == "Active" || o.Status == "Reserved"))
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefault();

            if (activeOrder == null)
                return "Available";

            if (activeOrder.Status == "Reserved")
                return "Reserved";

            if (activeOrder.Status == "Active")
                return "Occupied";

            return "Available";
        }


        public RestaurantTable? GetTableById(int tableId)
        {
            return _context.RestaurantTables
                           .FirstOrDefault(t => t.TableId == tableId);
        }

        public MenuItem? GetMenuItemById(int id)
        {
            return _context.MenuItems
                           .FirstOrDefault(m => m.Id == id);
        }

        public void UpdateTable(RestaurantTable table)
        {
            _context.RestaurantTables.Update(table);
            _context.SaveChanges();
        }
    }
}
