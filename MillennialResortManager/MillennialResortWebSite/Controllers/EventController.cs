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

    public class EventController : Controller
    {
        IEventManager eventManager = new EventManager();

        IMemberManager _memberManager = new MemberManagerMSSQL();

        IGuestManager _guestManager = new GuestManager();

        IMemberTabManager _memberTabManager = new MemberTabManager();

        IMemberTabLineManager _memberTabLineManager = new MemberTabLineManager();

        IOfferingManager _offeringManager = new OfferingManager();

        // GET: Event
        public ActionResult Index()
        {
            List<Event> events = eventManager.RetrieveAllEvents();

            return View(events);
        }

        [Authorize]
        // GET: Event/Buy
        public ActionResult Buy(int id)
        {
            Guest guest = new Guest();
            Event theEvent = null;
            try
            {
                string email = User.Identity.Name;
                guest = _guestManager.RetrieveGuestByEmail(email);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                theEvent = eventManager.RetrieveEventByID(id);
            }
            catch (Exception ex)
            {
                TempData["error"] = new ErrorViewModel(
                    Title: "Events",
                    Message: "We could not purhcase your ticket at this time ",
                    ExceptionMessage: ex.Message,
                    ButtonMessage: "Try again",
                    ReturnController: "Event",
                    ReturnAction: "Index"
                    );

                return RedirectToAction("Index", "Error");
            }
            BuyTicketModel ticket = new BuyTicketModel()
            {
                EventID = id,
                EventTitle = theEvent.EventTitle,
                Price = theEvent.Price,
                Quantity = 0,
                OfferingID = theEvent.OfferingID,
                Date = theEvent.EventStartDate
            };
            return View(ticket);
        }

        // POST: Amenitites/Create
        [HttpPost]
        public ActionResult Buy(BuyTicketModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guest guest = new Guest();
                    string email = User.Identity.Name;
                    int memberId = _memberManager.RetrieveMemberByEmail(email);
                    guest = _guestManager.RetrieveGuestByEmail(email);
                    MemberTab memberTab = _memberTabManager.RetrieveActiveMemberTabByMemberID(memberId);
                    Offering offering = _offeringManager.RetrieveOfferingByID(model.OfferingID);

                    MemberTabLine tab = new MemberTabLine()
                    {
                        MemberTabID = memberTab.MemberTabID,
                        OfferingID = model.OfferingID,
                        Quantity = model.Quantity,
                        Price = model.Price,
                        EmployeeID = offering.EmployeeID,
                        Discount = 0,
                        GuestID = guest.GuestID,
                        DatePurchased = DateTime.Now
                    };
                    if (_memberTabManager.CreateMemberTabLine(tab) != 0)
                    {
                        TempData["success"] = new SuccessViewModel(
                            Title: "an Event!",
                            dateTime: model.Date.ToShortDateString(),
                            type: model.EventTitle,
                            time: " the greatest time of your life",
                            ButtonMessage: "Go to Account",
                            ReturnController: "MyAccount",
                            ReturnAction: "Index"
                    );

                        return RedirectToAction("Index", "Success");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = new ErrorViewModel(
                    Title: "Events",
                    Message: "We could not purhcase your ticket at this time ",
                    ExceptionMessage: ex.Message,
                    ButtonMessage: "Try again",
                    ReturnController: "Event",
                    ReturnAction: "Index"
                    );

                    return RedirectToAction("Index", "Error");
                }
            }
            return View(model);
        }
    }
}