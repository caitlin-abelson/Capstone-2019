using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillennialResortWebSite.Controllers
{
    public class BuildingController : Controller
    {
        IBuildingAccessor buildingAccessor = new BuildingAccessorMock();
        // GET: Building
        public ActionResult Index()
        {
            List<Building> buildings = buildingAccessor.SelectAllBuildings();

            return View(buildings);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Building building = buildingAccessor.SelectBuildingByID(id);
            return View(building);
        }
    }
}