using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
/// <summary>
 /// Author: Kevin Broskow
 /// Created Date: 2/27/2019
 /// 
 /// The shop data objects class holds the objects for a shop that exists at the resort.
 /// </summary>
    public class Shop
    {
        public int ShopID { get; set; }
        public int RoomID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }


        /// <summary>
        /// Author: James Heim
        /// Created 2019/03/08
        /// 
        /// Error to return when trying to reference a null Shop.
        /// </summary>
        public string NullShopError
        {
            get
            {
                return "There is not a Shop by that ID.";
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019/03/08
        /// 
        /// Error to return when trying to deactivate an inactive shop.
        /// </summary>
        public string DeactivateInactiveShopError
        {
            get
            {
                return "An inactive shop cannot be deactivated.";
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019/03/08
        /// 
        /// Error to return when trying to activate an active shop.
        /// </summary>
        public string ActivateActiveShopError
        {
            get
            {
                return "An active shop cannot be reactivated.";
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019/03/08
        /// 
        /// Error to return when trying to delete an active shop.
        /// </summary>
        public string DeleteActiveShopError
        {
            get
            {
                return "A shop must be deactivated before it can be deleted.";
            }
        }

        public bool IsValid()
        {
            if(validName() && validDescription())
            {
                return true;
            }
            return false;
        }

        private bool validDescription()
        {
            if (Description != null && Description !="" && Description.Length <1001)
            {
                return true;
            }
            return false;
        }

        private bool validName()
        {
            if(Name != null && Name!="" && Name.Length < 51)
            {
                return true;
            }
            return false;
        }

        
    }
    
}
