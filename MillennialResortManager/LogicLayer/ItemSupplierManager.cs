
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
	/// <summary author="Eric Bostwick" created="2019/02/05">
	/// Logic Layer Class for interacting with Data Access Layer
	/// for managing Item Supplier Table
	/// </summary>
	public class ItemSupplierManager : IItemSupplierManager
	{
		private IItemSupplierAccessor _itemSupplierAccessor;

		public ItemSupplierManager()
		{
			_itemSupplierAccessor = new ItemSupplierAccessor();
		}
		public ItemSupplierManager(ItemSupplierAccessorMock itemSupplierAccessorMock)
		{
			_itemSupplierAccessor = itemSupplierAccessorMock;
		}

		/// <summary author="Eric Bostwick" created="2019/02/05">
		/// Insert an Record to the itemsupplier table
		/// </summary>
		/// <returns>
		/// returns 1 if successful 0 if not
		/// </returns>
		public int CreateItemSupplier(ItemSupplier itemSupplier)
		{
			int result;

			try
			{
				if (!itemSupplier.IsValid())
				{
					throw new ArgumentException("Data for this itemsupplier record is invalid");
				}
				result = _itemSupplierAccessor.InsertItemSupplier(itemSupplier);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return result;
		}

		/// <summary author="Eric Bostwick" created="2019/02/04">
		/// Gets list of itemsuppliers from itemsupplier table
		/// based upon the itemid
		/// </summary>
		/// <returns>
		/// List of ItemSupplers
		/// </returns>
		public List<ItemSupplier> RetrieveAllItemSuppliersByItemID(int itemID)
		{

			List<ItemSupplier> _itemSuppliers;
			try
			{
				_itemSuppliers = _itemSupplierAccessor.SelectItemSuppliersByItemID(itemID);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return _itemSuppliers;
		}

		/// <summary author="Eric Bostwick" created="2019/02/04">
		/// Gets an itemsupplier from itemsupplier table
		/// based upon the itemid and supplierID
		/// </summary>
		/// <returns>
		/// a supplierID
		/// </returns>
		public ItemSupplier RetrieveItemSupplier(int itemID, int supplierID)
		{
			ItemSupplier _itemSupplier;
			try
			{
				_itemSupplier = _itemSupplierAccessor.SelectItemSupplierByItemIDandSupplierID(itemID, supplierID);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return _itemSupplier;
		}

		/// <summary author="Eric Bostwick" created="2019/02/06">
		/// Updates an itemsuppler item by calling UpdateItemSupplier in the
		/// ItemSupplierAccessor 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="oldItem"></param>
		/// <returns>
		/// 1 if update successful 0 if not
		/// </returns>
		public int UpdateItemSupplier(ItemSupplier newItemSupplier, ItemSupplier oldItemSupplier)
		{
			int result;
			try
			{
				if (!newItemSupplier.IsValid())
				{
					throw new ArgumentException("Data for this itemsupplier is invalid");
				}
				result = _itemSupplierAccessor.UpdateItemSupplier(newItemSupplier, oldItemSupplier);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		/// <summary author="Eric Bostwick" created="2019/02/06">
		/// Updated:
		/// RetrieveAllSuppliersForItemSupplierManageMent takes the list that was returned from the DataAccess Layer
		/// and pushes it to the presentation layer so that it can be displayed in the data grid
		/// </summary>
		/// <returns></returns>
		public List<Supplier> RetrieveAllSuppliersForItemSupplierManagement(int itemID)
		{
			List<Supplier> suppliers = null;

			try
			{
				suppliers = _itemSupplierAccessor.SelectAllSuppliersForItemSupplierManagement(itemID);
			}
			catch (Exception)
			{
				throw;
			}

			return suppliers;
		}

		public int DeleteItemSupplier(int itemID, int supplierID)
		{
			int result;
			try
			{
				result = _itemSupplierAccessor.DeleteItemSupplier(itemID, supplierID);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public int DeactivateItemSupplier(int itemID, int supplierID)
		{
			int result;
			try
			{
				result = _itemSupplierAccessor.DeactivateItemSupplier(itemID, supplierID);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
	}
}
