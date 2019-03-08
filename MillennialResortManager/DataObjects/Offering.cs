/// <summary>
/// Jared Greenfield
/// Created: 2019/01/24
/// 
/// Represents an Offering object. This is anything that people at the hotel can purchase.
/// These Offerings will then be added to the bill.
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Offering
    {
        public Offering(string offeringTypeID, int employeeID, string description, decimal price)
        {
            OfferingTypeID = offeringTypeID;
            EmployeeID = employeeID;
            Description = description;
            Price = price;
        }

        public Offering(int offeringID, string offeringTypeID, int employeeID, string description, decimal price, bool active)
        {
            OfferingID = offeringID;
            OfferingTypeID = offeringTypeID;
            EmployeeID = employeeID;
            Description = description;
            Price = price;
            Active = active;
        }

        public int OfferingID { get; set; }
        public string OfferingTypeID { get; set; }
        public int EmployeeID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }

        public bool ValidateOfferingTypeID()
        {
            bool isValid = true;
            if (OfferingTypeID == null || OfferingTypeID == "" || OfferingTypeID.Length > 15)
            {
                isValid = false;
            }

            return isValid;
            
        }
        public bool ValidateDescription()
        {
            bool isValid = true;
            if (Description.Length > 1000)
            {
                isValid = false;
            }

            return isValid;
        }

        public bool ValidatePrice()
        {
            bool isValid = true;
            if (Price < 0)
            {
                isValid = false;
            }

            return isValid;
        }

        public bool IsValid()
        {
            bool isValid = false;
            if (ValidateDescription() && ValidateOfferingTypeID() && ValidatePrice())
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
