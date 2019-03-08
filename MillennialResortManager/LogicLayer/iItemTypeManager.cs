using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
	/// Kevin Broskow
	/// Created: 2019/01/20
	/// 
	/// Interface for the ItemType managers.
	/// </summary>
    public interface IItemTypeManager
    {
        void AddItemType(ItemType newItemType);
        void EditItemType(ItemType newItemType, ItemType oldItemType);
        ItemType RetrieveItemType();
        List<String> RetrieveAllItemTypesString();
        List<ItemType> RetrieveAllItemTypes();
        void DeleteItemType();
    }
}
