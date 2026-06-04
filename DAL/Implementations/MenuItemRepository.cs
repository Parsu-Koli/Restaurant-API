using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class MenuItemRepository(AppDbContext context) : IMenuItemRepository
    {
        private readonly AppDbContext _context = context;

        public void AddMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();
        }

        public void DeleteMenuItem(int id)
        {
            var menuItem = _context.MenuItems.Find(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                _context.SaveChanges();
            }
            else
            {
               throw new Exception($"MenuItem with id {id} not found.");
            }
        }

        public List<MenuItem> GetAllMenuItems()
        {
            return [.._context.MenuItems];
        }

        public MenuItem GetMenuItemById(int id)
        {
            var result = _context.MenuItems.Find(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception($"MenuItem with id {id} not found.");
            }
        }

        public void UpdateMenuItem(MenuItem menuItem)
        {
            var existingMenuItem = _context.MenuItems.Find(menuItem.Id);
            if (existingMenuItem != null)
            {
                existingMenuItem.Name = menuItem.Name;
                existingMenuItem.Description = menuItem.Description;
                existingMenuItem.Price = menuItem.Price;
                existingMenuItem.CategoryId = menuItem.CategoryId;
                existingMenuItem.ImageUrl = menuItem.ImageUrl;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"MenuItem with id {menuItem.Id} not found.");
            }
        }

        public List<MenuItem> GetMenuItemByCategory(int id)
        {
            var result = _context.MenuItems.Where(e => e.CategoryId == id).ToList();
            return result;
        }

        public async Task<IEnumerable<MenuItem>> SearchMenuItemsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.MenuItems
                    .Where(x => x.IsAvailable)
                    .ToListAsync();
            }

            return await _context.MenuItems
                .Where(x => x.IsAvailable &&
                            (x.Name!.Contains(searchTerm) ||
                             x.Description!.Contains(searchTerm)))
                .ToListAsync();
        }
    }
}
