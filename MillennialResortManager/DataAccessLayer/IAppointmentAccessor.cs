﻿/// <summary>
/// Wes Richardson
/// Created: 2019/03/07
/// 
/// Interface for Appointment Access
/// </summary>
using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IAppointmentAccessor
    {
        int InsertAppointment(Appointment appointment);
        Appointment SelectAppointmentByID(int id);
        List<AppointmentType> SelectAppointmentTypes();
        List<AppointmentGuestViewModel> SelectGuestList();
        int UpdateAppointment(Appointment appointment);
    }
}