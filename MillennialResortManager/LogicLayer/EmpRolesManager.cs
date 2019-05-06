using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="Austin Berquam" created="2019/01/26">
	/// EmpRolesAccessor class is used to access the Roles table
	/// and the stored procedures as well
	/// </summary>
	public class EmpRolesManager : IEmpRolesManager
	{
		IEmpRolesAccessor empRolesAccessor;
		public EmpRolesManager()
		{
			empRolesAccessor = new EmpRolesAccessor();
		}
		public EmpRolesManager(MockEmpRolesAccessor mock)
		{
			empRolesAccessor = new MockEmpRolesAccessor();
		}

		/// <summary author="Austin Berquam" created="2019/01/26">
		/// Method that collects the EmpRoles from the accessor
		/// </summary>
		/// <returns> List of EmpRoles </returns>
		public List<EmpRoles> RetrieveAllRoles(string status)
		{
			List<EmpRoles> roles = null;

			if (status != "")
			{
				try
				{
					roles = empRolesAccessor.SelectEmpRoles(status);
				}
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}

			return roles;
		}

		/// <summary author="Austin Berquam" created="2019/01/26">
		/// Method that sends the created role to the accessor
		/// </summary>
		/// <param name="newRole">Object holding the new role to add to the table</param>
		/// <returns> Row Count </returns>
		public bool CreateRole(EmpRoles newRole)
		{
			ValidationExtensionMethods.ValidateID(newRole.RoleID);
			ValidationExtensionMethods.ValidateDescription(newRole.Description);
			bool result = false;

			try
			{
				result = (1 == empRolesAccessor.InsertEmpRole(newRole));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Austin Berquam" created="2019/01/26">
		/// Method that retrieves all role types and stores it in a list
		/// </summary>
		/// <returns> RoleTypes in a List returns>
		public List<string> RetrieveAllRoles()
		{
			List<string> roles = null;

			try
			{
				roles = empRolesAccessor.SelectAllRoleID();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return roles;
		}

		/// <summary author="Austin Berquam" created="2019/01/26">
		/// Method that deletes an Employee and removes it in the table
		/// </summary>
		/// <param name="role">Employee Role ID to delete</param>
		/// <returns> bool on if the role was deleted </returns>
		public bool DeleteRole(string role)
		{
			bool result = false;

			try
			{
				result = (1 == empRolesAccessor.DeleteEmpRole(role));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}
	}
}
