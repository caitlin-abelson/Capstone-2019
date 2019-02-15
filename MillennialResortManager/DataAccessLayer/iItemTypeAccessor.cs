using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
	/// Kevin Broskow
	/// Created: 2019/01/20
	/// 
	/// Interface for accessing ItemType Data
	/// </summary>
    interface iItemTypeAccessor
    {
        void CreateItemType(ItemType newItemType);
        ItemType RetrieveItemType();
        List<String> RetrieveAllItemTypes();
        void UpdateItemType(ItemType newItemType, ItemType oldItemType);
        void DeactivateItemType(ItemType deactivatingItemType);
        void PurgeItemType(ItemType purgingItemType);

    }
}
