using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Resort Property Type Manager Interface
    /// </summary>
    public interface IResortPropertyTypeManager
    {
        string AddResortPropertyType(ResortPropertyType resortPropertyType);

        ResortPropertyType RetrieveResortPropertyTypeById(string id);

        IEnumerable<ResortPropertyType> RetrieveResortPropertyTypes();

        void UpdateResortPropertyType(ResortPropertyType old, ResortPropertyType newResortPropertyType);

        void DeleteResortPropertyType(string id, Employee employee);
    }
}