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
	/// <summary author="Craig Barkley" created="2019/02/05">
	/// This class is for the appointment Types in the logic layer, to be a connector to the 
	/// the Presentation Layer via the data.
	/// </summary>
	public class AppointmentTypeManager : IAppointmentTypeManager
	{
		private IAppointmentTypeAccessor _appointmentTypeAccessor;

		public AppointmentTypeManager()
		{
			_appointmentTypeAccessor = new AppointmentTypeAccessor();
		}
		public AppointmentTypeManager(IAppointmentTypeAccessor appointmentTypeAccessor)
		{
			_appointmentTypeAccessor = appointmentTypeAccessor;
		}

		/// <summary author="Craig Barkley" created="2019/02/05">
		/// Method that Adds an Appointment Type 
		/// </summary>
		/// <param name="appointmentType">The newAppointmentType is passed to the CreateAppointmentType</param>
		/// <returns> Results </returns>
		public bool AddAppointmentType(AppointmentType newAppointmentType)
		{
			ValidationExtensionMethods.ValidateID(newAppointmentType.AppointmentTypeID);
			ValidationExtensionMethods.ValidateDescription(newAppointmentType.Description);

			bool result = false;

			try
			{
				result = (1 == _appointmentTypeAccessor.CreateAppointmentType(newAppointmentType));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Craig Barkley" created="2019/02/05">
		/// Method that Retrieves All Appointment Types to a list.
		/// </summary>
		/// <returns> appointmentTypes </returns>
		public List<string> RetrieveAllAppointmentTypes()
		{
			List<string> appointmentTypes = null;
			try
			{
				appointmentTypes = _appointmentTypeAccessor.SelectAllAppointmentTypeID();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return appointmentTypes;
		}

		/// <summary author="Craig Barkley" created="2019/02/05">
		/// Method that Retrieves All Appointment Types by Status.
		/// </summary>
		/// <param name="status">The Status of the Appointment Types are retrieved.</param>
		/// <returns> appointmentTypes </returns>
		public List<AppointmentType> RetrieveAllAppointmentTypes(string status)
		{
			List<AppointmentType> appointmentTypes = null;

			if (status != "")
			{
				try
				{
					appointmentTypes = _appointmentTypeAccessor.RetrieveAllAppointmentTypes(status);
				}
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}

			return appointmentTypes;
		}

		/// <summary author="Craig Barkley" created="2019/02/05">
		/// Method that Deletes an Appointment Type.
		/// </summary>
		/// <param name="appointmentType">The Appointment Type are passed to DeleteAppointmentType.</param>
		/// <returns> bool Result. </returns>
		public bool DeleteAppointmentType(string appointmentType)
		{
			bool result = false;
			try
			{
				result = (1 == _appointmentTypeAccessor.DeleteAppointmentType(appointmentType));
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