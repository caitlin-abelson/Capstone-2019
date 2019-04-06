using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Interface for Resort Property Accessor
    /// </summary>
    public interface IResortPropertyAccessor
    {
        int AddResortProperty(ResortProperty resortProperty);

        ResortProperty RetrieveResortPropertyById(string id);

        IEnumerable<ResortProperty> RetrieveResortProperties();

        void UpdateResortProperty(ResortProperty oldResortProperty, ResortProperty newResortProperty);

        void DeleteResortProperty(string id);
    }
}
