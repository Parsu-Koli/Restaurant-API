using DAL.Data;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Implementations
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context?? throw new ArgumentNullException(nameof(context));

        public List<User> GetAllUsers()
        {
            return [.._context.Users];
        }

        public User GetUserById(int id)
        {
            var result = _context.Users.Find(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception($"User with id {id} not found.");
            }
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var result = _context.Users.Find(id);
            if (result != null)
            {
                _context.Users.Remove(result);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"User with id {id} not found.");
            }
        }
    }
}
