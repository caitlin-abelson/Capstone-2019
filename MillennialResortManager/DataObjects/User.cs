using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public List<string> Roles { get; private set; }

        // Need constructor with all inputs b/c setters are private
        public User(int userID, string firstName,
                string lastName, List<string> roles)
        {
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
            Roles = roles;
        }
    }
}
