using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Austin Delaney" created="2019/04/27">
	/// Mock/fake implementation of the <see cref="IRoleManager"/> interface for testing the presentation layer.
	/// </summary>
	public class FakeRoleManager : IRoleManager
	{
		private Dictionary<Role, bool> _rolesWithActiveVal = new Dictionary<Role, bool>();

		public FakeRoleManager()
		{
			_rolesWithActiveVal.Add(new Role { RoleID = "Employee", Description = "Just a guy" }, false);
			_rolesWithActiveVal.Add(new Role { RoleID = "Shift Supervisor", Description = "Supervises shifts? What do you think" }, true);
			_rolesWithActiveVal.Add(new Role { RoleID = "Customer Service", Description = "Helps customers with issues" }, true);
			_rolesWithActiveVal.Add(new Role { RoleID = "Local superhero", Description = "Just a hero for fun" }, false);
			_rolesWithActiveVal.Add(new Role { RoleID = "Manager", Description = "The big honcho" }, true);
		}

		/// <summary author="Austin Delaney" created="2019/04/27">
		/// Validates role, throwing proper exceptions, then returns true.
		/// </summary>
		/// <param name="newRole">Role to 'add'.</param>
		/// <returns>Always true.</returns>
		public bool CreateRole(Role newRole)
		{
			if (null == newRole)
			{ throw new ArgumentNullException("Role to be created cannot be null"); }
			if (!Validation.IsValidRoleID(newRole.RoleID) || !Validation.IsValidDescription(newRole.Description))
			{ throw new ArgumentException("The data for this role is invalid"); }

			return true;
		}

		/// <summary author="Austin Delaney" created="2019/04/27">
		/// Validates roleID and returns true.
		/// </summary>
		/// <param name="roleId"></param>
		/// <returns></returns>
		public bool DeleteRole(string roleId)
		{
			if (null == roleId)
			{ throw new ArgumentNullException("Role to be created cannot be null"); }
			if (!Validation.IsValidRoleID(roleId))
			{ throw new ArgumentException("The data for this role is invalid"); }

			return true;
		}

		/// <summary author="Austin Delaney" created="2019/04/27">
		/// Returns a bunch of made up roles.
		/// </summary>
		/// <returns>Some made up roles.</returns>
		public List<Role> RetrieveAllActiveRoles()
		{
			return _rolesWithActiveVal.Where(kp => kp.Value).Select(rwv => rwv.Key).ToList();
		}

		/// <summary author="Austin Delaney" created="2019/04/27">
		/// Returns a bunch of made up roles.
		/// </summary>
		/// <returns>Some made up roles.</returns>
		public List<Role> RetrieveAllInActiveRoles()
		{
			return _rolesWithActiveVal.Where(kp => !kp.Value).Select(rwv => rwv.Key).ToList();
		}

		/// <summary author="Austin Delaney" created="2019/04/27">
		/// Returns a bunch of made up roles.
		/// </summary>
		/// <returns>Some made up roles.</returns>
		public List<Role> RetrieveAllRoles()
		{
			return _rolesWithActiveVal.Select(rwv => rwv.Key).ToList();
		}

		/// <summary author="Austin Delaney" created="2019/04/27">
		/// Validates roleID, makes up a description, and returns a formed Role object.
		/// </summary>
		/// <param name="roleID">ID to use for the role being built.</param>
		/// <returns>Valid role object.</returns>
		public Role RetrieveRoleByRoleId(string roleID)
		{
			if (null == roleID)
			{ throw new ArgumentNullException("Role to be created cannot be null"); }
			if (!Validation.IsValidRoleID(roleID))
			{ throw new ArgumentException("The data for this role is invalid"); }

			return new Role { RoleID = roleID, Description = "Sample description" };
		}

		/// <summary author="Austin Delaney" created="2019/04/27">
		/// Does literally nothing except validate.
		/// </summary>
		/// <param name="oldRole">Irrelevant.</param>
		/// <param name="newRole">Irrelevant.</param>
		public void UpdateRole(Role oldRole, Role newRole)
		{
			if (null == oldRole)
			{ throw new ArgumentNullException("Role to be created cannot be null"); }
			if (null == newRole)
			{ throw new ArgumentNullException("Role to be created cannot be null"); }
			if (!Validation.IsValidRoleID(oldRole.RoleID) || !Validation.IsValidDescription(oldRole.Description))
			{ throw new ArgumentException("The data for this role is invalid"); }
			if (!Validation.IsValidRoleID(newRole.RoleID) || !Validation.IsValidDescription(newRole.Description))
			{ throw new ArgumentException("The data for this role is invalid"); }
		}
	}
}