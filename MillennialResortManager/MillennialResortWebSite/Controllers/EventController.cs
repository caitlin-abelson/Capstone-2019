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
        IEventAccessor eventAccessor = new MockEventAccessor();


        // GET: Event
        public ActionResult Index()
        {
            List<Event> events = eventAccessor.selectAllEvents();

            return View(events);
        }
    }
}