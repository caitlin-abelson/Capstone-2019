using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="Jared Greenfield" created="2019/01/22">
	/// The interface for Offering object logic operations.
	/// </summary>
	public interface IItemManager
	{
		/// <summary author="Jared Greenfield" created="2019/01/23">
		/// Retrieves a list of all Items.
		/// </summary>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of all Items</returns>
		List<Item> RetrieveAllItems();

		/// <summary author="Jared Greenfield" created="2019/02/06">
		/// Creates an Item.
		/// </summary>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of all Items</returns>
		int CreateItem(Item item);

		/// <summary author="Jared Greenfield" created="2019/01/30">
		/// Retrieves all the Items involved in a recipe.
		/// </summary>
		/// <param name="recipeID">An Offering object to be added to the database.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of Items in a Recipe</returns>
		List<Item> RetrieveLineItemsByRecipeID(int recipeID);

		/// <summary author="Jared Greenfield" created="2019/02/07">
		/// Retrieves all the Item associated with a recipeID.
		/// </summary>
		/// <param name="recipeID">The ID of the Recipe.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of Items in a Recipe</returns>
		Item RetrieveItemByRecipeID(int recipeID);

		/// <summary author="Jared Greenfield" created="2019/02/09">
		/// Updates an Item with a new Item.
		/// </summary>
		/// <param name="oldItem">The old Item.</param>
		/// <param name="newItem">The updated Item.</param>
		/// <exception cref="SQLException">Update Fails (example of exception tag)</exception>
		/// <returns>True if the update was successful, false if not.</returns>
		bool UpdateItem(Item oldItem, Item newItem);
        List<Item> RetrieveItemNamesAndIDs();
        Item RetrieveItem(int itemID);
        void DeactivateItem(Item selectedItem);
        void DeleteItem(Item selecteditem);
    }
}