using BLL.DTOs;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IRestaurantTableService
    {
        List<RestaurantTable> GetAllRestaurantTables();
        RestaurantTable GetRestaurantTableById(int id);
        void AddRestaurantTable(RestaurantTable restaurantTable);
        void UpdateRestaurantTable(RestaurantTable restaurantTable);
        void DeleteRestaurantTable(int id);

        Task<string> GetTableStatus(int tableId);
        Task<string> GetTableStatusByDate(int tableId, DateTime date);

        Task<TableCurrentOrderDto?> GetCurrentOrderByTableId(int tableId);
        Task<List<MostOrderedMenuItemDto>> GetTopMostOrderedMenuItems(int count);
    }
}