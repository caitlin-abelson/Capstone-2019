/// <summary>
/// Dani Russo
/// Created: 2019/02/20
/// 
/// A class of extension methods to validate BuildingManager class.
/// </summary>
/// <remarks>
/// Austin Berquam
/// Updated: 2019/02/23
/// 
/// Adjusted the methods to work with look-up tables
/// 
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public static class ValidationExtensionMethods
    {
        /// <summary>
        /// Dani Russo
        /// Created: 2019/02/20
        /// 
        /// If a string value is a valid length or not null
        /// Max length 50 characters.
        /// </summary>
        public static void ValidateID(this string buildingID)
        {

            if (buildingID == null)
            {
                throw new ArgumentNullException("ID needs a value.");
            }
            if (buildingID.Length > 50)
            {
                throw new ArgumentException("Limit ID to 50 characters.");
            }
        }
        /// <summary>
        /// Dani Russo
        /// Created: 2019/02/20
        /// 
        /// If a string value is a valid length or null
        /// Max length 1000 characters.
        public static void ValidateDescription(string description)
        {
            if (description != null)
            {
                if (description.Length > 1000)
                {
                    throw new ArgumentException("Limit description to 150 characters.");
                }
            }
        }
    }
}
