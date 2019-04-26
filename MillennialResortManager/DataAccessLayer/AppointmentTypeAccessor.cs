using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataObjects;

namespace DataAccessLayer
{
    public class AppointmentTypeAccessor : IAppointmentTypeAccessor
    {

        /// <summary>
        ///  @Author Craig Barkley
        ///  @Created 2/5/2019
        ///  
        /// Class for the stored procedure data forAppointment Types
        /// </summary>
        /// <param name="newAppointmentType"></param>
        /// <returns></returns>

        public int CreateAppointmentType(AppointmentType newAppointmentType)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_insert_appointment_type", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Parameters for new Event Request          
            cmd.Parameters.AddWithValue("@AppointmentTypeID", newAppointmentType.AppointmentTypeID);
            cmd.Parameters.AddWithValue("@Description", newAppointmentType.Description);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }


            return rows;
        }

        /// <summary>
        /// Method that Selects All Appointment Types by id.
        /// </summary>
        /// <param name="">The ID of the Appointment Type are retrieved.</param>
        /// <returns> appointmentTypes </returns>

        public List<string> SelectAllAppointmentTypeID()
        {
            var appoinmentTypes = new List<string>();

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_appointment_types", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        appoinmentTypes.Add(read.GetString(0));
                    }
                }
                read.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return appoinmentTypes;
        }

        /// <summary>
        /// Method that retrieves the Appointment types table and stores it as a list. Retrieves by Status.
        /// </summary>
        /// <returns>List of Appointment Types </returns>	

        public List<AppointmentType> RetrieveAllAppointmentTypes(string status)
        {
            List<AppointmentType> appointmentTypes = new List<AppointmentType>();

            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand("sp_retrieve_appointment_types", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        appointmentTypes.Add(new AppointmentType()
                        {
                            AppointmentTypeID = read.GetString(0),
                            Description = read.GetString(1)
                        });

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
            return appointmentTypes;
        }

        /// <summary>
        /// Method that deletes a Appointment Type and removes it from the table. 
        /// </summary>
        /// <param name="appointmentTypeID">The ID of the Employee Roles being deleted</param>
        /// <returns> Row Count </returns>

        public int DeleteAppointmentType(string appointmentTypeID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_delete_appointment_type_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentTypeID", appointmentTypeID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public AppointmentType RetrievAppointmentTypeById(string id)
        {
            throw new NotImplementedException();
        }
    }
}

