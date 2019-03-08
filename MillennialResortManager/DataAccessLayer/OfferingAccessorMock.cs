using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created : 02/20/2019
    /// This is a mock Data accessor which implements the IItemAccessor interface. It is used for testing purposes only
    /// </summary>
    public class OfferingAccessorMock : IOfferingAccessor
    {
        private List<Offering> _offerings;

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// This constructor sets up all of our dummy data we will be using
        /// </summary>
        public OfferingAccessorMock()
        {
            _offerings = new List<Offering>();
            _offerings.Add(new Offering(100000, "Item", 100000, "Big Burger", (Decimal)12.99, true));
            _offerings.Add(new Offering(100001, "Item", 100000, "Big Hot Dog", (Decimal)11.99, true));
            _offerings.Add(new Offering(100002, "Item", 100000, "Big Pool Noodle", (Decimal)10.99, true));
            _offerings.Add(new Offering(100003, "Item", 100000, "Taco Grande Supreme 100 Pound Edition", (Decimal)200.99, true));

        }
        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// This will create an Offering using the data provided in the offering object.
        /// </summary>
        /// <param name="offering">The Offering we want to add to our mock system.</param>
        /// <returns>The ID of the Offering</returns>
        public int InsertOffering(Offering offering)
        {
            _offerings.Add(offering);
            return offering.OfferingID;
        }
        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// This will return the Offering with the specified ID.
        /// </summary>
        /// <param name="offeringID">The Id of the Offering we want select.</param>
        /// <returns>Offering object</returns>
        public Offering SelectOfferingByID(int offeringID)
        {
            Offering offering = null;
            offering = _offerings.Find(x => x.OfferingID == offeringID);
            return offering;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// This will update the Offering with the new Offering.
        /// </summary>
        /// <param name="oldOffering">The old Offering.</param>
        /// <param name="newOffering">The updated Offering.</param>
        /// <returns>1 if successful, 0 otherwise</returns>
        public int UpdateOffering(Offering oldOffering, Offering newOffering)
        {
            int rowsAffected = 0;
            foreach (var offering in _offerings)
            {
                if (offering.OfferingID == oldOffering.OfferingID &&
                    offering.OfferingTypeID == oldOffering.OfferingTypeID &&
                    offering.Active == oldOffering.Active &&
                    offering.Description == oldOffering.Description &&
                    offering.EmployeeID == oldOffering.EmployeeID &&
                    offering.Price == oldOffering.Price &&
                    oldOffering.OfferingID == newOffering.OfferingID)
                {
                    offering.Active = newOffering.Active;
                    offering.Description = newOffering.Description;
                    offering.EmployeeID = newOffering.EmployeeID;
                    offering.OfferingTypeID = newOffering.OfferingTypeID;
                    offering.Price = newOffering.Price;
                    rowsAffected = 1;
                    break;
                }

            }
            return rowsAffected;
        }
    }
}
