/// <summary>
/// Alisa Roehr
/// Created: 2019/01/24
/// 
/// class for Guests info. 
/// A Guest is someone that is also staying at the resort with a Member
/// </summary>
/// 
///<remarks>
///
///</remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Guest
    {
        /// <summary>
        /// Author: Alisa Roehr
        /// Created: 2019/01/24
        /// 
        /// A Guest is someone that is also staying at the resort with a Member. 
        /// </summary>
        public int GuestID { get; set; }
        public int MemberID { get; set; }
        public string GuestTypeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Minor { get; set; }
        public bool Active { get; set; }
        public bool ReceiveTexts { get; set; }
        public string EmergencyFirstName { get; set; }
        public string EmergencyLastName { get; set; }
        public string EmergencyPhoneNumber { get; set; }
        public string EmergencyRelation { get; set; }
        public bool CheckedIn { get; set; }

        
        public Guest(int memberID, string fName,
                string lName, string phoneNumber, string mail, bool texts, string emergencyFName, 
                string emergencyLName, string emergencyPhone, string emergencyRelation)
        {
            MemberID = memberID;
            GuestTypeID = "Basic guest";
            FirstName = fName;
            LastName = lName;
            PhoneNumber = phoneNumber;
            Email = mail;
            ReceiveTexts = texts;
            EmergencyFirstName = emergencyFName;
            EmergencyLastName = emergencyLName;
            EmergencyPhoneNumber = emergencyPhone;
            EmergencyRelation = emergencyRelation;
            Minor = false;
            Active = true;
            CheckedIn = false;
        }
        public Guest()
        {
        }
    }
}
