using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Phillip Hansen" created="2019/04/03">
	/// </summary>
	public class EventSponsorManager
    {

        private EventSponsorAccessor _eventSponsorAccessor;

		/// <summary author="Phillip Hansen" created="2019/04/03">
		/// Constructor for calling non-static methods
		/// </summary>
		public EventSponsorManager()
        {
            _eventSponsorAccessor = new EventSponsorAccessor();
        }

		/// <summary author="Phillip Hansen" created="2019/04/03">
		/// Method for creating an event calling to the accessor for events
		/// </summary>
		/// <param name="newEvent"></param> creates a new Event object called newEvent
		public void CreateEventSponsor(int eventID, int sponsorID)
        {

            try
            {
                _eventSponsorAccessor.insertEventSponsor(eventID, sponsorID);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Phillip Hansen" created="2019/04/03">
		/// Method for retrieving all event sponsors as a list
		/// </summary>
		/// <returns></returns>
		public List<EventSponsor> RetrieveAllEventSponsors()
        {
            List<EventSponsor> eventSponsors;

            try
            {
                eventSponsors = _eventSponsorAccessor.selectAllEventSponsors();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return eventSponsors;
        }

		/// <summary author="Phillip Hansen" created="2019/04/03">
		/// Deletes the event by taking the object as a whole, and passes only the ID
		/// </summary>
		/// <param name="purgeEvent"></param> the event to be purged
		public void DeleteEventSponsor(EventSponsor purgeEventSpons)
        {

            try
            {
                _eventSponsorAccessor.deleteEventByID(purgeEventSpons.EventID, purgeEventSpons.SponsorID);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}
    }
}
