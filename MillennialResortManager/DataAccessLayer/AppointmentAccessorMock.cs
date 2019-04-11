/// <summary>
/// Wes Richardson
/// Created: 2019/03/07
/// 
/// Mock Data for testing Appointment Manager
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class AppointmentAccessorMock : IAppointmentAccessor
    {
        private List<Appointment> _appointments;
        private List<AppointmentType> _appointmentTypes;
        private List<AppointmentGuestViewModel> _guestViewModels;
        private int nextAppID;

        public AppointmentAccessorMock()
        {
            _appointments = new List<Appointment>();
            _appointmentTypes = new List<AppointmentType>();
            _guestViewModels = new List<AppointmentGuestViewModel>();
            nextAppID = 100000;
            BuildAppointmentList();
            BuildAppointmentTypeList();
            BuildGuestList();
        }

        public int InsertAppointmentByGuest(Appointment appointment)
        {
            appointment.AppointmentID = nextAppID;
            _appointments.Add(appointment);
            return 1;
        }

        public Appointment SelectAppointmentByID(int id)
        {
            Appointment apt = null;
            foreach (var appointment in _appointments)
            {
                if (appointment.AppointmentID == id)
                {
                    apt = appointment;
                }
            }
            return apt;
        }

        public List<AppointmentType> SelectAppointmentTypes()
        {
            return _appointmentTypes;
        }

        public List<AppointmentGuestViewModel> SelectGuestList()
        {
            return _guestViewModels;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        /// 
        /// Selects Appointments based on a guest ID
        /// </summary>
        /// <param name="guestID"></param>
        /// <returns>A list of Mock Appointments</returns>
        public List<Appointment> SelectAppointmentByGuestID(int guestID)
        {
            List<Appointment> appointments = new List<Appointment>();

            foreach (var appointment in _appointments)
            {
                if (appointment.GuestID == guestID)
                {
                    appointments.Add(appointment);
                }
            }

            return appointments;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/28
        /// 
        /// Deletes Mock Appointments by Given ID
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns>Number of Rows affected</returns>
        public int DeleteAppointmentByID(int appointmentID)
        {
            int rows = 0;

            foreach (var appointment in _appointments)
            {
                if (appointment.AppointmentID == appointmentID)
                {
                    _appointments.Remove(appointment);
                    rows = 1;
                    break;
                }
            }

            return rows;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/03/07
        ///  
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public int UpdateAppointment(Appointment appointment)
        {
            int row = 0;
            foreach (var appt in _appointments)
            {
                if (appt.AppointmentID == appointment.AppointmentID)
                {
                    _appointments.Remove(appt);
                    _appointments.Add(appointment);
                    row = 1;
                    break;
                }
            }
            return row;
        }

        private void BuildAppointmentTypeList()
        {
            AppointmentType at1 = new AppointmentType()
            {
                AppointmentTypeID = "Test Type 1",
                Description = "Test Type 1"
            };
            _appointmentTypes.Add(at1);

            AppointmentType at2 = new AppointmentType()
            {
                AppointmentTypeID = "Test Type 2",
                Description = "Test Type 2"
            };
            _appointmentTypes.Add(at2);

            AppointmentType at3 = new AppointmentType()
            {
                AppointmentTypeID = "Test Type 3",
                Description = "Test Type 3"
            };
            _appointmentTypes.Add(at3);

            AppointmentType at4 = new AppointmentType()
            {
                AppointmentTypeID = "Test Type 4",
                Description = "Test Type 4"
            };
            _appointmentTypes.Add(at4);
        }
        private void BuildGuestList()
        {
            AppointmentGuestViewModel apgm1 = new AppointmentGuestViewModel()
            {
                GuestID = 100000,
                FirstName = "John",
                LastName = "Doe",
                Email = "John@Company.com"
            };
            _guestViewModels.Add(apgm1);

            AppointmentGuestViewModel apgm2 = new AppointmentGuestViewModel()
            {
                GuestID = 100001,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "Jane@Company.com"
            };
            _guestViewModels.Add(apgm2);
        }
        private void BuildAppointmentList()
        {

            Appointment apt1 = new Appointment()
            {
                AppointmentID = nextAppID,
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(2020, 12, 25, 10, 30, 50),
                EndDate = new DateTime(2020, 12, 25, 10, 50, 50),
                Description = "Spa"
            };
            nextAppID++;
            _appointments.Add(apt1);

            Appointment apt2 = new Appointment()
            {
                AppointmentID = nextAppID,
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(2020, 12, 26, 10, 30, 50),
                EndDate = new DateTime(2020, 12, 26, 10, 50, 50),
                Description = "Spa"
            };
            nextAppID++;
            _appointments.Add(apt2);

            Appointment apt3 = new Appointment()
            {
                AppointmentID = nextAppID,
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(2020, 12, 27, 10, 30, 50),
                EndDate = new DateTime(2020, 12, 27, 10, 50, 50),
                Description = "Spa"
            };
            nextAppID++;
            _appointments.Add(apt3);

        }
    }
}
