/// <summary>
/// Wes Richardson
/// Created: 2019/03/07
/// 
/// Passing data related to Appointments to the Appointment Manager
/// </summary>
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class AppointmentAccessor : IAppointmentAccessor
    {
        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// Inserts a new appointment into the database
        /// </summary>
        public int InsertAppointment(Appointment appointment)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_insert_appointment";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentTypeID", appointment.AppointmentType);
            cmd.Parameters.AddWithValue("@GuestID", appointment.GuestID);
            cmd.Parameters.AddWithValue("@StartDate", appointment.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", appointment.EndDate);
            cmd.Parameters.AddWithValue("@Description", appointment.Description);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// Retrieves an Appointment based on given ID
        /// </summary>
        /// <param name="id">An Appointment ID</param>
        /// <returns>An Appointment based on ID</returns>
        public Appointment SelectAppointmentByID(int id)
        {
            Appointment appointment = null;

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_select_appointment_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    appointment = new Appointment()
                    {
                        AppointmentID = id,
                        AppointmentType = reader.GetString(0),
                        GuestID = reader.GetInt32(1),
                        StartDate = reader.GetDateTime(2),
                        EndDate = reader.GetDateTime(3),
                        Description = reader.GetString(4)
                    };
                }
                else
                {
                    throw new ApplicationException("Appointment Data not found.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }

            return appointment;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/28
        /// 
        /// Selects Appointments based on a guest ID
        /// </summary>
        /// <param name="guestID"></param>
        /// <returns>A list of appointments</returns>
        public List<Appointment> SelectAppointmentByGuestID(int guestID)
        {
            List<Appointment> appointments = null;

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_select_appointment_by_guest_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    appointments = new List<Appointment>();
                    while (reader.Read())
                    {
                        Appointment app = new Appointment()
                        {
                            AppointmentID = reader.GetInt32(0),
                            AppointmentType = reader.GetString(1),
                            GuestID = guestID,
                            StartDate = reader.GetDateTime(2),
                            EndDate = reader.GetDateTime(3),
                            Description = reader.GetString(4)
                        };
                        appointments.Add(app);
                    }
                }
                else
                {
                    throw new ApplicationException("No Appointments Found for Guest");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }

            return appointments;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// Sends data to the database to update an appointment
        /// </summary>
        public int UpdateAppointment(Appointment appointment)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_update_appointment";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentID", appointment.AppointmentID);
            cmd.Parameters.AddWithValue("@AppointmentTypeID", appointment.AppointmentType);
            cmd.Parameters.AddWithValue("@GuestID", appointment.GuestID);
            cmd.Parameters.AddWithValue("@StartDate", appointment.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", appointment.EndDate);
            cmd.Parameters.AddWithValue("@Description", appointment.Description);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// Gets a list of appointment types and their descriptions
        /// </summary>
        public List<AppointmentType> SelectAppointmentTypes()
        {
            List<AppointmentType> appointmentTypes = null;

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_select_appointment_types";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    appointmentTypes = new List<AppointmentType>();
                    while (reader.Read())
                    {
                        var apt = new AppointmentType()
                        {
                            AppointmentTypeID = reader.GetString(0),
                            Description = reader.GetString(1)
                        };
                        appointmentTypes.Add(apt);
                    }
                }
                else
                {
                    throw new ApplicationException("Appointment Type data not found");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }

            return appointmentTypes;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// Retrieves a list of guestsID, 
        /// </summary>
        public List<AppointmentGuestViewModel> SelectGuestList()
        {
            List<AppointmentGuestViewModel> appointmentGuestViewModelList = null;

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_select_appointment_guest_view_list";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    appointmentGuestViewModelList = new List<AppointmentGuestViewModel>();
                    while (reader.Read())
                    {
                        var agvm = new AppointmentGuestViewModel()
                        {
                            GuestID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3)
                        };
                        appointmentGuestViewModelList.Add(agvm);
                    }
                }
                else
                {
                    throw new ApplicationException("Guest List data not found");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }

            return appointmentGuestViewModelList;
        }

        public int DeleteAppointmentByID(int ID)
        {
            int rows = 0;
            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_delete_appointment_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppointmentID", ID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
    }
}
