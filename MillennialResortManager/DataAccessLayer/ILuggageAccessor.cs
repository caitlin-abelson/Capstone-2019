using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Jacob Miller
    /// Created: 3/28/19
    /// </summary>
    public interface ILuggageAccessor
    {
        bool CreateLuggage(Luggage l);
        bool DeleteLuggage(Luggage l);
        List<Luggage> RetrieveAllLuggage();
        Luggage RetrieveLuggageByID(int id);
        bool UpdateLuggage(Luggage oldLuggage, Luggage newLuggage);
        List<LuggageStatus> RetrieveAllLuggageStatus();
    }
}