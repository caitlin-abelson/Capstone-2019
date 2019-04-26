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
	/// Data Object for storing Products
	/// </summary>
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int OnHandQty { get; set; }
        public int ReorderQty { get; set; }
        public string ItemType { get; set; }
        public string Description { get; set; }
        public DateTime DateActive { get; set; }
        public bool Active { get; set; }
        public bool CustomerPurchasable { get; set; }
        public int RecipeID { get; set; }
        public int OfferingID { get; set; }

        public bool IsValid()
        {
            if (ValidateName() && ValidateItemType() && ValidateDescription())
            {
                return true;
            }
            return false;
        }

        public bool ValidateName()
        {
            if (Name != null && Name != "" && Name.Length < 51)
            {
                return true;
            }
            return false;
        }
        public bool ValidateItemType()
        {
            if(ItemType != null && ItemType != "" && ItemType.Length < 16)
            {
                return true;
            }

            return false;
        }
        public bool ValidateDescription()
        {
            if (Description.Length < 1001)
            {
                return true;
            }

            return false;
        }
    }
}
