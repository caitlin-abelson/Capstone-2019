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
    public interface IItemTypeAccessor
    {
        void CreateItemType(ItemType newItemType);
        ItemType RetrieveItemType();
        List<string> RetrieveAllItemTypesString();
        List<ItemType> RetrieveAllItemTypes();
        void UpdateItemType(ItemType newItemType, ItemType oldItemType);
        void DeactivateItemType(ItemType deactivatingItemType);
        void PurgeItemType(ItemType purgingItemType);
    }
}
