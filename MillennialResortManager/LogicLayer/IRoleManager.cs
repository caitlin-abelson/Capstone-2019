using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Eduardo Colon
    /// Created: 2019/02/09
    /// 
    /// the interface IRoleManager
    /// </summary>
    public interface IRoleManager
    {

        List<Role> RetrieveAllRoles();

        
        int CreateRole(Role newRole);
     
        Role RetrieveRoleByRoleId(string roleID);
        void UpdateRole(Role oldRole, Role newRole);
        void DeleteRole(string roleID, bool isActive);
        List<Role> RetrieveAllActiveRoles();
        List<Role> RetrieveAllInActiveRoles();



    }
}
