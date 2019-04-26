using System;

namespace DataObjects
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Resort Property object
    /// </summary>
    [Serializable]
    public class ResortProperty
    {
        public int ResortPropertyId { get; set; }

        public string ResortPropertyTypeId { get; set; }
    }
}