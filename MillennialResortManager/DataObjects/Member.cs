using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
	/// <summary>
	/// Author: Matt LaMarche
	/// Created : 1/24/2019
	/// The Member Object is designed to directly carry information about a Member based on the information about Members in our Data Dictionary
	/// 
	/// <remarks>
	/// Name: Ramesh Adhikari
	/// Update Date: 03/08/2019
	/// Description: Added the password property
	/// </remarks>
	/// <remarks>
	/// Austin Delaney
	/// Date: 2019/01/06
	/// 
	/// Implemented ISender and IMessagable interface
	/// </remarks>
	/// </summary>
	public class Member : ISender , IMessagable
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

		public List<string> Aliases
		{
			get
			{
				return new string[] { "Member" }.ToList();
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