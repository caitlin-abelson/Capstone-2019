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
using System.ComponentModel;
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

		[DisplayName("Guest Type")]
		public string GuestTypeID { get; set; }

		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[DisplayName("Last Name")]
		public string LastName { get; set; }

		[DisplayName("Phone Number")]
		public string PhoneNumber { get; set; }

		public string Email { get; set; }
		public bool Minor { get; set; }
		public bool Active { get; set; }

		[DisplayName("Receive Texts?")]
		public bool ReceiveTexts { get; set; }

		[DisplayName("First Name")]
		public string EmergencyFirstName { get; set; }

		[DisplayName("Last Name")]
		public string EmergencyLastName { get; set; }

		[DisplayName("Phone Number")]
		public string EmergencyPhoneNumber { get; set; }

		[DisplayName("Relation")]
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

		public Guest(int guestId, int memberID, string fName,
				string lName, string phoneNumber, string mail, bool texts, string emergencyFName,
				string emergencyLName, string emergencyPhone, string emergencyRelation)
		{
			GuestID = guestId;
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
		}

		public List<string> Aliases
		{
			get
			{
				return new string[] { GuestTypeID, "Guest" }.ToList();
			}
		}

		public string Alias
		{
			get
			{
				return Email;
			}
		}

		public Guest()
		{
			CheckedIn = false;
		}
	}
}