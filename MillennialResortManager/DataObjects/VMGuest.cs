using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class VMGuest
    {
        public int GuestID { get; set; }
        public int MemberID { get; set; }
        public string GuestTypeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberLastName { get; set; }
        public bool Minor { get; set; }
        public bool Active { get; set; }
        public bool ReceiveTexts { get; set; }
        public string EmergencyFirstName { get; set; }
        public string EmergencyLastName { get; set; }
        public string EmergencyPhoneNumber { get; set; }
        public string EmergencyRelation { get; set; }
        public bool CheckedIn { get; set; }

    }
}
