using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{




    /// <summary>
    /// Eduardo Colon
    /// Created: 2019/02/09
    /// 
    /// RoleAccessorMock to be used for testing
    /// 
    /// the class RoleAccessorMock deriveds IRoleManager
    /// </summary>
    public class RoleAccessorMock : IRoleAccessor
    {
        private List<Role> _role;
        private List<string> _allRoles;

        public RoleAccessorMock()
        {
            _role = new List<Role>();
            _role.Add(new Role() { RoleID = "Checks in", Description = "Checks in" });
            _role.Add(new Role() { RoleID = "Checks out", Description = "Checks out" });
            _role.Add(new Role() { RoleID = "Procures", Description = "Procdures in" });
            _role.Add(new Role() { RoleID = "Admin", Description = "Admins roles " });

            _allRoles = new List<string>();
            foreach (var item in _role)
            {
                _allRoles.Add(item.RoleID);
            }
        }


        public void DeleteRole(string roleID)
        {
            try
            {
                RetrieveRoleByRoleId(roleID);
            }
            catch (Exception)
            {
                throw;
            }
            _role.Remove(_role.Find(b => b.RoleID == roleID));
        }

        public void DeleteEmployeeRole(string roleID)
        {
            try
            {
                RetrieveRoleByRoleId(roleID);
            }
            catch (Exception)
            {
                throw;
            }
            _role.Remove(_role.Find(b => b.RoleID == roleID));
        }

        public List<Role> RetrieveActiveRoles()
        {
            return _role;
        }

        public List<Role> RetrieveInactiveRoles()
        {
            return _role;
        }

        public int InsertRole(Role newRole)
        {
            _role.Add(newRole);

            return 1;
        }

        public void UpdateRole(Role oldRole, Role newRole)
        {
            foreach (var item in _role)
            {
                if (item.RoleID == oldRole.RoleID)
                {

                    item.Description = newRole.Description;


                }
            }
        }

        public void DeactivateRole(string roleID)
        {
            bool searchedRole = false;


            foreach (var item in _role)
            {
                if (item.RoleID == roleID)
                {



                    searchedRole = true;

                    break;
                }
            }
            if (!searchedRole)
            {
                throw new ApplicationException("Role was not found with this ID");
            }

        }

        public List<Role> RetrieveAllRoles()
        {
            return _role;
        }

        public Role RetrieveRoleByRoleId(string roleID)
        {
            Role role = new Role();
            role = _role.Find(b => b.RoleID == roleID);
            if (role == null)
            {
                throw new ArgumentException("RoleID doesn't match.");
            }

            return role;
        }
    }
}
