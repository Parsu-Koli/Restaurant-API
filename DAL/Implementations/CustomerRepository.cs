using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
namespace DAL.Implementations
{
    public class CustomerRepository(AppDbContext context) : ICustomerRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public List<Customer> GetAllCustomers()
        {
            return [.. _context.Customers];
        }

        public Customer GetCustomerById(int id)
        {
            var result = _context.Customers.Find(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception($"Customer with id {id} not found.");
            }
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var result = _context.Customers.Find(id);
            if (result != null)
            {
                _context.Customers.Remove(result);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Customer with id {id} not found.");
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = _context.Customers.Find(customer.CustomerId);
            if (existingCustomer != null)
            {
                existingCustomer.FullName = customer.FullName;
                existingCustomer.Email = customer.Email;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.Address = customer.Address;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Customer with id {customer.CustomerId} not found.");
            }
        }
    }
}
