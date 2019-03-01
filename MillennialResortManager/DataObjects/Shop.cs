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
