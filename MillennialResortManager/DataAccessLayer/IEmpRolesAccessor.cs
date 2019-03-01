
/// Austin Berquam
/// Created: 2019/02/12
/// 
/// Interface that implements Create and Delete functions for EmpRoles Types
/// accessor classes.
/// </summary>
using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IEmpRolesAccessor
    {
        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Creates a new EmpRoles type
        /// </summary>
        int InsertEmpRole(EmpRoles empRoles);
        List<EmpRoles> SelectEmpRoles(string status);

        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Deletes a new EmpRoles type
        /// </summary>
        List<string> SelectAllRoleID();
        int DeleteEmpRole(string roleID);
    }
}