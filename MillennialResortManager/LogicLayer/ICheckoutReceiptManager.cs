using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ICheckoutReceiptManager
    {
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
        bool generateMemberTabReceipt(Reservation reservation, Member member, List<OfferingVM> allOfferings,
            MemberTab tab, string filePath, List<GuestRoomAssignmentVM> allGuests);
        
    }
}