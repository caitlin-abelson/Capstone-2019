using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;

namespace MillennialResortWebSite.Controllers
{
    public class AppointmentController : Controller
    {
        IAppointmentManager _appointmentMgr;
        Guest _guest;
        List<Appointment> _appointments;


        public AppointmentController()
        {
            int _guestID = 100000;
            _appointmentMgr = new AppointmentManager();

            try
            {
                _appointments = _appointmentMgr.RetrieveAppointmentsByGuestID(_guestID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Appointment
        public ActionResult Index()
        {
            return View(_appointments);
        }

        // GET: Appointment/Details/5
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

        // GET: Appointment/Create
        public ActionResult CreateSpa()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        public ActionResult CreateSpa(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Create
        public ActionResult CreateMassage()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        public ActionResult CreateMassage(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Create
        public ActionResult CreateWhaleWatching()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        public ActionResult CreateWhaleWatching(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Edit/5
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
    }
}
