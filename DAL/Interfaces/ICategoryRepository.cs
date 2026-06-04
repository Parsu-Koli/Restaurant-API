using DAL.Models;

namespace DAL.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();

        Category GetCategoryById(int id);

        void AddCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(int id);
    }
}
