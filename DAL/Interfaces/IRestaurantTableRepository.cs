using DAL.Models;
namespace DAL.Interfaces
{
    public interface IRestaurantTableRepository
    {
        List<RestaurantTable> GetAllRestaurantTables();
        RestaurantTable GetRestaurantTableById(int id);
        void AddRestaurantTable(RestaurantTable restaurantTable);
        void UpdateRestaurantTable(RestaurantTable restaurantTable);
        void DeleteRestaurantTable(int id);

        Task<string> GetTableStatus(int tableId);
        Task<string> GetTableStatusByDate(int tableId, DateTime date);

        Task<Order?> GetActiveOrderByTableId(int tableId);
        Task<List<(MenuItem Item, int TotalQuantity)>> GetTopMostOrderedMenuItems(int count);

    }
}
