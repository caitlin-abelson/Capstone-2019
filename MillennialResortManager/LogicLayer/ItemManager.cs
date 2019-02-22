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
        private ItemAccessor _itemAccessor = new ItemAccessor();

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
