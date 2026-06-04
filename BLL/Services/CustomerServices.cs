using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CustomerServices(ICustomerRepository repo) : ICustomerRepository
    {
        private readonly ICustomerRepository _repo = repo?? throw new ArgumentNullException(nameof(repo));

        public void AddCustomer(Customer customer)
        {
            _repo.AddCustomer(customer);
        }

        public void DeleteCustomer(int id)
        {
            _repo.DeleteCustomer(id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public Customer GetCustomerById(int id)
        {
            return _repo.GetCustomerById(id);
        }

        public void UpdateCustomer(Customer customer)
        {
            _repo.UpdateCustomer(customer);
        }
    }
}
