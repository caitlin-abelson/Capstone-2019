using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using MillennialResortWebSite.Models;

namespace MillennialResortWebSite.Controllers
{
    [Authorize]
    public class MyAccountController : Controller
    {
        IGuestManager _guestManager = new GuestManager();

        IMemberTabManager _memberTabManager = new MemberTabManager();

        IMemberTabLineManager _memberTabLineManager = new MemberTabLineManager();

        IMemberManager _memberManager = new MemberManagerMSSQL();

        IAppointmentManager apptManager = new AppointmentManager();

        IReservationManager resManager = new ReservationManagerMSSQL();

        
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
        [Authorize]
        // GET: MyAccount/ViewAppointments/5
        public ActionResult ViewAppointments(int id)
        {
            List<Appointment> appt;
            try
            {
                appt = apptManager.RetrieveAppointmentsByGuestID(id);
            }
            catch (Exception ex)
            {
                TempData["error"] = new ErrorViewModel(
                    Title: "Your Appointments",
                    Message: "We could not pull up a list of your appointments!",
                    ExceptionMessage: ex.Message,
                    ButtonMessage: "Back to Account",
                    ReturnController: "MyAccount",
                    ReturnAction: "Index"
                    );

                return RedirectToAction("Index", "Error");
            }
            return View(appt);
        }
        [Authorize]
        // GET: MyAccount/ViewReservations/5
        public ActionResult ViewReservations(int id)
        {
            Reservation res;
            try
            {
                res = resManager.RetrieveReservationByGuestID(id);
            }
            catch (Exception ex)
            {
                    TempData["error"] = new ErrorViewModel(
                    Title: "Your Reservations",
                    Message: "We could not pull up a list of your reservations!",
                    ExceptionMessage: ex.Message,
                    ButtonMessage: "Back to Account",
                    ReturnController: "MyAccount",
                    ReturnAction: "Index"
                    );

                return RedirectToAction("Index", "Error");
            }
            return View(res);
        }

        /// <summary>
        /// Added by: Matt H. on 4/26/17
        /// </summary>
        // GET: MyAccount/ViewTab/5
        public ActionResult ViewTab()
        {
            try
            {
                string email = User.Identity.Name;

                int id = _memberManager.RetrieveMemberByEmail(email);

                MemberTab memberTab = _memberTabManager.RetrieveActiveMemberTabByMemberID(id);

                List<MemberTabLine> memberTabLines = _memberTabLineManager.RetrieveMemberTabLineByMemberID(memberTab.MemberTabID);

                ViewTabMixer viewTabMixer = new ViewTabMixer
                {
                    MemberTab = memberTab,
                    MemberTabLines = memberTabLines
                };

                return View(viewTabMixer);
            }
            catch (Exception ex)
            {
                TempData["error"] = new ErrorViewModel(
                Title: "Your Tab",
                Message: "We could not pull up your tab!",
                ExceptionMessage: ex.Message,
                ButtonMessage: "Back to Account",
                ReturnController: "MyAccount",
                ReturnAction: "Index"
                );

                return RedirectToAction("Index", "Error");
            }
        }
    }
}