using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class UserServices(IUserRepository repo) : IUserRepository
    {
        private readonly IUserRepository _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public void AddUser(User user)
        {
            _repo.AddUser(user);
        }

        public void DeleteUser(int id)
        {
            _repo.DeleteUser(id);
        }

        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _repo.GetUserById(id);
        }

        public void UpdateUser(User user)
        {
            _repo.UpdateUser(user);
        }
    }
}
