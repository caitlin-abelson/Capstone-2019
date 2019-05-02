using DataAccessLayer;
using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillennialResortWebSite.Controllers
{
    public class EventController : Controller
    {
        IEventManager eventManager = new EventManager();


        // GET: Event
        public ActionResult Index()
        {
            List<Event> events = eventManager.RetrieveAllEvents();

            return View(events);
        }
    }
}