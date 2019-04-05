using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Author: Jacob Miller
    /// Created: 3/285/19
    /// </summary>
    public class LuggageManager
    {
        IGuestAccessor guestAccessor;
        ILuggageAccessor luggageAccessor;
        public LuggageManager()
        {
            luggageAccessor = new LuggageAccessor();
            guestAccessor = new GuestAccessor();
        }
        public LuggageManager(MockLuggageAccessor mockLuggage, GuestAccessor mockGuest)
        {
            luggageAccessor = mockLuggage;
            guestAccessor = mockGuest;
        }
        public bool AddLuggage(Luggage l)
        {
            bool result = false;
            try
            {
                result = luggageAccessor.CreateLuggage(l);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public Luggage RetrieveLuggageByID(int id)
        {
            return luggageAccessor.RetrieveLuggageByID(id);
        }
        public List<Luggage> RetrieveAllLuggage()
        {
            List<Luggage> luggage = luggageAccessor.RetrieveAllLuggage();
            Guest g;
            for (int l = 0; l < luggage.Count; l++)
            {
                g = guestAccessor.SelectGuestByGuestID(luggage[l].GuestID);
                luggage[l].GuestFirstName = g.FirstName;
                luggage[l].GuestLastName = g.LastName;
            }
            return luggage;
        }
        public bool EditLuggage(Luggage oldLuggage, Luggage newLuggage)
        {
            bool result = false;
            try
            {
                result = luggageAccessor.UpdateLuggage(oldLuggage, newLuggage);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public bool DeleteLuggage(Luggage l)
        {
            return luggageAccessor.DeleteLuggage(l);
        }
        public List<LuggageStatus> RetrieveAllLuggageStatus()
        {
            return luggageAccessor.RetrieveAllLuggageStatus();
        }
    }
}
