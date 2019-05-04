using DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created On: 2019-05-01
    /// Accessor for storing a receipt in HTML
    /// </summary>
    public class CheckoutReceiptAccessor : ICheckoutReceiptAccessor
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
        public bool generateMemberTabReceipt(Reservation reservation, Member member, List<OfferingVM> allOfferings, MemberTab tab, string filePath, List<GuestRoomAssignmentVM> allGuests)
        {
            bool success = false;
            decimal totalMember = 0; //The member's Total
            List<decimal> guestTotals = new List<decimal>(); // List of all guest totals
            // Start Creating the file
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(filePath);

                //Header Information
                writer.WriteLine("<!DOCTYPE html>");
                writer.WriteLine("<html>");
                writer.WriteLine("<head>");
                writer.WriteLine("<meta charset='utf - 8'>");
                writer.WriteLine("<meta http - equiv = 'X-UA-Compatible' content = 'IE=edge'>");
                writer.WriteLine("<title>" + reservation.DepartureDate.ToShortDateString().Replace("/", "-") + member.Email + "</title>");
                writer.WriteLine("<link rel='stylesheet' type='text/css' href='css/main.css'>");
                writer.WriteLine("</head>");
                // Header End
                //Body Start
                writer.WriteLine("<body>");
                writer.WriteLine("<header class='middle' id='summaryTitle'>Millenial Resorts Summary of Charges</header>");

                // Member Information
                writer.WriteLine("<h1 class='middle'>Member Information</h1>");
                writer.WriteLine("<div class='middle memberInfo'>");
                writer.WriteLine("<div class='left'>");
                writer.WriteLine("<h2><bold>Member Name:</bold> " + member.FirstName + " " + member.LastName + "</h2>");
                writer.WriteLine("<h2><bold>Email:</bold> " + member.Email + "</h2>");
                writer.WriteLine("</div>");
                writer.WriteLine("<div class='right'>");
                writer.WriteLine("<h2><bold>Phone Number:</bold> " + member.PhoneNumber.ToFormattedPhoneNumber() + "</h2>");
                writer.WriteLine("<h2><bold>Reservation Date:</bold> " + reservation.ArrivalDate.ToShortDateString() + " - " + reservation.DepartureDate.ToShortDateString() + "</h2>");
                writer.WriteLine("</div>");
                writer.WriteLine("</div>");

                // Member Purchases
                writer.WriteLine("<section>");
                writer.WriteLine("<h1 class='middle'>Member Purchases</h1>");
                writer.WriteLine("<table>");
                writer.WriteLine("<tr>");
                writer.WriteLine("<th class='offeringPurchased tableHeader'>Offering Purchased</th>");
                writer.WriteLine("<th class='datePurchased tableHeader'>Date Purchased</th>");
                writer.WriteLine("<th class='price tableHeader'>Price</th>");
                writer.WriteLine("<th class='quantity tableHeader'>Quantity</th>");
                writer.WriteLine("<th class='totalColumn tableHeader'>Total</th>");
                writer.WriteLine("</tr>");
                List<MemberTabLine> memberLine = new List<MemberTabLine>();
                foreach (MemberTabLine item in tab.MemberTabLines)
                {
                    // Only add Member's items (this means no guestID)
                    if (item.GuestID == null)
                    {
                        memberLine.Add(item);
                    }
                }
                memberLine = memberLine.OrderByDescending(x => x.DatePurchased).ToList(); // Sort in chronological order

                //Table Items
                foreach (MemberTabLine item in memberLine)
                {
                    writer.WriteLine("<tr>");
                    writer.WriteLine("<th>" + allOfferings.Find(x => x.OfferingID == item.OfferingID).OfferingName + "</th>");
                    writer.WriteLine("<th>" + item.DatePurchased.ToUniversalTime() + "</th>");
                    writer.WriteLine("<th>" + item.Price.ToString("c") + "</th>");
                    writer.WriteLine("<th>" + item.Quantity + "</th>");
                    writer.WriteLine("<th>" + (item.Price * item.Quantity).ToString("c") + "</th>");
                    writer.WriteLine("</tr>");
                    totalMember += item.Price * item.Quantity;
                }
                writer.WriteLine("</table>");
                writer.WriteLine("<h2 class='total'> Member Total: " + totalMember.ToString("c") + "</h2>");
                writer.WriteLine("</section>");

                // Guest Purchases
                writer.WriteLine("<section>");
                writer.WriteLine("<h1 class='middle'>Guest Purchases</h1>");
                foreach (GuestRoomAssignmentVM guest in allGuests)
                {
                    decimal guestTotal = 0;
                    // Rewrite a new section for each Guest involved
                    writer.WriteLine("<div class='guestTab'>");
                    writer.WriteLine("<h3 class='guestName'> Guest: " + guest.FirstName + " " + guest.LastName + "</h3>");
                    writer.WriteLine("<table>");
                    writer.WriteLine("<tr>");
                    writer.WriteLine("<th class='offeringPurchased tableHeader'>Offering Purchased</th>");
                    writer.WriteLine("<th class='datePurchased tableHeader'>Date Purchased</th>");
                    writer.WriteLine("<th class='price tableHeader'>Price</th>");
                    writer.WriteLine("<th class='quantity tableHeader'>Quantity</th>");
                    writer.WriteLine("<th class='totalColumn tableHeader'>Total</th>");
                    writer.WriteLine("</tr>");
                    List<MemberTabLine> guestLine = new List<MemberTabLine>();
                    foreach (MemberTabLine item in tab.MemberTabLines)
                    {
                        // Only add Member's items (this means no guestID)
                        if (item.GuestID == guest.GuestID)
                        {
                            guestLine.Add(item);
                        }
                    }
                    guestLine = guestLine.OrderByDescending(x => x.DatePurchased).ToList(); // Sort in chronological order

                    //Table Items
                    foreach (MemberTabLine item in guestLine)
                    {
                        writer.WriteLine("<tr>");
                        writer.WriteLine("<th>" + allOfferings.Find(x => x.OfferingID == item.OfferingID).OfferingName + "</th>");
                        writer.WriteLine("<th>" + item.DatePurchased.ToUniversalTime() + "</th>");
                        writer.WriteLine("<th>" + item.Price.ToString("c") + "</th>");
                        writer.WriteLine("<th>" + item.Quantity + "</th>");
                        writer.WriteLine("<th>" + (item.Price * item.Quantity).ToString("c") + "</th>");
                        writer.WriteLine("</tr>");
                        guestTotal += item.Price * item.Quantity;
                    }
                    writer.WriteLine("</table>");
                    writer.WriteLine("<h2 class='total'> Guest Total: " + guestTotal.ToString("c") + "</h2>");
                    writer.WriteLine("</div>");
                    guestTotals.Add(guestTotal);
                }
                decimal grandTotal = totalMember;
                foreach (decimal price in guestTotals)
                {
                    grandTotal += price;
                }
                writer.WriteLine("<h2 class='total'> Grand Total: " + grandTotal.ToString("c") + "</h2>");
                writer.WriteLine("</section>");

                // Footer Start
                writer.WriteLine("<footer>");
                writer.WriteLine("<p>&copy; Millenial Resorts " + DateTime.Now.Year + "</p>");
                writer.WriteLine("</footer>");
                //Footer End
                writer.WriteLine("</body>");
                //Body End

                writer.WriteLine("</html>");
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }
            finally
            {
                writer.Close();
            }
            return success;
        }
    }
}
