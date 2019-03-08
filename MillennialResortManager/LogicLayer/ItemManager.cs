using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

/// <summary>
/// Richard Carroll
/// Created: 2019/01/30
/// 
/// This class is used to retrieve Item data for the
/// presentation layer.
/// </summary>
namespace LogicLayer
{
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

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/15/2019
        /// ItemManager Is an implementation of the IItemManager Interface meant to interact with the Item Accessor
        /// </summary>
        public ItemManager()
        {
            _itemAccessor = new ItemAccessor();
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/15/2019
        /// ItemManager Is an implementation of the IItemManager Interface meant to interact with the mock accessor
        /// </summary>
        public ItemManager(ItemAccessorMock mock)
        {
            _itemAccessor = mock;
        }


        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/23
        ///
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
            catch (Exception)
            {

                throw;
            }

            return items;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/02/07
        ///
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
            catch (Exception)
            {

                throw;
            }

            return item;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/30
        ///
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
            catch (Exception)
            {

                throw;
            }

            return items;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/02/06
        ///
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
            catch (Exception)
            {
                throw;
            }
            return id;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/02/09
        ///
        /// Updates an Item with a new Item.
        /// </summary>
        /// 
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
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
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
            catch (Exception)
            {

                throw;
            }

            return items;
        }
    }
}
