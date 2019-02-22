using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Richard Carroll
    /// Created: 2019/01/28
    /// 
    /// This class contains all the data needed for Database 
    /// Interactions for Orders
    /// </summary>
    ///
    /// <remarks>
    public class InternalOrder
    {
        public int InternalOrderID { get; set; }
        public int EmployeeID { get; set; }
        public string DepartmentID { get; set; }
        public string Description { get; set; }
        public bool OrderComplete { get; set; }
        public DateTime DateOrdered { get; set; }
        public bool ValidateDepartmentID()
        {
            bool isValid = true;

            if (DepartmentID == "" || DepartmentID.Length > 50)
            {
                isValid = false;
            }

            return isValid;
        }
        public bool ValidateDescription()
        {
            bool isValid = true;

            if (Description == null || Description == "" || Description.Length > 1000)
            {
                isValid = false;
            }

            return isValid;
        }
        public bool isValid()
        {
            bool isValid = true;

            if (!ValidateDepartmentID() || !ValidateDescription())
            {
                isValid = false;
            }

            return isValid;
        }

    }
}
