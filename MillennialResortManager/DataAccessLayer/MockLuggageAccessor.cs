using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class MockLuggageAccessor : ILuggageAccessor
    {
        private List<Luggage> _luggage;

        public MockLuggageAccessor()
        {
            _luggage = new List<Luggage>();
            _luggage.Add(new Luggage() { LuggageID = 100000, GuestID = 100000, Status = "In Room"});
            _luggage.Add(new Luggage() { LuggageID = 100001, GuestID = 100001, Status = "In Lobby"});
            _luggage.Add(new Luggage() { LuggageID = 100002, GuestID = 100002, Status = "In Transit"});
        }
        public bool CreateLuggage(Luggage l)
        {
            _luggage.Add(l);
            return true;
        }
        public bool DeleteLuggage(Luggage l)
        {
            _luggage.Remove(l);
            return true;
        }
        public List<Luggage> RetrieveAllLuggage()
        {
            return _luggage;
        }
        public Luggage RetrieveLuggageByID(int id)
        {
            Luggage l = null;
            l = _luggage.Find(x => x.LuggageID == id);
            if (l == null)
            {
                throw new ApplicationException("Couldn't find any Luggage with matching ID.");
            }
            return l;
        }
        public bool UpdateLuggage(Luggage oldLuggage, Luggage newLuggage)
        {
            bool found = false;
            for (int i = 0; i < _luggage.Capacity; i++)
            {
                if (_luggage[i].LuggageID == oldLuggage.LuggageID)
                {
                    _luggage[i].GuestID = newLuggage.GuestID;
                    _luggage[i].Status = newLuggage.Status;
                }
            }
            if (found)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<LuggageStatus> RetrieveAllLuggageStatus()
        {
            return new List<LuggageStatus>() { 
                new LuggageStatus(){ LuggageStatusID = "In Lobby"},
                new LuggageStatus(){ LuggageStatusID = "In Room"},
                new LuggageStatus(){ LuggageStatusID = "In Transint"}};
        }
    }
}
