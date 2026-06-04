using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class StockItemRepository(AppDbContext context) : IStockItemRepository
    {
        private readonly AppDbContext _context = context?? throw new ArgumentNullException(nameof(context));

        public List<StockItem> GetAllStockItems()
        {
            return [.._context.StockItems];
        }

        public StockItem GetStockItemById(int id)
        {
            var result = _context.StockItems.Find(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception($"StockItem with id {id} not found.");
            }
        }

        public void AddStockItem(StockItem stockItem)
        {
            _context.StockItems.Add(stockItem);
            _context.SaveChanges();
        }

        public void UpdateStockItem(StockItem stockItem)
        {
            var existingStockItem = _context.StockItems.Find(stockItem.StockItemId);
            if (existingStockItem != null)
            {
                existingStockItem.Name = stockItem.Name;
                existingStockItem.Quantity = stockItem.Quantity;
                
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"StockItem with id {stockItem.StockItemId} not found.");
            }
        }

        public void DeleteStockItem(int id) {
            var result = _context.StockItems.Find(id);
            if (result != null)
            {
                _context.StockItems.Remove(result);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"StockItem with id {id} not found.");
            }
        }
    }
}
