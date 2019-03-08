using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;


namespace LogicLayer
{
    public class AppointmentTypeManager : IAppointmentTypeManager
    {
        /// <summary>
        /// @Author Craig Barkley
        /// @Created 2/05/2019
        /// 
        /// This class is for the appointment Types in the logic layer, to be a connector to the 
        /// the Presentation Layer via the data.
        /// </summary>
        /// <param name="newAppointmentType"></param>
        /// <returns></returns>
        /// 
        private IAppointmentTypeAccessor _appointmentTypeAccessor;

        public AppointmentTypeManager()
        {
            _appointmentTypeAccessor = new AppointmentTypeAccessor();
        }
        public AppointmentTypeManager(IAppointmentTypeAccessor appointmentTypeAccessor)
        {
            _appointmentTypeAccessor = appointmentTypeAccessor;
        }
        //public AppointmentTypeManager(AppointmentTypeAccessorMock mock)
        //{
        //    _appointmentTypeAccessor = new AppointmentTypeAccessorMock();
        //}


        /// <summary>
        /// Method that Adds an Appointment Type 
        /// </summary>
        /// <param name="appointmentType">The newAppointmentType is passed to the CreateAppointmentType</param>
        /// <returns> Results </returns>

        //Method for creating a new Event Request
        public bool AddAppointmentType(AppointmentType newAppointmentType)
        {
            ValidationExtensionMethods.ValidateID(newAppointmentType.AppointmentTypeID);
            ValidationExtensionMethods.ValidateDescription(newAppointmentType.Description);

            bool result = false;

            try
            {
                result = (1 == _appointmentTypeAccessor.CreateAppointmentType(newAppointmentType));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        /// <summary>
        /// Method that Retrieves All Appointment Types to a list.
        /// </summary>
        /// <param name="">The ID of the Appointment Type are retrieved.</param>
        /// <returns> appointmentTypes </returns>
        public List<string> RetrieveAllAppointmentTypes()
        {
            List<string> appointmentTypes = null;
            try
            {
                appointmentTypes = _appointmentTypeAccessor.SelectAllAppointmentTypeID();
            }
            catch (Exception)
            {
                throw;
            }
            return appointmentTypes;
        }

        /// <summary>
        /// Method that Retrieves All Appointment Types by Status.
        /// </summary>
        /// <param name="">The Status of the Appointment Types are retrieved.</param>
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
                catch (Exception)
                {
                    throw;
                }
            }

            return appointmentTypes;
        }


        /// <summary>
        /// Method that Deletes an Appointment Type.
        /// </summary>
        /// <param name="">The Appointment Type are passed to DeleteAppointmentType.</param>
        /// <returns> bool Result. </returns>

        public bool DeleteAppointmentType(string appointmentType)
        {
            bool result = false;
            try
            {
                result = (1 == _appointmentTypeAccessor.DeleteAppointmentType(appointmentType));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

    }
}

