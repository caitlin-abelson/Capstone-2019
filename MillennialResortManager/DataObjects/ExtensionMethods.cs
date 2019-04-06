using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataObjects
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/06
        /// 
        /// Extension method used for deep
        /// copy of object by serialization
        /// (byte by byte)
        /// </summary>
        /// <typeparam name="T">Generic parameter</typeparam>
        /// <param name="a">object to clone</param>
        /// <returns>clone of object a</returns>
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/06
        /// 
        /// Extension method used to check if employee
        /// has roles or privileges to perform an operation
        /// </summary>
        /// <param name="employee">employee</param>
        /// <param name="errorStr">out param for error message</param>
        /// <param name="roles">list of allowed roles</param>
        /// <returns>return true of employee.roles is in roles and false if vice versa</returns>
        public static bool HasRoles(this Employee employee, out string errorStr, params string[] roles)
        {
            errorStr = "";

            // ... if 'employee' or 'employee role' is null, there is nothing to check, return true
            if (employee?.EmployeeRoles == null)
                return true;

            // ... check if employee rules is contained in role params
            if (roles.Any(role => employee.EmployeeRoles.SingleOrDefault(x => x.RoleID == role) != null))
                return true;

            // ... role was not found, return false
            var rolesStr = string.Join(separator: ", ", values: employee.EmployeeRoles.Select(x => x.RoleID));
            errorStr = $"You do not have system privilege to delete\nCurrent Roles: {(rolesStr.Length != 0 ? rolesStr : "you have no roles!")}";
            return false;
        }
    }
}