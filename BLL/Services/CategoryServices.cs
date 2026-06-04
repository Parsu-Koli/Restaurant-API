using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class CategoryServices(ICategoryRepository repo) : ICategoryRepository
    {
        private readonly ICategoryRepository _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public void AddCategory(Category category)
        {
            _repo.AddCategory(category);
        }

        public void DeleteCategory(int id)
        {
            _repo.DeleteCategory(id);
        }

        public List<Category> GetAllCategories()
        {
            return _repo.GetAllCategories();
        }

        public Category GetCategoryById(int id)
        {
            return _repo.GetCategoryById(id);
        }

        public void UpdateCategory(Category category)
        {
            _repo.UpdateCategory(category);
        }
    }
}
