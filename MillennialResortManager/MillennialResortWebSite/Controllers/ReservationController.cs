using MillennialResortWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillennialResortWebSite.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Index(ReservationSearchModel model)
        {
            return View();
        }
    }
}