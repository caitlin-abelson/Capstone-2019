using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Event
    {
        /// <summary>
        /// @Author Phillip Hansen
        /// @Created 1/23/2019
        /// 
        /// Updated: 3/1/2019 by Phillip Hansen
        /// Updated fields to match new definition in Data Dictionary
        /// 
        /// Updated: 3/29/2019 by Phillip Hansen
        /// Updated fields to match new definitions in Data Dictionary
        /// 
        /// Creates the Event Request Object
        /// </summary>
        public int EventID { get; set; }
        public string EventTitle { get; set; }
        public int OfferingID { get; set; }
        //public string OfferingName { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EventTypeID { get; set; }
        public string Description { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public bool KidsAllowed { get; set; }
        public int NumGuests { get; set; }
        public int SeatsRemaining { get; set; }
        public string Location { get; set; }
        public bool Sponsored { get; set; }
        public bool Approved { get; set; }
        public bool PublicEvent { get; set; }


        ////Constructors for Event Request since setters are private
        ////NOTE: These are for when CREATING a new Event Request
        //public Event(string eventReqTitle, int employeeID, string eventTypeID,
        //    string description, DateTime eventReqStartDate, DateTime eventEndDate,
        //    bool kidsAllowed, int numGuests, string location, bool sponsored, int //SponsorID, bool approved)
        //{
        //    EventTitle = eventReqTitle;
        //    EmployeeID = employeeID;
        //    EventTypeID = eventTypeID;
        //    Description = description;
        //    EventStartDate = eventReqStartDate;
        //    EventEndDate = eventEndDate;
        //    KidsAllowed = kidsAllowed;
        //    NumGuests = numGuests;
        //    Location = location;
        //    Sponsored = sponsored;
        //    //SponsorID = //SponsorID;
        //    Approved = approved;
        //}
        //public Event(string eventReqTitle, int employeeID, string eventTypeID,
        //    string description, DateTime eventReqStartDate, DateTime eventEndDate,
        //    bool kidsAllowed, int numGuests, string location, bool sponsored, bool approved)
        //{
        //    EventTitle = eventReqTitle;
        //    EmployeeID = employeeID;
        //    EventTypeID = eventTypeID;
        //    Description = description;
        //    EventStartDate = eventReqStartDate;
        //    EventEndDate = eventEndDate;
        //    KidsAllowed = kidsAllowed;
        //    NumGuests = numGuests;
        //    Location = location;
        //    Sponsored = sponsored;
        //    Approved = approved;
        //}

        ////Constructors for Event Request since setters are private
        //public Event(int eventReqID, string eventReqTitle, int employeeID, string eventTypeID,
        //    string description, DateTime eventReqStartDate, DateTime eventEndDate,
        //    bool kidsAllowed, int numGuests, string location, bool sponsored, bool approved)
        //{
        //    EventID = eventReqID;
        //    EventTitle = eventReqTitle;
        //    EmployeeID = employeeID;
        //    EventTypeID = eventTypeID;
        //    Description = description;
        //    EventStartDate = eventReqStartDate;
        //    EventEndDate = eventEndDate;
        //    KidsAllowed = kidsAllowed;
        //    NumGuests = numGuests;
        //    Location = location;
        //    Sponsored = sponsored;
        //    Approved = approved;
        //}

        //public Event(int eventReqID, string eventReqTitle, int employeeID, string eventTypeID,
        //    string description, DateTime eventReqStartDate, DateTime eventEndDate,
        //    bool kidsAllowed, int numGuests, string location, bool sponsored, int //SponsorID, bool approved)
        //{
        //    EventID = eventReqID;
        //    EventTitle = eventReqTitle;
        //    EmployeeID = employeeID;
        //    EventTypeID = eventTypeID;
        //    Description = description;
        //    EventStartDate = eventReqStartDate;
        //    EventEndDate = eventEndDate;
        //    KidsAllowed = kidsAllowed;
        //    NumGuests = numGuests;
        //    Location = location;
        //    Sponsored = sponsored;
        //    //SponsorID = //SponsorID;
        //    Approved = approved;
        //}


    }
}
