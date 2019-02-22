using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
	/// Kevin Broskow
	/// Created: 2019/01/20
	/// 
	/// Manages ItemType objects for use with MSSQL
	/// </summary>
    public class ItemTypeManagerMSSQL : IItemTypeManager
    {
        ItemTypeAccessorMSSQL _itemTypeAccessor = new ItemTypeAccessorMSSQL();
        public void AddItemType(ItemType newItemType)
        {
            throw new NotImplementedException();
        }

        public void DeleteItemType()
        {
            throw new NotImplementedException();
        }

        public void EditItemType(ItemType newItemType, ItemType oldItemType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/23
        /// 
        /// Method used to retrieve all of the ItemTypes stored in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <returns>List<String> That contains all of the itemTypeIDs</returns>	
        public List<String> RetrieveAllItemTypes()
        {
            List<String> itemTypes = new List<String>();
            try
            {
                itemTypes = _itemTypeAccessor.RetrieveAllItemTypes();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return itemTypes;
        }


        public ItemType RetrieveItemType()
        {
            throw new NotImplementedException();
        }
        
    }
}
