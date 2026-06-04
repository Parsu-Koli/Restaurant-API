using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
namespace DAL.Implementations
{
    public class SupplierRepository(AppDbContext context) : ISupplierRepository
    {
        private readonly AppDbContext _context = context?? throw new ArgumentNullException(nameof(context));

        public List<Supplier> GetAllSuppliers()
        {
            return [.._context.Suppliers];
        }

        public Supplier GetSupplierById(int id)
        {
            var result = _context.Suppliers.Find(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception($"Supplier with id {id} not found.");
            }
        }

        public void AddSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            _context.SaveChanges();
        }

        public void DeleteSupplier(int id)
        {
            var result = _context.Suppliers.Find(id);
            if (result != null)
            {
                _context.Suppliers.Remove(result);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Supplier with id {id} not found.");
            }
        }
    }
}
