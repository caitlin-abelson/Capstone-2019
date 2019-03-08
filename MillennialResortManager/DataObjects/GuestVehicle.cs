using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Richard Carroll
    /// Created Date: 2/28/19
    /// 
    /// This Class holds the data necessary for inserting into the 
    /// Data Access Layer.
    /// </summary>
    public class GuestVehicle
    {
        public int GuestID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
        public string Color { get; set; }
        public string ParkingLocation { get; set; }

        public bool validateMake()
        {
            bool result = true;

            if (Make == null || Make.Length > 30 || Make == "")
            {
                result = false;
            }

            return result;
        }
        public bool validateModel()
        {
            bool result = true;

            if (Model == null || Model.Length > 30 || Model == "")
            {
                result = false;
            }

            return result;

        }
        public bool validatePlateNumber()
        {
            bool result = true;

            if (PlateNumber == null || PlateNumber.Length > 10 || PlateNumber == "")
            {
                result = false;
            }

            return result;
        }
        public bool validateColor()
        {
            bool result = true;

            if (Color.Length > 30)
            {
                result = false;
            }

            return result;
        }
        public bool validateParkingLocation()
        {
            bool result = true;

            if (ParkingLocation.Length > 50)
            {
                result = false;
            }

            return result;
        }
        public bool isValid()
        {
            bool result = true;

            if (!validateColor() || !validateMake() || !validateModel() || !validateParkingLocation() || 
                !validatePlateNumber())
            {
                result = false;
            }

            return result;
        }
    }
}
