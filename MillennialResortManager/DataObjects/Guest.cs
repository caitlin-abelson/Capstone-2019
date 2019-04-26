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
	/// <summary>
	/// Author: Alisa Roehr
	/// Created: 2019/01/24
	/// 
	/// A Guest is someone that is also staying at the resort with a Member. 
	/// <remarks>
	/// Austin Delaney
	/// Date: 2019/01/06
	/// 
	/// Implemented ISender and IMessagable interface
	/// </remarks>
	/// </summary>
	public class Guest : ISender
    {

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

        /*
        // Need constructor with all inputs b/c setters are private
        public Guest(int guestID, int memberID, string guestType, string firstName,
                string lastName, string phoneNumber, bool minor)
        {
            GuestID = guestID;
            MemberID = memberID;
            GuestTypeID = guestType;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Minor = minor;
            Active = true;
        }*/

		public List<string> Aliases
		{
			get
			{
				return new string[] { GuestTypeID , "Guest" }.ToList();
			}
		}

		public string Alias
		{
			get
			{
				return Email;
			}
		}
	}
}