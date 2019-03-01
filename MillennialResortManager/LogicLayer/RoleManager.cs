using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{

    /// <summary>
    /// Eduardo Colon
    /// Created: 2019/02/09
    /// 
    /// Concrete class for IRoleManager.
    /// </summary>
    public class RoleManager : IRoleManager
    {
        private IRoleAccessor _roleAccessor;


        public RoleManager()
        {
            _roleAccessor = new RoleAccessor();
        }


        public RoleManager(RoleAccessorMock roleAccessorMock)
        {
            _roleAccessor = roleAccessorMock;
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/30
        /// 
        /// method to get a list of users
        /// </summary>
        public List<Role> RetrieveAllRoles()
        {
            List<Role> roles;
            try
            {
                roles = _roleAccessor.RetrieveAllRoles();
            }
            catch (Exception)
            {
                throw;
            }

            return roles;
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/08
        /// 
        /// method to create role
        /// </summary>

        public int CreateRole(Role newRole)
        {
            int role = 0;

            try
            {
                if (!isValid(newRole))
                {
                    throw new ArgumentException("The data for this role is invalid");
                }

                role = _roleAccessor.InsertRole(newRole);
            }
            catch
            {
                throw;
            }

            return role;
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/24
        /// 
        /// method to retrieve role by roleid
        /// </summary>


        public Role RetrieveRoleByRoleId(string roleID)
        {
            Role role;
            try
            {
                role = _roleAccessor.RetrieveRoleByRoleId(roleID);
            }
            catch (Exception)
            {
                throw;
            }
            return role;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/24
        /// 
        /// method to update role 
        /// </summary>


        public void UpdateRole(Role oldRole, Role newRole)
        {
            try
            {
                if (!validateDescription(newRole.Description))
                {
                    throw new ArgumentException("The description for this role is invalid");

                }
                _roleAccessor.UpdateRole(oldRole, newRole);
            }
            catch (Exception)
            {

                throw;
            }
        }





        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/24
        /// 
        /// method to delete a role when it is not active; otherwise just deactivate it
        /// </summary>


        /*
    public void DeleteRole(string roleID, bool isActive)
    {
        // check to see if the role is active.
        if (isActive == true)
        {
            try
            {

                    _roleAccessor.DeactivateRole(roleID);


            }
            catch (Exception)
            {
                throw;
            }
        }
        // If the status of the role is inactive, this will be deleted,.
        else
        {
            try
            {
                _roleAccessor.DeleteEmployeeRole(roleID);
                _roleAccessor.DeleteRole(roleID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    */
        public bool DeleteRole(string roleId)
        {

            // you can check for other things like length

            bool result = false;

            try
            {
                _roleAccessor.DeleteEmployeeRole(roleId);
                _roleAccessor.DeleteRole(roleId);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/24
        /// 
        /// Retrieves  all  active roles
        /// </summary>

        public List<Role> RetrieveAllActiveRoles()
        {
            List<Role> roles = new List<Role>();
            try
            {
                roles = _roleAccessor.RetrieveActiveRoles();
            }
            catch (Exception)
            {
                throw;
            }
            return roles;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/24
        /// 
        /// Retrieves  all  inactive roles
        /// </summary>
        public List<Role> RetrieveAllInActiveRoles()
        {
            List<Role> roles = new List<Role>();
            try
            {
                roles = _roleAccessor.RetrieveInactiveRoles();
            }
            catch (Exception)
            {
                throw;
            }
            return roles;
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/08
        /// 
        /// method to validate roleID
        /// </summary>
        public bool validateRoleID(string roleID)
        {
            if (roleID.Length < 1 || roleID.Length > 50 || RetrieveAllRoles().Any(r => r.RoleID == roleID))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/08
        /// 
        /// method to validate description
        /// </summary>
        public bool validateDescription(string description)
        {
            if (description.Length < 1 || description.Length > 1000)
            {
                return false;

            }
            return true;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/08
        /// 
        /// method to check validation for roleid and  description
        /// </summary>
        public bool isValid(Role role)
        {
            if (validateRoleID(role.RoleID) && validateDescription(role.Description))
            {
                return true;
            }
            return false;
        }


    }
}
