using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillennialResortWebSite.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        public ActionResult Index()
        {
            return View();
        }


        // GET: MyAccount/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyAccount/Edit/5
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

        // GET: MyAccount/ViewAppointments/5
        public ActionResult ViewAppointments(int id)
        {
            return View();
        }

        // POST: MyAccount/ViewAppointments/5
        [HttpPost]
        public ActionResult ViewAppointments(int id, FormCollection collection)
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

        // GET: MyAccount/ViewReservations/5
        public ActionResult ViewReservations(int id)
        {
            return View();
        }

        // POST: MyAccount/ViewReservations/5
        [HttpPost]
        public ActionResult ViewReservations(int id, FormCollection collection)
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

        // GET: MyAccount/AddGuests/5
        public ActionResult AddGuests(int id)
        {
            return View();
        }

        // POST: MyAccount/AddGuests/5
        [HttpPost]
        public ActionResult AddGuests(int id, FormCollection collection)
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


        // GET: MyAccount/AddEmergencyContact/5
        public ActionResult AddEmergencyContact(int id)
        {
            return View();
        }

        // POST: MyAccount/AddEmergencyContact/5
        [HttpPost]
        public ActionResult AddEmergencyContact(int id, FormCollection collection)
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

        // GET: MyAccount/Delete/5
        public ActionResult Deactivate(int id)
        {
            return View();
        }

        // POST: MyAccount/Delete/5
        [HttpPost]
        public ActionResult Deactivate(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}