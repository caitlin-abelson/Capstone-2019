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

        IEnumerable<String> _times;

        public AmenitiesController()
        {
            List<string> time = new List<string>{
                "8:00 am",
                "8:30 am",
                "9:00 am",
                "9:30 am",
                "10:00 am",
                "10:30 am",
                "11:00 am",
                "11:30 am",
                "12:00 pm",
                "12:30 pm",
                "1:00 pm",
                "1:30 pm",
                "2:00 pm",
                "2:30 pm",
                "3:00 pm",
                "3:30 pm",
                "4:00 pm",
                "4:30 pm",
                "5:00 pm",
                "5:30 pm",
                "6:00 pm",
                "6:30 pm"
            };
            try
            {
                _times = time;
            }
            catch (Exception)
            {
                // redirect to error page if need be
                throw;
            }
        }

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
                Description = "Incomplete",
                StartDate = DateTime.Now
            };
            ViewBag.Times = _times;
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
                        Description = appointment.Description,
                        StartDate = appointment.StartDate,
                        EndDate = appointment.StartDate.AddDays(1),
                        GuestID = guest.GuestID
                    };
                    if (_apptManager.CreateAppointmentByGuest(appt))
                    {
                        TempData["success"] = new SuccessViewModel(
                            Title: "an Appointment!",
                            dateTime: appointment.StartDate.ToShortDateString(),
                            type: appointment.AppointmentType,
                            time: appointment.Time,
                            ButtonMessage: "Go to Account",
                            ReturnController: "MyAccount",
                            ReturnAction: "Index"
                    );

                        return RedirectToAction("Index", "Success");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = new ErrorViewModel(
                    Title: "Appointment",
                    Message: "We could not schedule you an appoinment for " + appointment.AppointmentType,
                    ExceptionMessage: ex.Message,
                    ButtonMessage: "Back to Amenities",
                    ReturnController: "Amenitites",
                    ReturnAction: "Index"
                    );

                    return RedirectToAction("Index", "Error");
                }
            }
            return View(appointment);
        }

    }
}