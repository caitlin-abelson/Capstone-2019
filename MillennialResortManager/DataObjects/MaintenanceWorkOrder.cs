using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 2/21/2019
    /// The MaintenanceWorkOrder Object is designed to directly carry information about a WorkOrder based on the information about WorkOrders in our Data Dictionary
    /// </summary>
    public class MaintenanceWorkOrder
    {
        public int MaintenanceWorkOrderID { get; set; }
        public string MaintenanceTypeID { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int RequestingEmployeeID { get; set; }
        public int WorkingEmployeeID { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string MaintenanceStatusID { get; set; }
        public int ResortPropertyID { get; set; }
        public bool Complete { get; set; }

        //public bool ValidateDateCompleted()
        //{
        //    if (DateCompleted.Equals(01/01/0001))
        //    {
        //        return true;
        //    }
        //    if (DateCompleted.Equals(null))
        //    {
        //        return true;
        //    }

        //    // DateCompleted must be after today
        //    if (DateRequested.Date.Day < DateCompleted.Date.Day )
        //    {
        //        return true;
        //    }
            
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool ValidateDateRequested()
        //{
        //    if (DateRequested == null)
        //    {
        //        return false;
        //    }

        //    // DateRequested cannot be before the Resort was created 
        //    if (DateRequested.Year < 1900)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        public bool ValidateDescription()
        {
            if ((Description != null && Description.Length < 1001) || Description == null)
            {
                return true;
            }
            return false;
        }

        public bool ValidateComments()
        {
            if ((Comments != null && Comments.Length < 1001) || Comments == null)
            {
                return true;
            }
            return false;
        }

        public bool ValidateResortPropertyID()
        {
            if (ResortPropertyID >= 0)
            {
                return true;
            }
            return false;
        }

        public bool ValidateRequestingEmployeeID()
        {
            if (RequestingEmployeeID >= 0)
            {
                return true;
            }
            return false;
        }

        public bool ValidateWorkingEmployeeID()
        {
            if (WorkingEmployeeID >= 0)
            {
                return true;
            }
            return false;
        }

        public bool IsValid()
        {
            if (ValidateDescription()  && ValidateComments() && ValidateResortPropertyID() && ValidateRequestingEmployeeID()
                && ValidateWorkingEmployeeID() )
            {
                return true;
            }
            return false;
        }




    }
}
