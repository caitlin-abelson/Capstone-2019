
using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Wes Richardson" created="2019/03/07">
	/// Interface for Appointment Manager
	/// </summary>
    public interface IAppointmentManager
    {
        bool CreateAppointmentByGuest(Appointment appointment);
        Appointment RetrieveAppointmentByID(int id);
        List<AppointmentType> RetrieveAppointmentTypes();
        List<AppointmentGuestViewModel> RetrieveGuestList();
        bool UpdateAppointment(Appointment appointment);
        List<Appointment> RetrieveAppointmentsByGuestID(int guestID);
        bool DeleteAppointmentByID(int id);
        List<Appointment> RetrieveAppointments();
    }
}