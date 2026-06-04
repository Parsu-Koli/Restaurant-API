using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace DAL.Implementations
{
    public class RestaurantTableRepository(AppDbContext context) : IRestaurantTableRepository
    {
        private readonly AppDbContext _context = context?? throw new ArgumentNullException(nameof(context));

        public List<RestaurantTable> GetAllRestaurantTables()
        {
            return [.._context.RestaurantTables];
        }

        //public RestaurantTable GetRestaurantTableById(int id)
        //{
        //    var result = _context.RestaurantTables.FirstOrDefault(a=> a.TableNumber == id);
        //    if (result != null)
        //    {
        //        return result;
        //    }
        //    else
        //    {
        //        throw new Exception($"RestaurantTable with id {id} not found.");
        //    }
        //}

        public RestaurantTable GetRestaurantTableById(int id)
        {
            var result = _context.RestaurantTables.Find(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception($"RestaurantTable with id {id} not found.");
            }
        }

        public void AddRestaurantTable(RestaurantTable restaurantTable)
        {
            _context.RestaurantTables.Add(restaurantTable);
            _context.SaveChanges();
        }

        public void UpdateRestaurantTable(RestaurantTable restaurantTable)
        {
            var existingTable = _context.RestaurantTables.Find(restaurantTable.TableId);
            if (existingTable != null)
            {
                existingTable.TableNumber = restaurantTable.TableNumber;
                existingTable.Capacity = restaurantTable.Capacity;
                existingTable.LocationZone = restaurantTable.LocationZone;
                existingTable.Status = restaurantTable.Status;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"RestaurantTable with id {restaurantTable.TableId} not found.");
            }
        }

        public void DeleteRestaurantTable(int id)
        {
            var result = _context.RestaurantTables.Find(id);
            if (result != null)
            {
                _context.RestaurantTables.Remove(result);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"RestaurantTable with id {id} not found.");
            }
        }

        public async Task<string> GetTableStatus(int tableId)
        {
             
            var hasActiveOrder = await _context.Orders
                .AnyAsync(o => o.TableId == tableId
                            && o.Status == "Active");

            if (hasActiveOrder)
                return "Occupied";

            // 2️⃣ Check if table is reserved today
            var hasReservation = await _context.ReservationTables
                .AnyAsync(r => r.TableId == tableId
                            && r.Status == "Confirmed"
                            && r.ReservedDate.Date == DateTime.Today);

            if (hasReservation)
                return "Reserved";

            // 3️⃣ Otherwise available
            return "Available";
        }

        public async Task<string> GetTableStatusByDate(int tableId, DateTime date)
        {
            // 1️⃣ Check if table has active order (real-time occupancy)
            var hasActiveOrder = await _context.Orders
                .AnyAsync(o => o.TableId == tableId
                            && o.Status == "Active");

            if (hasActiveOrder && date.Date == DateTime.Today)
                return "Occupied";

            // 2️⃣ Check reservation for selected date
            var hasReservation = await _context.ReservationTables
                .AnyAsync(r => r.TableId == tableId
                            && r.Status == "Confirmed"
                            && r.ReservedDate.Date == date.Date);

            if (hasReservation)
                return "Reserved";

            return "Available";
        }

        public async Task<Order?> GetActiveOrderByTableId(int tableId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Include(o => o.Payments)
                .Where(o => o.TableId == tableId && o.Status == "Active")
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<List<(MenuItem Item, int TotalQuantity)>> GetTopMostOrderedMenuItems(int count)
        {
            var result = await _context.OrderItems
                .GroupBy(oi => oi.MenuItemId)
                .Select(g => new
                {
                    MenuItemId = g.Key,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(count)
                .ToListAsync();

            var menuItemIds = result.Select(x => x.MenuItemId).ToList();

            var menuItems = await _context.MenuItems
                .Where(m => menuItemIds.Contains(m.Id))
                .ToListAsync();

            return result.Select(r =>
            (
                menuItems.First(m => m.Id == r.MenuItemId),
                r.TotalQuantity
            )).ToList();
        }

    }
}
