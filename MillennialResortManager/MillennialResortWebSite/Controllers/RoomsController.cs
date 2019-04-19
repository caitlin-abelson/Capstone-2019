using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillennialResortWebSite.Controllers
{
    public class RoomsController : Controller
    {

        IRoomManager roomManager;
        IReservationManager reservationManager;

        IEnumerable<Reservation> reservation;
        IEnumerable<Room> rooms;


        // GET: Rooms
        public ActionResult Index()
        {
            roomManager = new RoomManager();
            rooms = roomManager.RetrieveRoomList();

            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";


            return View(rooms);
        }



        public ActionResult SearchRoomAvailability()
        {
            reservationManager = new ReservationManagerMSSQL();
            reservation = reservationManager.RetrieveAllReservations();

            return View(reservation);
        }




    }
}