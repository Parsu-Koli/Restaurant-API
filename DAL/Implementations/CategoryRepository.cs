using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
namespace DAL.Implementations
{
    public class CategoryRepository(AppDbContext context) : ICategoryRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public List<Category> GetAllCategories()
        {
            try
            {
                return [.._context.Categories];
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching categories.", ex);
            }
        }

        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.Find(id)??
                throw new KeyNotFoundException($"Category with id {id} not found.");
            return category;
        }

        public void AddCategory(Category category)
        {
            ArgumentNullException.ThrowIfNull(category);

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
             
            var existing = _context.Categories.Find(category.Id)
     ?? throw new KeyNotFoundException($"Category with id {category.Id} not found.");


            existing.Name = category.Name;
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id)??
              throw new KeyNotFoundException($"Category with id {id} not found.");

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
