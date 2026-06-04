using BLL.DTOs;
using BLL.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class MenuItemServices : IMenuItemService
    {
        private readonly IMenuItemRepository _repo;
        private readonly string _imageFolderPath;

        public MenuItemServices(IMenuItemRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));

            _imageFolderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/images/menu");

            if (!Directory.Exists(_imageFolderPath))
                Directory.CreateDirectory(_imageFolderPath);
        }

        public void AddMenuItem(CreateMenuItemDto dto)
        {
            string? imagePath = null;

            if (dto.Image != null)
            {
                var fileName = Guid.NewGuid() +
                               Path.GetExtension(dto.Image.FileName);

                var fullPath = Path.Combine(_imageFolderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                dto.Image.CopyTo(stream);

                imagePath = "/images/menu/" + fileName;
            }

            var menuItem = new MenuItem
            {
                Name = dto.Name,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                IsAvailable = dto.IsAvailable,
                ImageUrl = imagePath
            };

            _repo.AddMenuItem(menuItem);
        }

        public List<MenuItem> GetAllMenuItems()
            => _repo.GetAllMenuItems();

        public MenuItem GetMenuItemById(int id)
            => _repo.GetMenuItemById(id);

        public void UpdateMenuItem(int id, CreateMenuItemDto dto)
        {
            var existing = _repo.GetMenuItemById(id);

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.CategoryId = dto.CategoryId;
            existing.IsAvailable = dto.IsAvailable;

            if (dto.Image != null)
            {
                var fileName = Guid.NewGuid() +
                               Path.GetExtension(dto.Image.FileName);

                var fullPath = Path.Combine(_imageFolderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                dto.Image.CopyTo(stream);

                existing.ImageUrl = "/images/menu/" + fileName;
            }

            _repo.UpdateMenuItem(existing);
        }

        public void DeleteMenuItem(int id)
            => _repo.DeleteMenuItem(id);

        public List<MenuItem> GetMenuItemByCategory(int categoryId)
            => _repo.GetMenuItemByCategory(categoryId);

        public async Task<IEnumerable<MenuItem>> SearchMenuItemsAsync(string searchTerm)
        {
            return await _repo.SearchMenuItemsAsync(searchTerm);
        }
    }
}