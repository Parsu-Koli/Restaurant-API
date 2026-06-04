using BLL.DTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class OrderServices(IOrderRepository repo, ICustomerRepository repose) : IOrderServices
    {
        private readonly IOrderRepository _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        private readonly ICustomerRepository _repose = repose ?? throw new ArgumentNullException(nameof(repose));

        public void AddOrder(CreateOrderDto dto)
        {
            var table = _repo.GetTableById(dto.TableId)
                        ?? throw new Exception($"Table with id {dto.TableId} not found.");

            if (table.Status == "Occupied")
                throw new Exception("Table is already occupied.");

            if (dto.OrderItems == null || !dto.OrderItems.Any())
                throw new Exception("Order must contain at least one item.");

            if (dto.CustomerDto == null)
                throw new Exception("Customer information is required.");

            // ✅ Create Customer (NOT SAVED YET)
            var customer = new Customer
            {
                FullName = dto.CustomerDto.FullName,
                Email = dto.CustomerDto.Email,
                PhoneNumber = dto.CustomerDto.PhoneNumber,
                Address = dto.CustomerDto.Address,
                CreatedAt = DateTime.UtcNow
            };

            decimal total = 0;
            var orderItems = new List<OrderItem>();

            foreach (var itemDto in dto.OrderItems)
            {
                var menuItem = _repo.GetMenuItemById(itemDto.MenuItemId)
                               ?? throw new Exception($"Menu item {itemDto.MenuItemId} not found.");

                total += menuItem.Price * itemDto.Quantity;

                orderItems.Add(new OrderItem
                {
                    MenuItemId = menuItem.Id,
                    Quantity = itemDto.Quantity,
                    Price = menuItem.Price
                });
            }

            // ✅ Create Order and attach Customer directly
            var order = new Order
            {
                Customer = customer, // 🔥 Navigation property instead of CustomerId
                TableId = dto.TableId,
                TotalPrice = total,
                Status = "Active",
                CreatedAt = DateTime.UtcNow,
                OrderItems = orderItems
            };

            table.Status = "Occupied";

            _repo.UpdateTable(table);

            // Only ONE SaveChanges inside AddOrder
            _repo.AddOrder(order);
        }

        public void DeleteOrder(int id)
        {
            _repo.DeleteOrder(id);
        }

        public List<OrderResponseDto> GetAllOrders()
        {
            var orders = _repo.GetAllOrders();

            return [.. orders.Select(o =>
            {
                var totalPaid = o.Payments?.Sum(p => p.Amount) ?? 0;
                var latestPayment = o.Payments?
                                     .OrderByDescending(p => p.PaidAt)
                                     .FirstOrDefault();

                return new OrderResponseDto
                {
                    Id = o.Id,
                    TotalPrice = o.TotalPrice,
                    Status = o.Status,
                    CreatedAt = o.CreatedAt,
                    CustomerName = o.Customer?.FullName,
                    TableNumber = o.RestaurantTable?.TableNumber ?? 0,

                    // ✅ Payment Logic
                    IsPaid = totalPaid >= o.TotalPrice,
                    PaidAt = latestPayment?.PaidAt,

                    Items = [.. o.OrderItems.Select(i => new OrderItemResponseDto
                    {
                        MenuItemName = i.MenuItem?.Name,
                        Quantity = i.Quantity,
                        Price = i.Price
                    })]
                };
            })];
        }

        public Order GetOrderById(int id)
        {
            return _repo.GetOrderById(id);
        }

        public void UpdateOrder(UpdateOrderDto dto)
        {
            var order = _repo.GetOrderById(dto.OrderId)
                        ?? throw new Exception($"Order with id {dto.OrderId} not found.");

            if (dto.OrderItems == null || !dto.OrderItems.Any())
                throw new Exception("Order must contain at least one item.");

            decimal total = 0;
            var updatedItems = new List<OrderItem>();

            foreach (var itemDto in dto.OrderItems)
            {
                var menuItem = _repo.GetMenuItemById(itemDto.MenuItemId)
                              ?? throw new Exception($"Menu item {itemDto.MenuItemId} not found.");

                total += menuItem.Price * itemDto.Quantity;

                updatedItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    MenuItemId = menuItem.Id,
                    Quantity = itemDto.Quantity,
                    Price = menuItem.Price
                });
            }

            // Update order fields
            order.TableId = dto.TableId;
            order.Status = dto.Status ?? order.Status;
            order.TotalPrice = total;

            // Replace order items
            order.OrderItems = updatedItems;

            _repo.UpdateOrder(order);
        }

        public string GetTableStatus(int tableId)
        {
            return _repo.GetTableStatus(tableId);
        }

        public OrderResponseDto? GetOrderByTableId(int tableId)
        {
            var order = _repo.GetOrderByTableId(tableId); // Fetch the Order entity from the repository.

            if (order == null)
                return null;

            // Map the Order entity to OrderResponseDto.
            return new OrderResponseDto
            {
                Id = order.Id,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                CustomerName = order.Customer?.FullName,
                customerId = order.CustomerId,
                TableNumber = order.RestaurantTable?.TableNumber ?? 0,
                Items = [.. order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    MenuItemName = oi.MenuItem?.Name, // Gets name from MenuItem table
                    Quantity = oi.Quantity,
                    Price = oi.Price
                })]
            };
        }
    }
}