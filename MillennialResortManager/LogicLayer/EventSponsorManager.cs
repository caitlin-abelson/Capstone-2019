using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// @Author: Phillip Hansen
    /// @Created: 4/3/2019
    /// 
    /// 
    /// </summary>
    public class EventSponsorManager
    {

        private EventSponsorAccessor _eventSponsorAccessor;

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Constructor for calling non-static methods
        /// </summary>
        public EventSponsorManager()
        {
            _eventSponsorAccessor = new EventSponsorAccessor();
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Method for creating an event calling to the accessor for events
        /// </summary>
        /// <param name="newEvent"></param> creates a new Event object called newEvent
        public void CreateEventSponsor(int eventID, int sponsorID)
        {

            try
            {
                _eventSponsorAccessor.insertEventSponsor(eventID, sponsorID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Method for retrieving all event sponsors as a list
        /// </summary>
        /// <returns></returns>
        public List<EventSponsor> RetrieveAllEvents()
        {
            List<EventSponsor> eventSponsors;

            try
            {
                eventSponsors = _eventSponsorAccessor.selectAllEventSponsors();
            }
            catch (Exception)
            {
                throw;
            }

            return eventSponsors;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Deletes the event by taking the object as a whole, and passes only the ID
        /// </summary>
        /// <param name="purgeEvent"></param> the event to be purged
        public void DeleteEventSponsor(EventSponsor purgeEventSpons)
        {

            try
            {
                _eventSponsorAccessor.deleteEventByID(purgeEventSpons.EventID, purgeEventSpons.SponsorID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
