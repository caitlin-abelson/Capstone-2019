using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	public class ShopManagerMSSQL : IShopManager
	{
		private Shop _shop = new Shop();
		private IShopAccessor _shopAccessor;

		/// <summary author="Kevin Broskow" created="2019/02/27">
		/// The constructor for the ShopManager class
		/// </summary>
		public ShopManagerMSSQL()
		{
			_shopAccessor = new ShopAccessorMSSQL();
		}

		/// <summary author="Kevin Broskow" created="2019/02/27">
		/// Constructor for the mock accessor
		/// </summary>
		/// <param name="employeeAccessorMock"></param>
		public ShopManagerMSSQL(IShopAccessor shopAccessorMock)
		{
			_shopAccessor = shopAccessorMock;
		}

		/// <summary author="James Heim" created="2019/03/08">
		/// Activate the shop passed in by calling the shop accessor method.
		/// </summary>
		/// <param name="shop"></param>
		/// <returns></returns>
		public bool ActivateShop(Shop shop)
		{
			bool result = false;

			try
			{
				if (shop == null)
				{
					throw new NullReferenceException(shop.NullShopError);
				}
				else if (shop.Active == true)
				{
					throw new InvalidOperationException(shop.ActivateActiveShopError);
				}
				else
				{
					result = (1 == _shopAccessor.ActivateShop(shop));
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Kevin Broskow" created="2019/02/27">
		/// Method used for inserting a shop into the database.
		/// </summary>
		/// <param name="employeeAccessorMock"></param>
		public int InsertShop(Shop shop)
		{
			int result = 0;
			if (shop.IsValid())
			{
				result = _shopAccessor.CreateShop(shop);
			}
			else
			{
				throw new ArgumentException();
			}

			return result;
		}

		/// <summary author="James Heim" created="2019/03/08">
		/// Deactivate the shop passed in by calling the shop accessor method.
		/// </summary>
		/// <param name="shop"></param>
		/// <returns></returns>
		public bool DeactivateShop(Shop shop)
		{
			bool result = false;

			try
			{
				if (shop == null)
				{
					throw new NullReferenceException(shop.NullShopError);
				}
				else if (shop.Active == false)
				{
					throw new InvalidOperationException(shop.DeactivateInactiveShopError);
				}
				else
				{
					result = (1 == _shopAccessor.DeactivateShop(shop));
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="James Heim" created="2019/03/07">
		/// Delete the shop via the accessor.
		/// </summary>
		/// <param name="shop"></param>
		/// <returns></returns>
		public bool DeleteShop(Shop shop)
		{
			bool result = false;

			if (RetrieveShopByID(shop.ShopID) == null)
			{
				// Shop Doesn't exist!
				throw new NullReferenceException(shop.NullShopError);

			}
			else if (shop.Active == true)
			{
				throw new InvalidOperationException(shop.DeleteActiveShopError);
			}
			else
			{

				try
				{
					result = (1 == _shopAccessor.DeleteShop(shop));
				}
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}

			return result;
		}

		/// <summary author="James Heim" created="2019/02/28">
		/// Retrieve an IEnumerable of Shop objects from
		/// the database.
		/// </summary>
		/// <returns>IEnumerable of Shops</returns>
		public IEnumerable<Shop> RetrieveAllShops()
		{
			List<Shop> shops = null;

			try
			{
				shops = (List<Shop>)_shopAccessor.SelectShops();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return shops;
		}

		/// <summary author="James Heim" created="2019/02/28">
		/// Retrieve the View Model Shop Objects via 
		/// the ShopAccessorMSSQL.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<VMBrowseShop> RetrieveAllVMShops()
		{
			List<VMBrowseShop> shops = null;

			try
			{
				shops = (List<VMBrowseShop>)_shopAccessor.SelectVMShops();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return shops;
		}

		/// <summary author="James Heim" created="2019/03/07">
		/// Retrieve the shop via the accessor method.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Shop RetrieveShopByID(int id)
		{
			Shop shop;

			try
			{
				shop = _shopAccessor.SelectShopByID(id);

				if (shop == null)
				{
					throw new NullReferenceException(shop.NullShopError);
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return shop;
		}

		public bool UpdateShop(Shop newShop, Shop oldShop)
		{
			bool result = false;
			try
			{
				if (newShop.IsValid())
				{
					if (_shopAccessor.UpdateShop(newShop, oldShop) > 0)
					{
						result = true;
					}
				}
				else
				{
					throw new ArgumentException();
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw new ArgumentException();
			}

			return result;
		}
	}
}