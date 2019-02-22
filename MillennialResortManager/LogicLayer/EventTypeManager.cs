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
    /// @Author Phillip Hansen
    /// @Created 1/24/2019
    /// 
    /// Manages Event Types through the Logic Layer
    /// </summary>
    public class EventTypeManager
    {
        public List<string> RetrieveEventTypes()
        {
            List<string> eventTypes = null;

            try
            {
                eventTypes = EventTypeAccessor.RetrieveAllEventTypes();
            }
            catch (Exception)
            {
                throw;
            }

            return eventTypes;
        }
    }
}
