using BLL.DTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class RestaurantTableServices(IRestaurantTableRepository repo) : IRestaurantTableService
    {
        private readonly IRestaurantTableRepository _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public void AddRestaurantTable(RestaurantTable restaurantTable)
        {
            _repo.AddRestaurantTable(restaurantTable);
        }

        public void DeleteRestaurantTable(int id)
        {
            _repo.DeleteRestaurantTable(id);
        }

        public List<RestaurantTable> GetAllRestaurantTables()
        {
            return _repo.GetAllRestaurantTables();
        }

        public RestaurantTable GetRestaurantTableById(int id)
        {
            return _repo.GetRestaurantTableById(id);
        }

        public Task<string> GetTableStatus(int tableId)
        {
            return _repo.GetTableStatus(tableId);
        }

        public Task<string> GetTableStatusByDate(int tableId, DateTime date)
        {
            return _repo.GetTableStatusByDate(tableId, date);
        }

        public void UpdateRestaurantTable(RestaurantTable restaurantTable)
        {
            _repo.UpdateRestaurantTable(restaurantTable);
        }

        public async Task<TableCurrentOrderDto?> GetCurrentOrderByTableId(int tableId)
        {
            var order = await _repo.GetActiveOrderByTableId(tableId);

            if (order == null)
                return null;

            return new TableCurrentOrderDto
            {
                TableId = tableId,
                OrderId = order.Id,
                TotalPrice = order.TotalPrice,
                OrderStatus = order.Status,
                IsPaid = order.Payments.Sum(p => p.Amount) >= order.TotalPrice,

                Items = [.. order.OrderItems.Select(oi => new MenuItemDto
                {
                    Name = oi.MenuItem!.Name,
                    Price = oi.MenuItem.Price,
                    Quantity = oi.Quantity
                })]
            };
        }

        public async Task<List<MostOrderedMenuItemDto>> GetTopMostOrderedMenuItems(int count)
        {
            var result = await _repo.GetTopMostOrderedMenuItems(count);

            return result.Select(r => new MostOrderedMenuItemDto
            {
                Name = r.Item.Name,
                Price = r.Item.Price,
                ImageUrl=r.Item.ImageUrl,
                TotalQuantitySold = r.TotalQuantity
            }).ToList();
        }
    }
}
