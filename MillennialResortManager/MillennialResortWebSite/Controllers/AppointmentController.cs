using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using MillennialResortWebSite.Models;

namespace MillennialResortWebSite.Controllers
{
    /// <summary>
    /// Wes Richardson
    /// Created: 2019/04/11
    /// 
    /// Controller for Appointments
    /// </summary>
    public class AppointmentController : Controller
    {
        private IAppointmentManager _appointmentMgr;
        private int _guestID = 0;
        private List<Appointment> _appointments;
        private IReservationManager _resvMgr;
        private int minDate = 0;
        private int maxDate = 0;
        private int plusYears = 0;

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/11
        /// 
        /// </summary>
        public AppointmentController()
        {
            _guestID = 100000;
            _appointmentMgr = new AppointmentManager();
            _resvMgr = new ReservationManagerMSSQL();
            ViewBag.errorMessage = "";
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/11
        /// 
        /// </summary>
        /// <returns>Appointment Index View</returns>
        public ActionResult Index()
        {
            try
            {
                _appointments = _appointmentMgr.RetrieveAppointmentsByGuestID(_guestID);
                if (_appointments == null || _appointments.Count < 1)
                {
                    ViewBag.errorMessage = "No Appointments could be found";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                RedirectToAction("Error");
            }
            return View(_appointments);
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/11
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Detail View of an Appointment</returns>
        public ActionResult Details(int id)
        {
            Appointment appt = null;
            try
            {
                appt = _appointmentMgr.RetrieveAppointmentByID(id);
            }
            catch (Exception)
            {
                
                throw;
            }
            return View(appt);
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/17
        /// 
        /// The Get for Creating a Spa Appointment
        /// </summary>
        /// <returns>A View for Creating Spa Appointments</returns>
        public ActionResult Spa()
        {
            //SetCalander();
            return View();
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/17
        /// 
        /// THe Post for Create Spa
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Spa(CreateAppointmentViewModel cavm)
        {
            int length = 3;
            CreateAppointment(cavm, length, "Spa");
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/17
        /// 
        /// The Get for Creating a Massage Appointment
        /// </summary>
        /// <returns>A View for Creating Massage Appointments</returns>
        public ActionResult Massage()
        {
            //SetCalander();
            return View();
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/17
        /// 
        /// THe Post for Create Massage Appointment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Massage(CreateAppointmentViewModel cavm)
        {
            int length = 1;
            CreateAppointment(cavm, length, "Massage");
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/17
        /// 
        /// The Get for Creating a Whale Watching Appointment
        /// </summary>
        /// <returns>A View for Creating Whale Watching Appointments</returns>
        public ActionResult WhaleWatching()
        {
            //SetCalander();
            return View();
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/17
        /// 
        /// THe Post for Create Whale Watching Appointment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult WhaleWatching(CreateAppointmentViewModel cavm)
        {
            int length = 3;
            CreateAppointment(cavm, length, "Whale Watching");
            return RedirectToAction("Index");
           
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/17
        /// 
        /// The Get for Creating a Sandcastle Building Appointment
        /// </summary>
        /// <returns>A View for Creating Sandcastle Building Appointments</returns>
        public ActionResult SandcastleBuilding()
        {
            //SetCalander();
            return View();
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/17
        /// 
        /// THe Post for Create Sandcastle Building
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SandcastleBuilding(CreateAppointmentViewModel cavm)
        {
            int length = 1;
            CreateAppointment(cavm, length, "Sandcastle Building");
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Wes Richardson
        /// Create: 2019/04/18
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Wes Richardson
        /// Create: 2019/04/18
        /// 
        /// Helper Method to get the blackout dates for the Datapicker
        /// </summary>
        private void SetCalander()
        {
            double minDateDouble = 0;
            double maxDateDouble = 0;
            try
            {
                var resv = _resvMgr.RetrieveReservationByGuestID(_guestID);
                if (resv.ArrivalDate > DateTime.Now || resv.DepartureDate > DateTime.Now)
                {
                    minDateDouble = (resv.ArrivalDate - DateTime.Now).TotalDays;
                    maxDateDouble = (resv.DepartureDate - DateTime.Now).TotalDays;
                    plusYears = resv.DepartureDate.Year - DateTime.Now.Year;
                    minDate = Convert.ToInt32(minDateDouble);
                    maxDate = Convert.ToInt32(maxDateDouble);
                }
            }
            catch (Exception)
            {

                throw;
            }
            ViewBag.minDate = minDate;
            ViewBag.maxDate = minDate;
            ViewBag.plusYears = plusYears;
        }

        /// <summary>
        /// Wes Richardson
        /// Create: 2019/04/18
        /// 
        /// Helper Method that creates the appointment
        /// </summary>
        /// <param name="cavm"></param>
        /// <param name="length"></param>

        private void CreateAppointment(CreateAppointmentViewModel cavm, int length, string type)
        {
            try
            {
                Appointment appointment = new Appointment
                {
                    AppointmentType = type,
                    StartDate = cavm.StartDay.AddHours(cavm.StartTime),
                    EndDate = cavm.StartDay.AddHours(cavm.StartTime + length),
                    Description = cavm.Description,
                    GuestID = _guestID
                };
                if (appointment.Description == null)
                {
                    appointment.Description = "";
                }
                _appointmentMgr.CreateAppointmentByGuest(appointment);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
            }
        }
    }
}
