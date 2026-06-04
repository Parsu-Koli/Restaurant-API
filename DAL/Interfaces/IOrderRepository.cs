using DAL.Models;
namespace DAL.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        void DeleteOrder(int id);
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        void UpdateOrder(Order order);
        string GetTableStatus(int tableId);

        RestaurantTable? GetTableById(int tableId);
        MenuItem? GetMenuItemById(int id);
        void UpdateTable(RestaurantTable table);
        Order? GetOrderByTableId(int tableId);
    }
}
