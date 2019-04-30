using MillennialResortWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MillennialResortWebSite.Controllers
{
    /// <summary>
    /// Wes Richardson
    /// Created: 2019/04/25
    /// 
    /// Controller for Error View
    /// </summary>
    public class ErrorController : Controller
    {
        /*
         * Code Block to use this error page
         * Insert your own data for each property
         *TempData["error"] = new ErrorViewModel(
          Title: "You've been screwed!", 
          Message: "All rooms are currently sealed with crime scene tape.", 
          ExceptionMessage: ex.Message, 
          ImageLocation: "~/Content/images/errorPics/ErrorPic.jpg",
          ImageAlt: "Error",
          ImageHeight: 221, 
          ImageWidth: 340,
          ButtonMessage: "Try Again",
          ReturnController: "Home", 
          ReturnAction: "Index"
          );

                return RedirectToAction("Index", "Error");
         */
        // GET: Error
        public ActionResult Index()
        {
            //TempData["error"] = new ErrorViewModel(
            //Title: "You've been screwed!",
            //Message: "All rooms are currently sealed with crime scene tape.",
            //ExceptionMessage: ex.Message,
            //ImageLocation: "~/Content/images/errorPics/ErrorPic.jpg",
            //ImageAlt: "Error",
            //ImageHeight: 221,
            //ImageWidth: 340,
            //ButtonMessage: "Try Again",
            //ReturnController: "Home",
            //ReturnAction: "Index"
            //);

            //return RedirectToAction("Index", "Error");

            return View((ErrorViewModel)TempData["error"]);
        }
    }
}