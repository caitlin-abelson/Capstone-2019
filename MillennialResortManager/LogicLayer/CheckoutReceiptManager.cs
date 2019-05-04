using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created On: 2019-05-01
    /// Manager for printing checkout receipts
    /// </summary>
    public class CheckoutReceiptManager : ICheckoutReceiptManager
    {
        private ICheckoutReceiptAccessor _checkoutReceiptAccessor;

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-05-01
        /// Normal Constructor for SQL operations
        /// </summary>
        public CheckoutReceiptManager()
        {
            _checkoutReceiptAccessor = new CheckoutReceiptAccessor();
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-05-01
        /// Mock constructor for test purposes.
        /// </summary>
        public CheckoutReceiptManager(ICheckoutReceiptAccessor mock)
        {
            _checkoutReceiptAccessor = mock;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-05-01
        /// Creates a receipt for the user and stores it as HTML
        /// <paramref name="allGuests">All guests in this reservation</paramref>
        /// <paramref name="allOfferings">All Offerings</paramref>
        /// <paramref name="filePath">Path to file being stored in</paramref>
        /// <paramref name="member">Member who owns the reservation</paramref>
        /// <paramref name="reservation">The reservation being checked out</paramref>
        /// <paramref name="tab">The tab of purchases</paramref>
        /// </summary>
        /// <returns>True if succeeded, False if failed</returns>
        public bool generateMemberTabReceipt(Reservation reservation, Member member, List<OfferingVM> allOfferings, MemberTab tab, string filePath, List<GuestRoomAssignmentVM> allGuests)
        {
            bool success = false;
            try
            {
                if (reservation == null || member == null || allOfferings == null || tab == null || filePath == null || filePath == "" || allGuests == null)
                {
                    throw new ArgumentException("Data provided was null, please provide all data needed.");
                }
                else
                {
                    success = _checkoutReceiptAccessor.generateMemberTabReceipt(reservation, member, allOfferings, tab, filePath, allGuests);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return success;

        }
    }
}
