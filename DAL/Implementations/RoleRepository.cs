using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace DAL.Implementations
{
    public class RoleRepository(AppDbContext context) : IRoleRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public List<Role> GetAllRoles()
        {
            return [.. _context.Roles.Include(a=> a.Users)];
        }

        public Role GetRoleById(int id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id) ?? throw new KeyNotFoundException($"Role with id {id} not found.");
        }

        public void AddRole(Role role)
        {
            ArgumentNullException.ThrowIfNull(role);
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void UpdateRole(Role role)
        {
            var existing = _context.Roles.Find(role.Id) ?? throw new KeyNotFoundException($"Role with id {role.Id} not found.");
            existing.RoleName = role.RoleName;
            _context.SaveChanges();
        }

        public void DeleteRole(int id)
        {
            var role = _context.Roles.Find(id) ?? throw new KeyNotFoundException($"Role with id {id} not found.");
            _context.Roles.Remove(role);
            _context.SaveChanges();
        }
    }
}
