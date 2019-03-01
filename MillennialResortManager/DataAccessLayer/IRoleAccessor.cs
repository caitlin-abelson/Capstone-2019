using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Eduardo Colon
    /// Created: 2019/02/09
    /// 
    /// the interface IRoleAccessor
    /// </summary>
    public interface IRoleAccessor
    {
        List<Role> RetrieveAllRoles();


        int InsertRole(Role newRole);

        Role RetrieveRoleByRoleId(string roleID);
        void UpdateRole(Role oldRole, Role newRole);
        void DeactivateRole(string roleID);
        void DeleteRole(string roleID);
        void DeleteEmployeeRole(string roleID);
        List<Role> RetrieveActiveRoles();
        List<Role> RetrieveInactiveRoles();

    }
}
