using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
	/// Kevin Broskow
	/// Created: 2019/01/20
	/// 
	/// Data Object for storing ItemTypes
	/// </summary>
    public class ItemType
    {
        public String ItemTypeID { get; set; }
        public String Description { get; set; }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// Full constructor for ItemType objects.
        /// </summary>
        public ItemType(string itemTypeID, string description)
        {
            ItemTypeID = itemTypeID;
            Description = description;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/03/08
        /// 
        /// Empty constructor for ItemType objects.
        /// </summary>
        public ItemType()
        {

        }

        public bool ValidateDescription()
        {
            bool isValid = true;
            if (Description.Length > 1000)
            {
                isValid = false;
            }
            return isValid;
        }

        public bool IsValid()
        {
            bool isValid = false;
            if (ValidateDescription())
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
