using MillennialResortWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;

namespace MillennialResortWebSite.Controllers
{
    public class ReservationController : Controller
    {
        private IRoomManager _roomManager;

        IEnumerable<Room> _rooms;
        // GET: Reservation
        public ActionResult Index(ReservationSearchModel model)
        {
            _roomManager = new RoomManager();

            _rooms = _roomManager.RetrieveRoomList();



            return View(_rooms);
        }



    }
}