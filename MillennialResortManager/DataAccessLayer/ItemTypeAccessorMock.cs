using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class ItemTypeAccessorMock : IItemTypeAccessor
    {
        private List<ItemType> _itemTypes;
        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// This constructor sets up all of our dummy data we will be using
        /// </summary>
        public ItemTypeAccessorMock()
        {
            _itemTypes = new List<ItemType>();
            _itemTypes.Add(new ItemType("Food", "Edible things for customers"));
            _itemTypes.Add(new ItemType("Janitorial Products", "Things for cleaning."));
            _itemTypes.Add(new ItemType("Office Supplies", "Paper, pencils, and other items for record keeping."));
        }

        public void CreateItemType(ItemType newItemType)
        {
            throw new NotImplementedException();
        }

        public void DeactivateItemType(ItemType deactivatingItemType)
        {
            throw new NotImplementedException();
        }

        public void PurgeItemType(ItemType purgingItemType)
        {
            throw new NotImplementedException();
        }

        public List<string> RetrieveAllItemTypes()
        {
            throw new NotImplementedException();
        }

        public List<string> RetrieveAllItemTypesString()
        {
            throw new NotImplementedException();
        }

        public ItemType RetrieveItemType()
        {
            throw new NotImplementedException();
        }

        public List<ItemType> SelectAllItemTypes()
        {
            return _itemTypes;
        }

        public void UpdateItemType(ItemType newItemType, ItemType oldItemType)
        {
            throw new NotImplementedException();
        }

        List<ItemType> IItemTypeAccessor.RetrieveAllItemTypes()
        {
            return _itemTypes;
        }
    }
}
