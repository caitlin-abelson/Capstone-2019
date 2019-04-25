using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using MillennialResortWebSite.Models;
using System.Net.Mail;

namespace MillennialResortWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModelsMixer homeViewModelsMixer = new HomeViewModelsMixer();

           // homeViewModelsMixer.Reservations = new List<ReservationSearchModel>();

            homeViewModelsMixer.Reservation = new List<NewReservation>();

           // homeViewModelsMixer.Rooms = new List<Room>();

            homeViewModelsMixer.MailingList = new IndexPageMailingListViewModel();

            return View(homeViewModelsMixer);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MailingList(HomeViewModelsMixer homeViewModelsMixer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EMailer.EmailUsername = "millennialcontact107@gmail.com";
                    EMailer.EmailPassword = "Supportteam1";

                    EMailer eMailer = new EMailer
                    {
                        ToEmail = homeViewModelsMixer.MailingList.Email,
                        Subject = homeViewModelsMixer.MailingList.FirstName + " "
                                    + homeViewModelsMixer.MailingList.LastName
                                    + " - Subscription to Our Monthly News Letter",
                        Body = "Thank you " + homeViewModelsMixer.MailingList.FirstName
                                    + " for signing up for our mailing list. You will now receive monthly "
                                    + "reports regarding all that is happening with the resort, including any pressing "
                                    + "issues or updates, as well as details regarding special events. "
                                    + "Please note: to opt out of our mailing list at any time, reply with the words OPT OUT",
                        IsHtml = true
                    };
                    eMailer.Send();
                    ViewBag.ResultStatus = "SignupSuccess";
                    ViewBag.FormSubmitResult = "Success! You're now signed up, check your inbox.";
                    return View("ContactResult");
                }
                catch (SmtpException smtpEx)
                {
                    ViewBag.ResultStatus = "Error";
                    //ViewBag.FormSubmitResult = "ERROR: invalid email address.";

                    ViewBag.FormSubmitResult = "ERROR: invalid email address.";
                    return View("ContactResult");
                    //return Content(smtpEx.Message); <---  for smtpException error-testing
                }
            }
            else
            {

                return View(homeViewModelsMixer);
            }
        }

        public ActionResult Contact()
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.SelectedSubject = "";
            contactViewModel.Subjects = new List<SelectListItem>()
            {

                new SelectListItem
              {
                  Text = "--Select a Value--",
                  Value = "placeholder",
                  Disabled = true,
              },
              new SelectListItem
              {
                  Text = "Current Member - Report Issue",
                  Value = "Current Member/Guest - Reporting an issue"
              },
              new SelectListItem
              {
                  Text = "Previous Member - Report Issue",
                  Value = "Previous Member/Guest - Reporting an issue"
              },
              new SelectListItem
              {
                  Text = "Questions about pricing",
                  Value = "Questions about pricing"
              },
              new SelectListItem
              {
                  Text = "Questions about reservations",
                  Value = "Questions about reservations"
              },
              new SelectListItem
              {
                  Text = "Questions about an event",
                  Value = "Questions about an event"
              },
              new SelectListItem
              {
                  Text = "Questions about pets",
                  Value = "Questions about pets"
              },
              new SelectListItem
              {
                  Text = "Questions about jobs",
                  Value = "Questions about job opportunities at the resort"
              },
              new SelectListItem
              {
                  Text = "MISC/OTHER",
                  Value = "MISC/OTHER"
              },
            };

            return View(contactViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel contactViewModel)
        {
            if (!contactViewModel.SelectedSubject.Equals("placeholder") && ModelState.IsValid)
            {
                try
                {
                    EMailer.EmailUsername = contactViewModel.Email;
                    EMailer.EmailPassword = "Supportteam1";

                    EMailer eMailer = new EMailer
                    {
                        ToEmail = "millennialcontact107@gmail.com",
                        Subject = contactViewModel.FirstName + " " + contactViewModel.LastName + " - " + contactViewModel.SelectedSubject,
                        Body = contactViewModel.Description,
                        IsHtml = true
                    };
                    eMailer.Send();
                    ViewBag.ResultStatus = "Success";
                    ViewBag.FormSubmitResult = "Success! Email sent.";
                    return View("ContactResult");
                }
                catch (SmtpException smtpEx)
                {
                    ViewBag.ResultStatus = "Error";
                    ViewBag.FormSubmitResult = "ERROR: invalid email address.";
                    return View("ContactResult");
                }
            }
            else
            {
                if (contactViewModel.SelectedSubject.Equals("placeholder"))
                {
                    ViewBag.DropdownError = "You must select an option from the drop down.";
                }
                else
                {
                    ViewBag.DropdownError = "";
                }


                contactViewModel.Subjects = new List<SelectListItem>()
            {
              new SelectListItem
              {
                  Text = "--Select a Values--",
                  Value = "placeholder",
                  Disabled = true,
              },
              new SelectListItem
              {
                  Text = "Current Member - Report Issue",
                  Value = "Current Member/Guest - Reporting an issue"
              },
              new SelectListItem
              {
                  Text = "Previous Member - Report Issue",
                  Value = "Previous Member/Guest - Reporting an issue"
              },
              new SelectListItem
              {
                  Text = "Questions about pricing",
                  Value = "Questions about pricing"
              },
              new SelectListItem
              {
                  Text = "Questions about reservations",
                  Value = "Questions about reservations"
              },
              new SelectListItem
              {
                  Text = "Questions about an event",
                  Value = "Questions about an event"
              },
              new SelectListItem
              {
                  Text = "Questions about pets",
                  Value = "Questions about pets"
              },
              new SelectListItem
              {
                  Text = "Questions about jobs",
                  Value = "Questions about job opportunities at the resort"
              },
              new SelectListItem
              {
                  Text = "MISC/OTHER",
                  Value = "MISC/OTHER"
              },
            };

                return View(contactViewModel);
                //return RedirectToAction("Contact");
            }

        }

        public ActionResult Rooms()
        {
            ViewBag.Message = "Rooms Page";

            return View();
        }






    }
}