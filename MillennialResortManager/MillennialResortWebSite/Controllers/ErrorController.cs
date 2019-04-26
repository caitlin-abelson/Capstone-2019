using MillennialResortWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        //ErrorViewModel errorViewModel;
        public ErrorController()
        {
            //errorViewModel = new ErrorViewModel("ErrorTitle", "ErrorMessage", "ErrorException", "~/Content/images/SandCastle.jpg", "Sandcastle", 300, 300, "Go Home", "Home", "Index");
        }
        // GET: Error
        public ActionResult Index(ErrorViewModel errorViewModel)
        {
            return View(errorViewModel);
        }
    }
}