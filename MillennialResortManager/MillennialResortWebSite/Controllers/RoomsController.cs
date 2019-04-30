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
    public class RoomsController : Controller
    {
        IRoomManager roomManager = new RoomManager();

        IReservationManager reservationManager = new ReservationManagerMSSQL();

        //IEnumerable<Reservation> reservation;

        IEnumerable<Room> rooms;

        IGuestManager _guestManager = new GuestManager();



        // GET: Rooms
        public ActionResult Index(ReservationSearchModel model)
        {
            roomManager = new RoomManager();

            model.Rooms = roomManager.RetrieveRoomList();

            int hour = DateTime.Now.Hour;

            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";

            return View(model);
        }





        [Authorize]
        public ActionResult Create(int id, DateTime start, DateTime end)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            Room room = null;
            Guest guest = new Guest();
            try
            {
                room = roomManager.RetreieveRoomByID(id);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            try
            {
                string email = User.Identity.Name;
                guest = _guestManager.RetrieveGuestByEmail(email);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }


            NewReservation res = new NewReservation()
            {
                ArrivalDate = start,
                DepartureDate = end,
                numberOfGuests = 0,
                numberOfPets = 0,
                roomType = room.RoomType,
                Notes = ""
            };

            return View(res);
        }


        // POST: Reservaion/Create
        [HttpPost]
        public ActionResult Create(NewReservation reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    Guest guest = new Guest();
                    string email = User.Identity.Name;
                    guest = _guestManager.RetrieveGuestByEmail(email);
                    Reservation res = new Reservation()
                    {
                        MemberID = guest.MemberID,
                        NumberOfGuests = reservation.numberOfGuests,
                        ArrivalDate = reservation.ArrivalDate.Value,
                        DepartureDate = reservation.DepartureDate.Value,
                        Notes = reservation.Notes
                    };
                    if (reservationManager.AddReservation(res))
                    {
                        return RedirectToAction("Index", "MyAccount");
                    }
                }
                catch
                {
                    throw;
                }
            }
            return View(reservation);
        }

    }
}