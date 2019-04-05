using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 3/27/2019
    /// The HouseKeepingRequest Object is designed to directly carry information based on the information about WorkOrders in our Data Dictionary
    /// </summary>
    public class HouseKeepingRequest
    {
        public int HouseKeepingRequestID { get; set; }
        public int BuildingNumber { get; set; }
        public int RoomNumber { get; set; }
        public string Description { get; set; }
        public int? WorkingEmployeeID { get; set; }
        public bool Active { get; set; }

        public bool ValidateDescription()
        {
            if ((Description != null && Description.Length < 1001) || Description == null)
            {
                return true;
            }
            return false;
        }

        public bool ValidateRoomNumber()
        {
            if (RoomNumber >= 0 && RoomNumber < 9999)
            {
                return true;
            }
            return false;
        }

        public bool ValidateBuildingNumber()
        {
            if (BuildingNumber >= 0 && BuildingNumber < 9999)
            {
                return true;
            }
            return false;
        }

        public bool IsValid()
        {
            if (ValidateBuildingNumber()  && ValidateRoomNumber() && ValidateDescription())
            {
                return true;
            }
            return false;
        }




    }
}
