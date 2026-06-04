using DAL.Models;

namespace DAL.Interfaces
{
    public interface IMenuItemRepository
    {
        List<MenuItem> GetAllMenuItems();
        MenuItem GetMenuItemById(int id);
        void AddMenuItem(MenuItem menuItem);
        void UpdateMenuItem(MenuItem menuItem);
        void DeleteMenuItem(int id);

        List<MenuItem> GetMenuItemByCategory(int id);
        Task<IEnumerable<MenuItem>> SearchMenuItemsAsync(string searchTerm);
    }
}
