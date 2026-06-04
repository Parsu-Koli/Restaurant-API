using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class SupplierServices(ISupplierRepository repo) : ISupplierRepository
    {
        private readonly ISupplierRepository _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public void AddSupplier(Supplier supplier)
        {
            _repo.AddSupplier(supplier);
        }

        public void DeleteSupplier(int id)
        {
            _repo.DeleteSupplier(id);
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _repo.GetAllSuppliers();
        }

        public Supplier GetSupplierById(int id)
        {
            return _repo.GetSupplierById(id);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _repo.UpdateSupplier(supplier);
        }
    }
}
