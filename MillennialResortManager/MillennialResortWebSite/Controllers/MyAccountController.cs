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
    public class MyAccountController : Controller
    {
        IGuestManager _guestManager = new GuestManager();


        // GET: MyAccount
        public ActionResult Index()
        {
            try
            {
                Guest guest = new Guest();

                string email = User.Identity.Name;
                guest = _guestManager.RetrieveGuestByEmail(email);

                return View(guest);
            }
            catch
            {
                RedirectToAction("Index", "Home");
            }
            return View();
            
        }


        // GET: MyAccount/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Guest guest = new Guest();

                string email = User.Identity.Name;

                guest = _guestManager.RetrieveGuestByEmail(email);

                return View(guest);
            }
            catch
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: MyAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Guest newGuest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string email = User.Identity.Name;
                    Guest oldGuest = _guestManager.RetrieveGuestByEmail(email);
                    Guest guest = new Guest(
                        guestId: oldGuest.GuestID,
                        memberID: oldGuest.MemberID,
                        fName: newGuest.FirstName,
                        lName: newGuest.LastName,
                        mail: newGuest.Email,
                        phoneNumber: newGuest.PhoneNumber,
                        emergencyFName: newGuest.EmergencyFirstName,
                        emergencyLName: newGuest.EmergencyLastName,
                        emergencyPhone: newGuest.EmergencyPhoneNumber,
                        emergencyRelation: newGuest.EmergencyRelation,
                        texts: newGuest.ReceiveTexts
                        );
                    _guestManager.EditGuest(guest, oldGuest);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View(newGuest);
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