using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
	/// Jared Grenfield / Kevin Broskow
	/// Created: 2019/01/20
	/// 
	/// Interface for accessing Item Data
	/// </summary>
    public interface IItemAccessor
    {

        int InsertItem(Item item);
        Item SelectItem(int itemID);
        List<Item> SelectAllItems();
        int UpdateItem(Item oldItem, Item newItem);
        void DeactivateItem(Item deactivatingItem);
        void DeleteItem(Item purgingItem);
        List<Item> SelectActiveItems();
        List<Item> SelectDeactiveItems();
        List<Item> SelectItemNamesAndIDs();
        Item SelectItemByRecipeID(int recipeID);
        List<Item> SelectLineItemsByRecipeID(int recipeID);

    }
}