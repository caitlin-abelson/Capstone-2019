using DataAccessLayer;
using DataObjects;
using LogicLayer;
using MillennialResortWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillennialResortWebSite.Controllers
{
    public class AmenitiesController : Controller
    {
        IAppointmentTypeManager apptTypeManager = new AppointmentTypeManager();
        IAppointmentTypeAccessor apptTypeAccessor = new AppointmentTypeAccessorMock();
        IAppointmentAccessor apptAccessor = new AppointmentAccessorMock();
        IGuestManager _guestManager = new GuestManager();
        IAppointmentManager _apptManager = new AppointmentManager();
        // GET: Amenities
        public ActionResult Index()
        {
            List<AppointmentType> appointments = apptTypeManager.RetrieveAllAppointmentTypes("all");

            return View(appointments);
        }

        [Authorize]
        // GET: Amenitites/Create
        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Guest guest = new Guest();
            try
            {
                string email = User.Identity.Name;
                guest = _guestManager.RetrieveGuestByEmail(email);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }

            AppointmentModel appt = new AppointmentModel()
            {
                AppointmentType = id,

                Description = "",
                StartDate = DateTime.Now
            };

            return View(appt);
        }

        // POST: Amenitites/Create
        [HttpPost]
        public ActionResult Create(AppointmentModel appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    Guest guest = new Guest();
                    string email = User.Identity.Name;
                    guest = _guestManager.RetrieveGuestByEmail(email);
                    Appointment appt = new Appointment()
                    {
                        AppointmentType = appointment.AppointmentType,
                        Description = "",
                        StartDate = appointment.StartDate,
                        EndDate = appointment.StartDate.AddDays(1),
                        GuestID = guest.GuestID
                    };
                    if (_apptManager.CreateAppointmentByGuest(appt))
                    {
                        return RedirectToAction("Index", "MyAccount");
                    }
                }
                catch
                {
                    throw;
                }
            }
            return View(appointment);
        }

    }
}