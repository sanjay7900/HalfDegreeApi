using HalfDegreeApi.Data;
using HalfDegreeApi.Models;
using HalfDegreeApi.Services;
using Microsoft.EntityFrameworkCore;

namespace HalfDegreeApi.Repositories
{
    public class RolesData : IRole<Roles>
    {
        private readonly HalfDegreeApiDbContext _context;

        public RolesData(HalfDegreeApiDbContext halfDegreeApiDbContext)
        {
            _context = halfDegreeApiDbContext;
        }
        public bool AddRole(Roles role)
        {
            if (role == null)
            {
                _context.roles!.Add(role!);
                _context.SaveChanges();
            }
            return true;
        }

        public  Roles GetRole(int roleid)
        {
            return  _context.roles!.FirstOrDefault(r=>r.Id == roleid)!; 
        }

        public Roles GetRole(string rolename)
        {
            return _context.roles!.Where(r => r.Name == rolename).FirstOrDefault()!;
        }

        public bool IsAvailable(string rolename)
        {
            var role = GetRole(rolename);
            if(role == null)
            {
                return false;
            }
            return true;
        }

        public bool RemoveRole(int id)
        {
            var deleterole = GetRole(id);
            if (deleterole == null)
            {
                return false;
            }
            _context.roles!.Remove(deleterole);
            _context.SaveChanges();

            return true;
        }
        public bool RemoveRole(string rolename)
        {
            var deleterole=GetRole(rolename);
            if (deleterole == null)
            {
                return false;
            }
            _context.roles!.Remove(deleterole);
            _context.SaveChanges();
            return true;

        }
        public Roles UpdateRole(Roles role)
        {
            _context.roles!.Update(role);
            _context.SaveChanges();
            return role;
        }
        public List<Roles> GetRoles()
        {
            return _context.roles!.ToList();
        }
    }
}
