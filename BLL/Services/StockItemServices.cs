using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StockItemServices(IStockItemRepository repo) : IStockItemRepository
    {
        private readonly IStockItemRepository _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public void AddStockItem(StockItem stockItem)
        {
            _repo.AddStockItem(stockItem);
        }

        public void DeleteStockItem(int id)
        {
            _repo.DeleteStockItem(id);
        }

        public List<StockItem> GetAllStockItems()
        {
            return _repo.GetAllStockItems();
        }

        public StockItem GetStockItemById(int id)
        {
            return _repo.GetStockItemById(id);
        }

        public void UpdateStockItem(StockItem stockItem)
        {
            _repo.UpdateStockItem(stockItem);
        }
    }
}
