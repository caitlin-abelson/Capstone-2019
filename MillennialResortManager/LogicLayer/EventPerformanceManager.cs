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
    /// @Created 4/10/2019
    /// </summary>
    public class EventPerformanceManager
    {
        private EventPerformanceAccessor _eventPerformanceAccessor;

        public EventPerformanceManager()
        {
            _eventPerformanceAccessor = new EventPerformanceAccessor();
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Method for calling the insert method in the accessor
        /// </summary>
        /// <param name="eventID"></param> The unique EventID
        /// <param name="performanceID"></param> The unique PerformanceID
        public void CreateEventSponsor(int eventID, int performanceID)
        {
            try
            {
                _eventPerformanceAccessor.insertEventPerformance(eventID, performanceID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Method for calling the selectAll() accessor method
        /// </summary>
        /// <returns></returns>
        public List<EventPerformance> RetrieveAllEventPerformances()
        {
            List<EventPerformance> eventPerformances;

            try
            {
                eventPerformances = _eventPerformanceAccessor.selectAllEventPerformances();
            }
            catch (Exception)
            {
                throw;
            }

            return eventPerformances;
        }
    }
}
