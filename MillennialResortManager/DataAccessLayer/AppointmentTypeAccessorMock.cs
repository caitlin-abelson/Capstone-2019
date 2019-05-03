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
               new AppointmentType {AppointmentTypeID = "Massage Therapy", Description = "Our Peaceful Massage is a professional massage clinic." +
                                                            "We have multiple licensed massage therapists and are open seven days a week." +
                                                            "Come in, relax, and take your time truly enveloping yourself in a peaceful environment."},

               new AppointmentType {AppointmentTypeID = "Sand Castle Building", Description = "Schedule an appointment with one of our professional " +
                                                                 "sand castle builders. Come build like the pros!"},

               new AppointmentType {AppointmentTypeID = "Spa", Description = "Come relax at our 5-star spa where our location uses mineral-rich spring water " +
                                                                 "(and sometimes seawater) to give medicinal baths and rejuvenate those pores!"},
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

        public AppointmentType RetrievAppointmentTypeById(string id)
        {
            AppointmentType apptType = null;
            foreach (var type in appointmentType)
            {
                if (type.AppointmentTypeID == id)
                {
                    apptType = new AppointmentType()
                    {
                        AppointmentTypeID = type.AppointmentTypeID,
                        Description = type.Description
                    };
                }
            }
            if (apptType == null)
            {
                throw new Exception("Appointment Type not found.");
            }
            return apptType;

        }
    }
}
