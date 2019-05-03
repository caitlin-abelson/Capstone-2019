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
    /// Created On: 2019-05-01
    /// Mock Accessor for Testing Purposes
    /// </summary>
    public class CheckoutReceiptAccessorMock : ICheckoutReceiptAccessor
    {
        public bool generateMemberTabReceipt(Reservation reservation, Member member, List<OfferingVM> allOfferings, MemberTab tab, string filePath, List<GuestRoomAssignmentVM> allGuests)
        {
            bool result = false;
            if (reservation == null || member == null || allOfferings == null || tab == null || filePath == null || filePath == "" || allGuests == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}
