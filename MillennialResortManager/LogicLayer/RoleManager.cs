using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Eduardo Colon" created="2019/02/09">
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

		/// <summary author="Eduardo Colon" created="2019/01/30">
		/// method to get a list of users
		/// </summary>
		public List<Role> RetrieveAllRoles()
		{
			List<Role> roles;
			try
			{
				roles = _roleAccessor.RetrieveAllRoles();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return roles;
		}


		/// <summary author="Eduardo Colon" created="2019/02/08">
		/// Sends a role to the the DAO to be added to the datastore.
		/// </summary>
		/// <updates>
		/// <update author="Austin Delaney" created="2019/04/27">
		/// Changed output from 'int' to 'bool', added exception logging, moved argument validation, added arguement
		/// null check.
		/// </update>
		/// </updates>
		/// <returns>The success of the operation, based on if the DAO signals 1 role was added to the data store.</returns>
		public bool CreateRole(Role newRole)
		{
			if (null == newRole)
			{ throw new ArgumentNullException("Role to be created cannot be null"); }
			if (!isValid(newRole))
			{ throw new ArgumentException("The data for this role is invalid"); }

			int result = -1;

			try
			{
				result = _roleAccessor.InsertRole(newRole);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return (1 == result);
		}

		/// <summary author="Eduardo Colon" created="2019/02/24">
		/// method to retrieve role by roleid
		/// </summary>
		public Role RetrieveRoleByRoleId(string roleID)
		{
			Role role;
			try
			{
				role = _roleAccessor.RetrieveRoleByRoleId(roleID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return role;
		}

		/// <summary author="Eduardo Colon" created="2019/02/24">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		public bool DeleteRole(string roleId)
		{
			bool result = false;

			try
			{
				_roleAccessor.DeleteEmployeeRole(roleId);
				_roleAccessor.DeleteRole(roleId);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Eduardo Colon" created="2019/02/24">
		/// Retrieves  all  active roles
		/// </summary>
		public List<Role> RetrieveAllActiveRoles()
		{
			List<Role> roles = new List<Role>();
			try
			{
				roles = _roleAccessor.RetrieveActiveRoles();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return roles;
		}

		/// <summary author="Eduardo Colon" created="2019/02/24">
		/// Retrieves  all  inactive roles
		/// </summary>
		public List<Role> RetrieveAllInActiveRoles()
		{
			List<Role> roles = new List<Role>();
			try
			{
				roles = _roleAccessor.RetrieveInactiveRoles();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return roles;
		}

		/// <summary author="Eduardo Colon" created="2019/02/08">
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

		/// <summary author="Eduardo Colon" created="2019/02/08">
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

		/// <summary author="Eduardo Colon" created="2019/02/08">
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