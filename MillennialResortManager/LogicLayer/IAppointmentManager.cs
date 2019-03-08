/// <summary>
/// Wes Richardson
/// Created: 2019/03/07
/// 
/// Interface for Appointment Manager
/// </summary>
using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IAppointmentManager
    {
        bool CreateAppointment(Appointment appointment);
        Appointment RetrieveAppointmentByID(int id);
        List<AppointmentType> RetrieveAppointmentTypes();
        List<AppointmentGuestViewModel> RetrieveGuestList();
        bool UpdateAppointment(Appointment appointment);
    }
}