using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James Heim
    /// Created 2019-04-25
    /// 
    /// A View Model for showing a TabLine in a DataGrid.
    /// </summary>
    public class VMTabLine
    {
        /// <summary>
        /// Human friendly name of the Offering.
        /// </summary>
        public string OfferingDescription { get; set; }

        /// <summary>
        /// Backing Store for the Quantity.
        /// </summary>
        private int _quantity;

        /// <summary>
        /// The amount of the offering.
        /// Change the Total each time the Quantity is updated. 
        /// </summary>
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;

                Total = (_price * _quantity).ToString("C");
            }
        }

        /// <summary>
        /// Backing Store for the Price.
        /// </summary>
        private decimal _price;

        /// <summary>
        /// Price of the individual unit formatted as a currency.
        /// Change the Total each time the Price is updated.
        /// </summary>
        public string FormattedPrice
        {
            get
            {
                return string.Format("{0:C}", _price);
            }
            set
            {
                try
                {
                    _price = decimal.Parse(value, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

                Total = (_price * _quantity).ToString("C");
            }
        }

        /// <summary>
        /// Price * Quantity
        /// </summary>
        public string Total { get; private set; }

        /// <summary>
        /// DateTime stamp.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Guest's First & Last names concatenated.
        /// If the Guest is null, it should be populated with the Member's name.
        /// </summary>
        public string GuestName { get; set; }

        /// <summary>
        /// Concatenated First and Last name of the Employee who
        /// added the Offering to the bill.
        /// If it was added automatically (without an employee), it should 
        /// be set as an empty string.
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// OfferingID so a selected item can reference the
        /// particular Offering.
        /// </summary>
        public int OfferingID { get; set; }

        /// <summary>
        /// OfferingType so a selected item can reference the
        /// particular Offering.
        /// </summary>
        public string OfferingType { get; set; }

        /// <summary>
        /// Nullable GuestID so a selected item can reference the
        /// particular Guest if there is one.
        /// </summary>
        public int? GuestID { get; set; }

        /// <summary>
        /// Nullable EmployeeID so a selected item can reference the
        /// particular Employee if there is one.
        /// </summary>
        public int? EmployeeID { get; set; }
    }
}
