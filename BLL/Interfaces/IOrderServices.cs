using BLL.DTOs;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IOrderServices
    {
        void AddOrder(CreateOrderDto dto);
        void DeleteOrder(int id);
        List<OrderResponseDto> GetAllOrders();
        Order GetOrderById(int id);
        void UpdateOrder(UpdateOrderDto dto);
        string GetTableStatus(int tableId);
        OrderResponseDto? GetOrderByTableId(int tableId);
    }
}