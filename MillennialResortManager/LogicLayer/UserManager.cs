using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;     // where secure hash algorithms live
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{

    public class UserManager        // Concrete class
    {
        // need to authenticate the user
        public User AuthenticateUser(string userName, string password)
        {

            User user = null;


            // hast the password
            password = hashSHA256(password);

            // this is unsafe code...
            try
            {
                if (1 == UserAccessor.VerifyUsernameAndPassword(userName, password))     // if the user is verified I want to create a user object
                {
                    // the user is validated, so instantiate a user
                    user = UserAccessor.RetrieveUserByEmail(userName);

                    if (password == hashSHA256("newuser"))
                    {
                        user.Roles.Clear();
                        user.Roles.Add("New User");
                    }
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }

            }
            catch (Exception ex)       // this is were we would communicate with the log
            {
                throw new ApplicationException("User not validated.", ex);  // ex as the inner exception, we we are preserving the inner exception
            }

            return user;
        }

        public Employee AuthenticateEmployee(string userName, string password)
        {
            
            Employee employee = null;
            

            // hast the password
            password = hashSHA256(password);

            // this is unsafe code...
            try
            {
                if (1 == UserAccessor.VerifyUsernameAndPassword(userName, password))     // if the user is verified I want to create a user object
                {
                    // the user is validated, so instantiate a user
                    employee = UserAccessor.RetrieveEmployeeByEmail(userName);

                    if (password == hashSHA256("newuser"))
                    {
                        //user.Roles.Clear();
                        //user.Roles.Add("New User");
                    }
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }

            }
            catch (Exception ex)       // this is were we would communicate with the log
            {
                throw new ApplicationException("User not validated.", ex);  // ex as the inner exception, we we are preserving the inner exception
            }

            return employee;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/02/05
        /// 
        /// Requests a full user from the data access layer and 
        /// passses it to the presentation layer if it's found.
        /// </summary>
        public User RetrieveFullUserByEmail(string email)
        {
            User user = null;

            try
            {
                user = UserAccessor.RetrieveFullUserByEmail(email);
            }
            catch (Exception)
            {

                throw;
            }

            return user;
        }

        // method to allow a password to be changed (10/2)
        public bool UpdatePassword(string userName, string oldPassword, string newPassword)
        {
            bool result = false;

            newPassword = hashSHA256(newPassword);
            oldPassword = hashSHA256(oldPassword);

            try
            {
                result = (1 == UserAccessor.UpdatePasswordHash(userName, oldPassword, newPassword));
            }
            catch (Exception)
            {

                throw;
            }




            return result;
        }

        public void RefreshRoles(User user, string email)
        {
            try
            {
                // get the roles
                var roles = UserAccessor.RetrieveRolesByEmail(email);

                user.Roles.Clear();
                foreach (var role in roles)
                {
                    user.Roles.Add(role);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        // need to hash the password
        private string hashSHA256(string source)        // source is the password passed in 
        {
            string result = "";

            // we need a byte array, hash algorthms do not work on strings or characters
            byte[] data;

            // use a .NET hash provider
            using (SHA256 sha256hash = SHA256.Create())      //using is a complier directive, do not confuse with using statements above which is a C# keyword 
            {
                // hash the input
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // now, we just need to build the result string with a String Builder
            var s = new StringBuilder();

            // loop through the bytes creating hex digits
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));       //x2 - formating string will take byte char and give the hexidecimal string 
            }

            // conver String Builder to a string
            result = s.ToString();

            return result;
        }

        
    }
}
