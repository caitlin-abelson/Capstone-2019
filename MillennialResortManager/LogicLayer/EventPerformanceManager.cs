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
	/// <summary author="Phillip Hansen" created="2019/04/10">
	/// </summary>
	public class EventPerformanceManager
    {
        private EventPerformanceAccessor _eventPerformanceAccessor;

        public EventPerformanceManager()
        {
            _eventPerformanceAccessor = new EventPerformanceAccessor();
        }

		/// <summary author="Phillip Hansen" created="2019/04/10">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Phillip Hansen" created="2019/04/10">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return eventPerformances;
        }
    }
}
