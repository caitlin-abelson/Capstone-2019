using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Resort Property Type Accessor
    /// </summary>
    public interface IResortPropertyTypeAccessor
    {
        string AddResortPropertyType(ResortPropertyType resortPropertyType);

        ResortPropertyType RetrieveResortPropertyTypeById(string id);

        IEnumerable<ResortPropertyType> RetrieveResortPropertyTypes();

        void UpdateResortPropertyType(ResortPropertyType old, ResortPropertyType newResortPropertyType);

        void DeleteResortPropertyType(string id);
    }
}