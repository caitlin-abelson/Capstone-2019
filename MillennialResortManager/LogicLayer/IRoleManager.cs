using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="Eduardo Colon" created="2019/02/09">
	/// the interface IRoleManager
	/// </summary>
	public interface IRoleManager
    {

        List<Role> RetrieveAllRoles();
		/// <summary author="Eduardo Colon" created="2019/02/08">
		/// Sends a role to the the DAO to be added to the datastore.
		/// </summary>
		/// <updates>
		/// <update author="Austin Delaney" created="2019/04/27">
		/// Changed output from 'int' to 'bool'.
		/// </update>
		/// </updates>
		/// <returns>If the operation was successful.</returns>
		bool CreateRole(Role newRole);
        Role RetrieveRoleByRoleId(string roleID);
        void UpdateRole(Role oldRole, Role newRole);
        List<Role> RetrieveAllActiveRoles();
        bool DeleteRole(string roleId);
        List<Role> RetrieveAllInActiveRoles();
    }
}