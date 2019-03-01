
/// Austin Berquam
/// Created: 2019/02/12
/// 
/// Interface that implements Create and Delete functions for EmpRoles Types
/// accessor classes.
/// </summary>
using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IEmpRolesManager
    {
        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Creates a new EmpRoles type
        /// </summary>
        bool CreateRole(EmpRoles newRole);
        List<EmpRoles> RetrieveAllRoles(string status);

        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Deletes a new EmpRoles type
        /// </summary>
        bool DeleteRole(string role);
        List<string> RetrieveAllRoles();
    }
}