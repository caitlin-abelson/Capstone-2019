using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IAppointmentTypeAccessor
    {
        int CreateAppointmentType(AppointmentType newAppointmentType);
        int DeleteAppointmentType(string appointmentTypeID);
        List<AppointmentType> RetrieveAllAppointmentTypes(string status);
        List<string> SelectAllAppointmentTypeID();
        AppointmentType RetrievAppointmentTypeById(string id);
    }
}