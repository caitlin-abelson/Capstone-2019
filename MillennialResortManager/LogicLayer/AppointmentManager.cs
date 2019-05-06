
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="" created="">
	/// Wes Richardson
	/// Created: 2019/03/07
	/// 
	/// Checks data before sending it to data access
	/// </summary>
	public class AppointmentManager : IAppointmentManager
    {
        private IAppointmentAccessor _appointmentAccessor;
        private bool appointmentValid;

		/// <summary author="" created="">
		/// Wes Richardson
		/// Created: 2019/03/07
		/// 
		/// Default Constructor for getting the AppointmentAccessor that retrieves data from database
		/// </summary>
		public AppointmentManager()
        {
            _appointmentAccessor = new AppointmentAccessor();
            appointmentValid = false;
        }

		/// <summary author="Wes Richardson" created="2019/03/07">
		/// Construtor for unit tests
		/// </summary>
		public AppointmentManager(IAppointmentAccessor appAccr)
        {
            _appointmentAccessor = appAccr;
        }

		/// <summary author="Wes Richardson" created="2019/03/07">
		/// Validtes and passes new appointment data to the appointment accessor
		/// </summary>
		public bool CreateAppointmentByGuest(Appointment appointment)
        {
            bool results = false;
            int rows = 0;
            try
            {
                validateAppointmentData(appointment);
                if (appointmentValid)
                {
                    rows = _appointmentAccessor.InsertAppointmentByGuest(appointment);
                    if (rows > 0)
                    {
                        results = true;
                    }
                }
            }
            catch (Exception ex)
            {
				ExceptionLogManager.getInstance().LogException(ex);
                throw ex;
            }
            return results;
        }

		/// <summary author="Wes Richardson" created="2019/03/07">
		/// gets an appointment based on the given id
		/// </summary>
		public Appointment RetrieveAppointmentByID(int id)
        {
            Appointment appointment = null;
            try
            {
                appointment = _appointmentAccessor.SelectAppointmentByID(id);
                if (appointment == null)
                {
                    throw new ApplicationException("No Data");
                }
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return appointment;
        }

		/// <summary author="Wes Richardson" created="2019/03/07">
		/// Validtes and passes updated appointment data to the appointment accessor
		/// </summary>
		public bool UpdateAppointment(Appointment appointment)
        {
            int rows = 0;
            bool results = false;
            try
            {
                validateAppointmentData(appointment);
                if (appointmentValid)
                {
                    rows = _appointmentAccessor.UpdateAppointment(appointment);
                    if (rows > 0)
                    {
                        results = true;
                    }
                }
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return results;
        }

		/// <summary author="Wes Richardson" created="2019/03/07">
		/// gets appointment types for the selecting from
		/// </summary>
		public List<AppointmentType> RetrieveAppointmentTypes()
        {
            List<AppointmentType> appointmentTypes = null;
            try
            {
                appointmentTypes = _appointmentAccessor.SelectAppointmentTypes();
                if (appointmentTypes == null)
                {
                    throw new ApplicationException("No Appointment Type Data");
                }
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return appointmentTypes;
        }

		/// <summary author="Wes Richardson" created="2019/03/07">
		/// gets a list of guests to add guestID to the appointment
		/// </summary>
		public List<AppointmentGuestViewModel> RetrieveGuestList()
        {
            List<AppointmentGuestViewModel> appointmentGuestViewModelList = null;
            try
            {
                appointmentGuestViewModelList = _appointmentAccessor.SelectGuestList();
                if (appointmentGuestViewModelList == null)
                {
                    throw new ApplicationException("No Guest Data found");
                }
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return appointmentGuestViewModelList;
        }

		/// <summary author="Wes Richardson" created="2019/03/28">
		/// </summary>
		/// <param name="guestID"></param>
		/// <returns>A list of appointments</returns>
		public List<Appointment> RetrieveAppointmentsByGuestID(int guestID)
        {
            List<Appointment> appointments = null;

            try
            {
                appointments = _appointmentAccessor.SelectAppointmentByGuestID(guestID);
                if (appointments == null)
                {
                    throw new ApplicationException("Appointment for Guest Not found");
                }
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return appointments;
        }

		/// <summary author="Wes Richardson" created="2019/03/28">
		/// </summary>
		/// <param name="id"></param>
		/// <returns>If the record was deleted</returns>
		public bool DeleteAppointmentByID(int id)
        {
            bool results = false;
            try
            {
                int rows = _appointmentAccessor.DeleteAppointmentByID(id);
                if (rows > 0)
                {
                    results = true;
                }
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return results;
        }

		/// <summary author="Wes Richardson" created="2019/03/07">
		/// Validates the data for an appoitment
		/// </summary>
		private void validateAppointmentData(Appointment appointment)
        {
            if (appointment.AppointmentType == "")
            {
                appointmentValid = false;
                throw new ApplicationException("No Appointment type selected");
            }
            else if (appointment.AppointmentType.Length > 25)
            {
                appointmentValid = false;
                throw new ApplicationException("Appointment Type too long");
            }
            else if (appointment.StartDate == null && string.IsNullOrEmpty(appointment.StartDate.ToString()))
            {
                appointmentValid = false;
                throw new ApplicationException("No Start date");
            }
            else if (appointment.StartDate.Date < DateTime.Now.Date)
            {
                appointmentValid = false;
                throw new ApplicationException("Cannot create an appointment with a past start time");
            }
            else if (appointment.EndDate == null && string.IsNullOrEmpty(appointment.EndDate.ToString()))
            {
                appointmentValid = false;
                throw new ApplicationException("No end date");
            }
            else if (DateTime.Compare(appointment.EndDate, appointment.StartDate) < 1)
            {
                appointmentValid = false;
                throw new ApplicationException("End Date must come after Start Date");
            }
            else if (appointment.Description.Length > 1000)
            {
                appointmentValid = false;
                throw new ApplicationException("Description is too long");
            }
            appointmentValid = true;
        }

		/// <summary author="Eduardo Colon" created="2019/04/23">
		/// Retrieves  all  appointments
		/// </summary>
		public List<Appointment> RetrieveAppointments()
        {
            var appointments = new List<Appointment>();
            try
            {
                appointments = _appointmentAccessor.SelectAppointments();
                if (appointments == null)
                {
                    throw new ApplicationException("No Data");
                }
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return appointments;
        }
    }
}