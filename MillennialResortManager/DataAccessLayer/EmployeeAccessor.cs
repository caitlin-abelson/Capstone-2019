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
