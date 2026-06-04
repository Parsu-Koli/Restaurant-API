using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class RoleServices(IRoleRepository repo) : IRoleRepository
    {
        private readonly IRoleRepository _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        public void AddRole(Role role)
        {
            _repo.AddRole(role);
        }
        public void DeleteRole(int id)
        {
            _repo.DeleteRole(id);
        }
        public List<Role> GetAllRoles()
        {
            return _repo.GetAllRoles();
        }
        public Role GetRoleById(int id)
        {
            return _repo.GetRoleById(id);
        }
        public void UpdateRole(Role role)
        {
            _repo.UpdateRole(role);
        }
    }
}
