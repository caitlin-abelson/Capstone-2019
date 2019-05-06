using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Richard Carroll" created="2019/01/30">
	/// This class is used to retrieve Item data for the
	/// presentation layer.
	/// </summary>
	public class ItemManager : IItemManager
	{
		private IItemAccessor _itemAccessor;
		public List<Item> items
		{
			get
			{
				return _items;
			}
			set
			{
				_items = value;
			}
		}

		private List<Item> _items = new List<Item>();

		/// <summary author="Jared Greenfield" created="2019/02/15">
		/// ItemManager Is an implementation of the IItemManager Interface meant to interact with the Item Accessor
		/// </summary>
		public ItemManager()
		{
			_itemAccessor = new ItemAccessor();
		}

		/// <summary author="Jared Greenfield" created="2019/02/15">
		/// ItemManager Is an implementation of the IItemManager Interface meant to interact with the mock accessor
		/// </summary>
		public ItemManager(ItemAccessorMock mock)
		{
			_itemAccessor = mock;
		}

		/// <summary author="Jared Greenfield" created="2019/01/23">
		/// Retrieves a list of all Items.
		/// </summary>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of all Items</returns>
		public List<Item> RetrieveAllItems()
		{
			List<Item> items = new List<Item>();
			try
			{
				items = _itemAccessor.SelectAllItems();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return items;
		}

		/// <summary author="Jared Greenfield" created="2019/02/07">
		/// Retrieves all the Item associated with a recipeID.
		/// </summary>
		/// <param name="recipeID">An Offering object to be added to the database.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of Items in a Recipe</returns>
		public Item RetrieveItemByRecipeID(int recipeID)
		{
			Item item = null;

			try
			{
				item = _itemAccessor.SelectItemByRecipeID(recipeID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return item;
		}

		/// <summary author="Jared Greenfield" created="2019/01/30">
		/// Retrieves all the Items involved in a recipe.
		/// </summary>
		/// <param name="recipeID">An Offering object to be added to the database.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of Items in a Recipe</returns>
		public List<Item> RetrieveLineItemsByRecipeID(int recipeID)
		{
			List<Item> items = null;

			try
			{
				items = _itemAccessor.SelectLineItemsByRecipeID(recipeID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return items;
		}

		/// <summary author="Jared Greenfield" created="2019/02/06">
		/// Creates an Item.
		/// </summary>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of all Items</returns>
		public int CreateItem(Item item)
		{
			int id = 0;
			try
			{
				if (item.IsValid())
				{
					id = _itemAccessor.InsertItem(item);
				}
				else
				{
					throw new ArgumentException("Data for this Item is not valid.");
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return id;
		}

		/// <summary author="Jared Greenfield" created="2019/02/09">
		/// Updates an Item with a new Item.
		/// </summary>
		/// <param name="oldItem">The old Item.</param>
		/// <param name="newItem">The updated Item.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>True if the update was successful, false if not.</returns>
		public bool UpdateItem(Item oldItem, Item newItem)
		{
			bool result = false;
			try
			{
				if (oldItem.IsValid())
				{
					if (newItem.IsValid())
					{
						if (1 == _itemAccessor.UpdateItem(oldItem, newItem))
						{
							result = true;
						}
					}
					else
					{
						throw new ArgumentException("The new Item is not valid.");
					}
				}
				else
				{
					throw new ArgumentException("The old Item is not valid.");
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="Richard Carroll" created="2019/01/30">
		/// This Method Requests Item Data from the Data Acccess 
		/// Layer and passes it to the Presentation Layer if it's 
		/// successful.
		/// </summary>
		public List<Item> RetrieveItemNamesAndIDs()
		{
			List<Item> items = new List<Item>();

			try
			{
				items = _itemAccessor.SelectItemNamesAndIDs();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return items;
		}

		/// <summary author="Kevin Broskow" created="2019/01/30">
		/// Used to retrieve a specific Item from the database
		/// </summary>
		/// <updates>
		/// <update author="Jared Greenfield" date="2019/04/03">
		/// Converted to Item from Product
		/// </update>
		/// </updates>
		/// <param name="itemID">The unique identifier for a item in the database</param>
		/// <returns>Item</returns>
		public Item RetrieveItem(int itemID)
		{
			Item allItem = null;
			try
			{
				allItem = _itemAccessor.SelectItem(itemID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return allItem;
		}

		/// <summary author="Kevin Broskow" created="2019/01/23">
		/// Method used to deactivate a item
		/// </summary>
		/// <updates>
		/// <update author="Jared Greenfield" date="2019/04/03">
		/// Converted to Item from Product
		/// </update>
		/// </updates>
		/// <param name="selectedItem">The item to be deactivated in the database</param>
		/// <returns>void</returns>
		public void DeactivateItem(Item selectedItem)
		{
			try
			{
				_itemAccessor.DeactivateItem(selectedItem);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Kevin Broskow" created="2019/01/23">
		/// Method used to delete a item from the database
		/// </summary>
		/// <updates>
		/// <update author="Jared Greenfield" date="2019/04/03">
		/// Converted to Item from Product
		/// </update>
		/// </updates>
		/// <param name="selecteditem">The item to be deleted from the database</param>
		/// <returns>void</returns>
		public void DeleteItem(Item selecteditem)
		{
			try
			{
				_itemAccessor.DeleteItem(selecteditem);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Kevin Broskow" created="2019/01/30">
		/// Used to retrieve all active Item from the database
		/// </summary>
		/// <updates>
		/// <update author="Jared Greenfield" date="2019/04/03">
		/// Converted to Item from Product
		/// </update>
		/// </updates>
		/// <returns>List<Item></returns>
		public List<Item> RetrieveActiveItems()
		{
			List<Item> activeItems = new List<Item>();
			try
			{
				activeItems = _itemAccessor.SelectActiveItems();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return activeItems;
		}

		/// <summary author="Kevin Broskow" created="2019/01/30">
		/// Used to retrieve all deactive Products from the database
		/// </summary>
		/// <updates>
		/// <update author="Jared Greenfield" date="2019/04/03">
		/// Converted to Item from Product
		/// </update>
		/// </updates>
		/// <returns>List<Item></returns>	
		public List<Item> RetrieveDeactiveItems()
		{
			List<Item> deactiveItem = new List<Item>();

			try
			{
				deactiveItem = _itemAccessor.SelectDeactiveItems();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return deactiveItem;
		}
	}
}