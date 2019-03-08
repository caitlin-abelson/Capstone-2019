using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class AppointmentTypeAccessorMock : IAppointmentTypeAccessor
    {
        /// <summary>
        /// Craig Barkley
        /// Created: 2019/02/28
        /// 
        /// This is a mock Data Accessor which implements IAppointmentTypeAccessor.  This is for testing purposes only.
        /// </summary>
        /// 

        private List<AppointmentType> appointmentType;
        /// <summary>
        /// Author: Craig Barkley
        /// Created: 2019/02/28
        /// This constructor that sets up dummy data
        /// </summary>
        public AppointmentTypeAccessorMock()
        {
            appointmentType = new List<AppointmentType>
            {
               new AppointmentType {AppointmentTypeID = "AppointmentType1", Description = "AppointmentType is a appointmentType"},
               new AppointmentType {AppointmentTypeID = "AppointmentType2", Description = "AppointmentType is a appointmentType"},
               new AppointmentType {AppointmentTypeID = "AppointmentType3", Description = "AppointmentType is a appointmentType"},
               new AppointmentType {AppointmentTypeID = "AppointmentType4", Description = "AppointmentType is a appointmentType"}
            };
        }

        public int CreateAppointmentType(AppointmentType newAppointmentType)
        {
            int listLength = appointmentType.Count;
            appointmentType.Add(newAppointmentType);
            if (listLength == appointmentType.Count - 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int DeleteAppointmentType(string appointmentTypeID)
        {
            int rowsDeleted = 0;
            foreach (var type in appointmentType)
            {
                if (type.AppointmentTypeID == appointmentTypeID)
                {
                    int listLength = appointmentType.Count;
                    appointmentType.Remove(type);
                    if (listLength == appointmentType.Count - 1)
                    {
                        rowsDeleted = 1;
                    }
                }
            }

            return rowsDeleted;
        }

        public List<string> SelectAllAppointmentTypeID()
        {
            throw new NotImplementedException();
        }

        public List<AppointmentType> RetrieveAllAppointmentTypes(string status)
        {
            return appointmentType;
        }
    }
}
