using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicLayer
{
   public static class MemberValidator
    {
        public static readonly int MEMBER_FIRST_NAME_MAX_LENGTH = 50;
        public static readonly int MEMBER_LAST_NAME_MAX_LENGTH = 100;
        public static readonly int MEMBER_PHONE_NUMBER_MAX_LENGTH = 11;
        public static readonly int MEMBER_EMAIL_MAX_LENGTH = 250;
        public static readonly int MEMBER_PASSWORD_MAX_LENGTH = 100;


        /// <summary>
        /// Author: Ramesh Adhikari
        /// Date: 2019/2/15
        /// 
        /// // Validate First Name to check if its null, empty string and check length
        /// </summary>
        /// <param name="member"></param>
        public static void ValidateFirstName(this Member member)
        {
            if(member.FirstName == null)
            {
                throw new ArgumentNullException("Member First Name Cannot be null");
            }
            else if(member.FirstName == "")
            {
                throw new ArgumentException("Member First Name is required");
            }
            else if (member.FirstName.Length > MEMBER_FIRST_NAME_MAX_LENGTH)
            {
                throw new ArgumentException("Member Name Cannot be over " + MEMBER_FIRST_NAME_MAX_LENGTH);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        public static void ValidatePassword(this Member member)
        {
            if (member.Password == null)
            {
                throw new ArgumentNullException("Member Password Cannot be null");
            }
            else if (member.Password == "")

            {
                throw new ArgumentException("Member password is required");
            }
            else if(member.Password.Length > MEMBER_PASSWORD_MAX_LENGTH)
            {
                throw new ArgumentException("Member Password cannot be over" + MEMBER_PASSWORD_MAX_LENGTH);
            }
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Date: 2019/2/15
        /// // This will validate the last name and make sure its not null, empty string and 
        /// first name length
        /// 
        /// </summary>
        /// <param name="member"></param>
        public static void ValidateLastName(this Member member)
        {
            if (member.LastName == null)
            {
                throw new ArgumentNullException("Member Last Name Cannot be null");
            }
            else if (member.LastName == "")
            {
                throw new ArgumentException("Member Last Name is required");
            }
            else if (member.LastName.Length > MEMBER_LAST_NAME_MAX_LENGTH)
            {
                throw new ArgumentException("Member Last Name Cannot be over " + MEMBER_LAST_NAME_MAX_LENGTH);
            }

        }
        // Validate phone number
        public static void ValidatePhoneNumber(this Member member)
        {
            if (member.PhoneNumber == null)
            {
                throw new ArgumentNullException("Member Phone Number Cannot be null");
            }
            else if (member.PhoneNumber == "")
            {
                throw new ArgumentException("Member PhoneNumber is required");
            }
            else if (member.PhoneNumber.Length > MEMBER_PHONE_NUMBER_MAX_LENGTH)
            {
                throw new ArgumentException("Member Phone Number Cannot be over " + MEMBER_PHONE_NUMBER_MAX_LENGTH);
            }
            else if (!Regex.IsMatch(member.PhoneNumber, @"^\(?\d{3}\)?[\s\-]?\d{3}\-?\d{4}$"))
            {
                throw new ArgumentException("Invalid Phone Number");
            }

        }
        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// Validate email and make sure its not null, empty, and check the length
        /// </summary>
       
        public static void ValidateEmail(this Member member)
        {
            if (member.Email == null)
            {
                throw new ArgumentNullException("Member Email Cannot be null");
            }
            else if (member.Email == "")
            {
                throw new ArgumentException("Member Email is required");
            }
           
            else if (member.Email.Length > MEMBER_EMAIL_MAX_LENGTH)
            {
                throw new ArgumentException("Member Phone Number Cannot be over " + MEMBER_PHONE_NUMBER_MAX_LENGTH);
           }
}

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// </summary>
    
        public static void Validate(this Member member)
        {
            member.ValidateFirstName();
            member.ValidateLastName();
            member.ValidatePhoneNumber();
            member.ValidateEmail();
            member.ValidatePassword();
        }

    }
}
