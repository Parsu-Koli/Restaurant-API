using DAL.Models;
namespace DAL.Interfaces
{
    public interface IStockItemRepository
    {
        List<StockItem> GetAllStockItems();
        StockItem GetStockItemById(int id);
        void AddStockItem(StockItem stockItem);
        void UpdateStockItem(StockItem stockItem);
        void DeleteStockItem(int id);
    }
}
