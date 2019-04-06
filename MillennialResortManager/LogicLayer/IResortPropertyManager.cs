using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IResortPropertyManager
    {
        int AddResortProperty(ResortProperty resortProperty);

        ResortProperty RetrieveResortPropertyById(string id);

        IEnumerable<ResortProperty> RetrieveResortProperties();

        void UpdateResortProperty(ResortProperty oldResortProperty, ResortProperty newResortProperty);

        void DeleteResortProperty(string id);
    }
}
