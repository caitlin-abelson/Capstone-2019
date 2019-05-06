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
	/// <summary author="Kevin Broskow" created="2019/01/20">
	/// Manages ItemType objects for use with MSSQL
	/// </summary>
	public class ItemTypeManagerMSSQL : IItemTypeManager
	{
		private IItemTypeAccessor _itemTypeAccessor;

		/// <summary author="Jared Greenfield" created="2019/02/20">
		/// ItemTypeManager Is an implementation of the IItemTypeManager Interface meant to interact with the ItemType Accessor
		/// </summary>
		public ItemTypeManagerMSSQL()
		{
			_itemTypeAccessor = new ItemTypeAccessorMSSQL();
		}

		/// <summary author="Jared Greenfield" created="2019/02/20">
		/// ItemTypeManager Is an implementation of the IItemTypeManager Interface meant to interact with the mock accessor
		/// </summary>
		public ItemTypeManagerMSSQL(ItemTypeAccessorMock itemTypeAccessorMock)
		{
			_itemTypeAccessor = itemTypeAccessorMock;
		}

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

		/// <summary author="Jared Greenfield" created="2019/01/24">
		/// Retrieves all Item Types
		/// </summary>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of all Item Types</returns>
		public List<ItemType> RetrieveAllItemTypes()
		{
			List<ItemType> itemTypes = new List<ItemType>();

			try
			{
				itemTypes = _itemTypeAccessor.RetrieveAllItemTypes();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return itemTypes;
		}

		/// <summary author="Kevin Broskow" created="2019/01/23">
		/// Method used to retrieve all of the ItemTypes stored in the database
		/// </summary>
		/// <returns>List<string> That contains all of the itemTypeIDs</returns>	
		public List<string> RetrieveAllItemTypesString()
		{
			List<string> itemTypes = new List<string>();
			try
			{
				itemTypes = _itemTypeAccessor.RetrieveAllItemTypesString();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
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