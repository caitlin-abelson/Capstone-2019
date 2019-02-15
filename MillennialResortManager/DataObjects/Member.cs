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
    /// </summary>
    public class Member
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
