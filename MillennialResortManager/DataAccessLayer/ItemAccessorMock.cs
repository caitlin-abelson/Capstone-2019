using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created : 02/14/2019
    /// This is a mock Data accessor which implements the IItemAccessor interface. It is used for testing purposes only
    /// </summary>
    public class ItemAccessorMock : IItemAccessor
    {
        private List<Item> _items;
        private List<RecipeItemLineVM> _recipeItemLines;
        private List<Item> _kevinItems;

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/14/2019
        /// Updated : 02/19/2019
        /// This constructor sets up all of our dummy data we will be using
        /// </summary>
        public ItemAccessorMock()
        {
            _items = new List<Item>();
            _items.Add(new Item(100000, null, false, null, "Food", "It's an onion.", 0, "White Onion", 0, new DateTime(2008, 11, 11), true));
            _items.Add(new Item(100001, 100000, true, 100000, "Food", "It's a burger.", 0, "Big Boy Burger", 0, new DateTime(2008, 11, 11), true));
            _items.Add(new Item(100002, null, false, null, "Food", "It's a sweet BBQ sauce.", 0, "Famous Dave's BBQ Sauce ", 0, new DateTime(2008, 11, 11), true));
            _items.Add(new Item(100003, 100000, false, 100001, "Food", "It's a savory bun.", 0, "Homemade Sesame Roll", 0, new DateTime(2008, 11, 11), true));
            _items.Add(new Item(100004, null, false, null, "Food", "It's ground hamburger.", 0, "Hamburger", 0, new DateTime(2008, 11, 11), true));

            _recipeItemLines = new List<RecipeItemLineVM>();
            _recipeItemLines.Add(new RecipeItemLineVM(100000, 100000, "White Onion", 1, "Whole"));
            _recipeItemLines.Add(new RecipeItemLineVM(100002, 100000, "Famous Dave's BBQ Sauce", 1, "Tsp"));
            _recipeItemLines.Add(new RecipeItemLineVM(100003, 100000, "Homemade Sesame Roll", 1, "Bun"));
            _recipeItemLines.Add(new RecipeItemLineVM(100004, 100000, "Hamburger", 1, "Quarter Pound"));

            _kevinItems = new List<Item>();
            _kevinItems.Add(new Item() { ItemID = 100000, ItemType = "Food", RecipeID = 10000, CustomerPurchasable = true, Description = "This is a test description", OnHandQty = 2, Name = "Taco", ReorderQty = 5, DateActive = DateTime.Now, Active = true });
            _kevinItems.Add(new Item() { ItemID = 100001, ItemType = "Food", RecipeID = 10000, CustomerPurchasable = true, Description = "This is a test description", OnHandQty = 2, Name = "Burrito", ReorderQty = 5, DateActive = DateTime.Now, Active = true });
            _kevinItems.Add(new Item() { ItemID = 100002, ItemType = "Food", RecipeID = 10000, CustomerPurchasable = true, Description = "This is a test description", OnHandQty = 2, Name = "Nacho", ReorderQty = 5, DateActive = DateTime.Now, Active = true });
            _kevinItems.Add(new Item() { ItemID = 100003, ItemType = "Food", RecipeID = 10000, CustomerPurchasable = true, Description = "This is a test description", OnHandQty = 2, Name = "Tbone Steak", ReorderQty = 5, DateActive = DateTime.Now, Active = true });
            _kevinItems.Add(new Item() { ItemID = 100004, ItemType = "Food", RecipeID = 10000, CustomerPurchasable = true, Description = "This is a test description", OnHandQty = 2, Name = "Mashed Potatoes", ReorderQty = 5, DateActive = DateTime.Now, Active = true });

        }



        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/14/2019
        /// This will create an item using the data provided in the Item item.
        /// </summary>
        /// <param name="item">The Item we want to add to our mock system.</param>
        /// <returns>The ID of the Item</returns>
        public int InsertItem(Item item)
        {
            _items.Add(item);
            return item.ItemID;
        }


        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/14/2019
        /// This will return a List of all items in the mock system.
        /// </summary>
        /// <returns>A list of all items</returns>
        public List<Item> SelectAllItems()
        {
            return _items;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/14/2019
        /// This will return a specific Item based on RecipeID from the mock system.
        /// </summary>
        /// <param name="recipeID">The ID we want to search through Items for.</param>
        /// <returns>The Item object with that Recipe ID</returns>
        public Item SelectItemByRecipeID(int recipeID)
        {
            return _items.Find(i => i.RecipeID == recipeID);
        }

        public List<Item> SelectItemNamesAndIDs()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/14/2019
        /// This will return a specific Item based on RecipeID from the mock system.
        /// </summary>
        /// <param name="recipeID">The ID we want to search through Items for.</param>
        /// <returns>A List of Item objects used in a Recipe</returns>
        public List<Item> SelectLineItemsByRecipeID(int recipeID)
        {
            List<Item> recipeItems = new List<Item>();
            List<int> idValues = new List<int>();
            foreach (var recipeItem in _recipeItemLines)
            {
                if (recipeItem.RecipeID == recipeID)
                {
                    idValues.Add(recipeItem.ItemID);
                }
            }
            foreach (int id in idValues)
            {
                var item = _items.Find(x => x.ItemID == id);
                if (item != null)
                {
                    recipeItems.Add(item);
                }
            }
            if (recipeItems.Count == 0)
            {
                recipeItems = null;
            }
            return recipeItems;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/14/2019
        /// This will return a specific Item based on RecipeID from the mock system.
        /// </summary>
        /// <param name="oldItem">The old item.</param>
        /// <param name="newItem">The new updated item.</param>
        /// <returns>1 if successful, 0 if not</returns>
        public int UpdateItem(Item oldItem, Item newItem)
        {
            int rowsAffected = 0;
            foreach (var item in _items)
            {
                if (item.ItemID == oldItem.ItemID)
                {
                    item.Active = newItem.Active;
                    item.CustomerPurchasable = newItem.CustomerPurchasable;
                    item.DateActive = newItem.DateActive;
                    item.Description = newItem.Description;
                    item.ItemType = newItem.ItemType;
                    item.Name = newItem.Name;
                    item.OfferingID = newItem.OfferingID;
                    item.OnHandQty = newItem.OnHandQty;
                    item.RecipeID = newItem.RecipeID;
                    item.ReorderQty = newItem.ReorderQty;
                    rowsAffected = 1;

                    break;
                }

            }
            return rowsAffected;
        }

        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 02/14/2019
        public void DeactivateItem(Item deactivatingItem)
        {
            Item p = null;
            p = _items.Find(x => x.ItemID == deactivatingItem.ItemID);
            if (p == null || p.ItemID == -1)
            {
                throw new ArgumentException("Item not found in system");
            }
            else
            {
                p.Active = false;
            }
        }
        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 02/14/2019
        public void DeleteItem(Item purgingItem)
        {
            bool foundItem = true;
            foreach (var item in _kevinItems)
            {
                if (item.ItemID == purgingItem.ItemID)
                {
                    item.Active = false;
                    foundItem = false;

                    _items.Remove(_kevinItems.Find(x => x.ItemID == purgingItem.ItemID));
                    break;
                }
            }
            if (!foundItem)
            {
                throw new ArgumentException("No item was found in the system");
            }
        }
        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 02/14/2019
        public Item SelectItem(int itemID)
        {
            Item p = new Item();
            p = _items.Find(x => x.ItemID == itemID);
            if (p == null)
            {
                throw new ArgumentException("ItemID did not match any Item in the system");
            }
            return p;
        }
        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 02/14/2019
        public List<Item> SelectActiveItems()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 02/14/2019
        public List<Item> SelectDeactiveItems()
        {
            throw new NotImplementedException();
        }
    }
}
