/// <summary>
/// Austin Berquam
/// Created: 2019/02/23
/// 
/// This is a mock Data Accessor which implements IroleAccessor.  This is for testing purposes only.
/// </summary>
/// 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class MockEmpRolesAccessor : IEmpRolesAccessor
    {
        private List<EmpRoles> role;

        /// <summary>
        /// Author: Austin Berquam
        /// Created: 2019/02/23
        /// This constructor that sets up dummy data
        /// </summary>
        public MockEmpRolesAccessor()
        {
            role = new List<EmpRoles>
            {
                new EmpRoles {RoleID = "role1", Description = "role is a role"},
                new EmpRoles {RoleID = "role2", Description = "role is a role"},
                new EmpRoles {RoleID = "role3", Description = "role is a role"},
                new EmpRoles {RoleID = "role4", Description = "role is a role"}
            };
        }

        public int InsertEmpRole(EmpRoles empRoles)
        {
            int listLength = role.Count;
            role.Add(empRoles);
            if (listLength == role.Count - 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public List<EmpRoles> SelectEmpRoles(string status)
        {
            return role;
        }

        public List<string> SelectAllRoleID()
        {
            throw new NotImplementedException();
        }

        public int DeleteEmpRole(string roleID)
        {
            int rowsDeleted = 0;
            foreach (var type in role)
            {
                if (type.RoleID == roleID)
                {
                    int listLength = role.Count;
                    role.Remove(type);
                    if (listLength == role.Count - 1)
                    {
                        rowsDeleted = 1;
                    }
                }
            }

            return rowsDeleted;
        }
    }
}