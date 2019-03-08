using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IItemAccessor
    {
        List<Item> SelectItemNamesAndIDs();
        int InsertItem(Item item);
        List<Item> SelectAllItems();
        Item SelectItemByRecipeID(int recipeID);
        List<Item> SelectLineItemsByRecipeID(int recipeID);
        int UpdateItem(Item oldItem, Item newItem);
    }
}