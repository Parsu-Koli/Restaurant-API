using BLL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMenuItemService
    {
        void AddMenuItem(CreateMenuItemDto dto);
        List<MenuItem> GetAllMenuItems();
        MenuItem GetMenuItemById(int id);
        void UpdateMenuItem(int id, CreateMenuItemDto dto);
        void DeleteMenuItem(int id);
        List<MenuItem> GetMenuItemByCategory(int categoryId);
        Task<IEnumerable<MenuItem>> SearchMenuItemsAsync(string searchTerm);
    }
}
