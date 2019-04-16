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

        IEnumerable<Room> rooms;


        // GET: Rooms
        public ActionResult Index()
        {
            roomManager = new RoomManager();
            rooms = roomManager.RetrieveRoomList();
            
            return View(rooms);
        }
    }
}