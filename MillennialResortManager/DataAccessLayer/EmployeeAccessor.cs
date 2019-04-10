using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class EmployeeAccessor : IEmployeeAccessor
    {
        public EmployeeAccessor()
        {

        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/27/19
        /// 
        /// The InsertEmployee method is for inserting a new employee into our records.
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        public void InsertEmployee(Employee newEmployee)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_employee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", newEmployee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", newEmployee.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", newEmployee.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", newEmployee.Email);
            cmd.Parameters.AddWithValue("@DepartmentID", newEmployee.DepartmentID);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/2/19
        /// 
        /// The DeactiveEmployee deactivates an employee
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeactiveEmployee(int employeeID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_deactivate_employee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }



        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/2/19
        /// 
        /// The DeleteEmployee deletes an inactive employee from the system.
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeleteEmployee(int employeeID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_employee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/13/19
        /// 
        /// Deletes an employee from the role table.
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeleteEmployeeRole(int employeeID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_employeeID_role";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// This method reads all of the employees that are in the database.
        /// </summary>
        /// <returns></returns>
        public List<Employee> SelectAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_all_employees";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = reader.GetInt32(0);
                        employee.FirstName = reader.GetString(1);
                        employee.LastName = reader.GetString(2);
                        employee.PhoneNumber = reader.GetString(3);
                        employee.Email = reader.GetString(4);
                        employee.DepartmentID = reader.GetString(5);
                        employee.Active = reader.GetBoolean(6);
                        employees.Add(employee);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return employees;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// This accessor retrieves an employee by their ID number
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Employee SelectEmployee(int employeeID)
        {
            Employee employee = new Employee();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_employee_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee.EmployeeID = reader.GetInt32(0);
                        employee.FirstName = reader.GetString(1);
                        employee.LastName = reader.GetString(2);
                        employee.PhoneNumber = reader.GetString(3);
                        employee.Email = reader.GetString(4);
                        employee.DepartmentID = reader.GetString(5);
                        employee.Active = reader.GetBoolean(6);
                        employee.EmployeeRoles = RetrieveEmployeeRoles(employee.EmployeeID);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return employee;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/2/19
        /// 
        /// The UpdateEmployee updates an employee's information
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <param name="oldEmployee"></param>
        public void UpdateEmployee(Employee newEmployee, Employee oldEmployee)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_employee_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", oldEmployee.EmployeeID);
            cmd.Parameters.AddWithValue("@FirstName", newEmployee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", newEmployee.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", newEmployee.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", newEmployee.Email);
            cmd.Parameters.AddWithValue("@DepartmentID", newEmployee.DepartmentID);
            cmd.Parameters.AddWithValue("@Active", newEmployee.Active);
            cmd.Parameters.AddWithValue("@OldFirstName", oldEmployee.FirstName);
            cmd.Parameters.AddWithValue("@OldLastName", oldEmployee.LastName);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldEmployee.PhoneNumber);
            cmd.Parameters.AddWithValue("@OldEmail", oldEmployee.Email);
            cmd.Parameters.AddWithValue("@OldDepartmentID", oldEmployee.DepartmentID);
            cmd.Parameters.AddWithValue("@OldActive", oldEmployee.Active);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/14/19
        /// 
        /// reads all of the active employees from the database
        /// </summary>
        /// <returns></returns>
        public List<Employee> SelectActiveEmployees()
        {
            List<Employee> employees = new List<Employee>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_employee_active";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = reader.GetInt32(0);
                        employee.FirstName = reader.GetString(1);
                        employee.LastName = reader.GetString(2);
                        employee.PhoneNumber = reader.GetString(3);
                        employee.Email = reader.GetString(4);
                        employee.DepartmentID = reader.GetString(5);
                        employee.Active = reader.GetBoolean(6);
                        employee.EmployeeRoles = RetrieveEmployeeRoles(employee.EmployeeID);
                        employees.Add(employee);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return employees;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/14/19
        /// 
        /// reads all of the inactive employees from the database
        /// </summary>
        /// <returns></returns>
        public List<Employee> SelectInactiveEmployees()
        {
            List<Employee> employees = new List<Employee>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_employee_inactive";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = reader.GetInt32(0);
                        employee.FirstName = reader.GetString(1);
                        employee.LastName = reader.GetString(2);
                        employee.PhoneNumber = reader.GetString(3);
                        employee.Email = reader.GetString(4);
                        employee.DepartmentID = reader.GetString(5);
                        employee.Active = reader.GetBoolean(6);
                        employee.EmployeeRoles = RetrieveEmployeeRoles(employee.EmployeeID);
                        employees.Add(employee);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return employees;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created Date: 3/7/19
        /// Returns an int containing the number of Employees who have a matching email and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int VerifyUsernameAndPassword(string userName, string password)
        {
            int result = 0;     // this will be the number of users found

            // we begin with a connection
            var conn = DBConnection.GetDbConnection();

            // next, we need command text
            string cmdText = @"sp_authenticate_user";

            // then we create a command object from command text and a connection
            var cmd = new SqlCommand(cmdText, conn);

            // now we need to set the command type
            cmd.CommandType = CommandType.StoredProcedure;      //CommandType defined in System.Data

            // next, set up the stored procedure's parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // pass values to the parameters
            cmd.Parameters["@Email"].Value = userName;
            cmd.Parameters["@PasswordHash"].Value = password;

            // now we need to use these connected types in an open connection
            // and this means unsafe code, so a try-catch
            try
            {
                // open the connections
                conn.Open();

                // execute the command
                result = (int)cmd.ExecuteScalar();   // need to cast in order to use it
            }
            catch (Exception up)
            {
                throw up;
            }
            return result;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created Date: 3/7/19
        /// Gets an Employee from our database by their email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Employee RetrieveEmployeeByEmail(string email)
        {
            Employee user = new Employee();

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_retrieve_employee_by_email";
            //string cmdText2 = @"sp_retrieve_employee_roles";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);
            //var cmd2 = new SqlCommand(cmdText2, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;
            //cmd2.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            //cmd2.Parameters.Add("@Email", SqlDbType.NVarChar, 250);


            // values
            cmd1.Parameters["@Email"].Value = email;
            //cmd2.Parameters["@Email"].Value = email;

            try
            {
                // open the connection
                conn.Open();
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();     // reads the first line
                    user.EmployeeID = reader1.GetInt32(0);
                    user.FirstName = reader1.GetString(1);
                    user.LastName = reader1.GetString(2);
                    user.Email = reader1.GetString(3);
                    user.PhoneNumber = reader1.GetString(4);
                    user.DepartmentID = reader1.GetString(5);
                    user.Active = reader1.GetBoolean(6);
                    user.EmployeeRoles = RetrieveEmployeeRoles(user.EmployeeID);
                }
                else
                {
                    // No roles found
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return user;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created Date: 3/11/2019
        /// Returns a list of Employee Roles 
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public List<Role> RetrieveEmployeeRoles(int EmployeeID)
        {
            List<Role> roles = new List<Role>();

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_retrieve_employee_roles_by_employeeid";
            //string cmdText2 = @"sp_retrieve_employee_roles";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);
            //var cmd2 = new SqlCommand(cmdText2, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.AddWithValue("@EmployeeID", EmployeeID);

            try
            {
                // open the connection
                conn.Open();
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Role r = new Role();
                        r.RoleID = reader.GetString(0);
                        r.Description = reader.GetString(1);
                        roles.Add(r);
                    }
                }
                else
                {

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return roles;
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/03/29
        ///
        /// connect to database to insert an employeeRole into the EmployeeRole crosstable.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="role"></param>
        public void InsertEmployeeRole(int employeeID, Role role)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_employee_role";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@RoleID", role.RoleID);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/03/29
        ///
        /// connect to database to delete an employeeRole into the EmployeeRole crosstable.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="role"></param>
        public void DeleteEmployeeRole(int employeeID, Role role)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_employee_role";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@RoleID", role.RoleID);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to retrieve all employeeinfo by employeeid
        /// </summary>
        public Employee RetrieveEmployeeInfo(int employeeID)
        {
            Employee employee = new Employee();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_employee_info_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee.EmployeeID = reader.GetInt32(0);
                        employee.FirstName = reader.GetString(1);
                        employee.LastName = reader.GetString(2);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return employee;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to retrieve all employeeinfo
        /// </summary>
        public List<Employee> RetrieveAllEmployeeInfo()
        {
            var employees = new List<Employee>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_employee_info";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var employee = new Employee();
                        employee.EmployeeID = reader.GetInt32(0);
                        employee.FirstName = reader.GetString(1);
                        employee.LastName = reader.GetString(2);
                        employees.Add(employee);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return employees;
        }
    }
}
