using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class CompleteSpecialOrder
    {
        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        /// Object of the table SpecialOrder
        /// </summary>
        public int SpecialOrderID { get; set; }
        public int EmployeeID { get; set; }
        public string Description { get; set; }
        public bool OrderComplete { get; set; }
        public DateTime DateOrdered { get; set; }
        public int SupplierID { get; set; }
       

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/20
        /// 
        /// Validates the description entered by user.
        ///
        /// </summary>
        public bool ValidateDescription()
        {
            bool isValid = true;

            if (Description == null || Description == "" || Description.Length > 1000)
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/03/08
        /// 
        /// Valid Date
        ///
        /// </summary>
        public bool ValidateDate()
        {
            bool isValid = true;

            if (DateOrdered == null || DateOrdered == DateTime.Now.AddDays(-1))
            {
                isValid = false;
            }

            return isValid;
        }



        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/20
        /// 
        /// Valid method, that returns if all input in this object is valid
        ///
        /// </summary>
        public bool isValid()
        {
            bool isValid = true;

            if (!ValidateDescription() || !ValidateDate())
            {
                isValid = false;
            }

            return isValid;
        }
    }
}

