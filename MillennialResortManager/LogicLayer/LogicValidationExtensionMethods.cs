/// <summary>
/// Dani Russo
/// Created: 2019/02/20
/// 
/// A class of extension methods to validate BuildingManager class.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public static class LogicValidationExtensionMethods
    {
        /// <summary>
        /// Dani Russo
        /// Created: 2019/02/20
        /// 
        /// If a string value is a valid length or not null
        /// Max length 50 characters.
        /// </summary>
        public static void ValidateBuildingID(this string buildingID)
        {

            if (buildingID == null)
            {
                throw new ArgumentNullException("Building ID needs a value.");
            }
            if (buildingID.Length > 50)
            {
                throw new ArgumentException("Limit Building ID to 50 characters.");
            }
        }

        /// <summary>
        /// Dani Russo
        /// Created: 2019/02/20
        /// 
        /// If a string value is a valid length or null
        /// Max length 150 characters.
        /// </summary>
        public static void ValidateBuildingName(this string name)
        {
            if (name != null)
            {
                if (name.Length > 150)
                {
                    throw new ArgumentException("Limit name to 150 characters.");
                }
            }
        }

        /// <summary>
        /// Dani Russo
        /// Created: 2019/02/20
        /// 
        /// If a string value is a valid length or null
        /// Max length 150 characters.
        /// </summary>
        public static void ValidateBuildingAddress(string address)
        {
            if (address != null)
            {
                if (address.Length > 150)
                {
                    throw new ArgumentException("Limit address to 150 characters.");
                }
            }
        }

        /// <summary>
        /// Dani Russo
        /// Created: 2019/02/20
        /// 
        /// If a string value is a valid length or null
        /// Max length 1000 characters.
        public static void ValidateBuildngDescription(string description)
        {
            if (description != null)
            {
                if (description.Length > 1000)
                {
                    throw new ArgumentException("Limit description to 150 characters.");
                }
            }
        }

        /// <summary>
        /// Dani Russo
        /// Created: 2019/02/20
        /// 
        /// If a string value is a valid length or not null
        /// Max length 25 characters.
        /// </summary>
        public static void ValidateBuildingStatusID(string statusID)
        {
            if (statusID == null)
            {
                throw new ArgumentNullException("Status ID needs a value.");
            }
            if (statusID.Length > 25)
            {
                throw new ArgumentException("Limit Status ID to 25 characters.");
            }
        }

        /// <summary>
        /// Dani Russo
        /// Created: 2019/02/20
        /// 
        /// Valdates that both building IDs match
        /// </summary>
        internal static void ValdateMatchingIDs(string buildingID, string otherBuildingID)
        {
            if (!buildingID.Equals(otherBuildingID))
            {
                throw new ArgumentException("Cannot change Building ID.");
            }
        }
    }
}
