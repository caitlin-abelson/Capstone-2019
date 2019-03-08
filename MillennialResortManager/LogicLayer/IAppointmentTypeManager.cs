using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IAppointmentTypeManager
    {
        bool AddAppointmentType(AppointmentType newAppointmentType);
        bool DeleteAppointmentType(string appointmentType);
        List<string> RetrieveAllAppointmentTypes();
        List<AppointmentType> RetrieveAllAppointmentTypes(string status);
    }
}