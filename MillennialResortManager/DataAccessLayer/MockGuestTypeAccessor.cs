/// <summary>
/// Austin Berquam
/// Created: 2019/02/23
/// 
/// This is a mock Data Accessor which implements IGuestTypeAccessor.  This is for testing purposes only.
/// </summary>
/// 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class MockGuestTypeAccessor : IGuestTypeAccessor
    {
        private List<GuestType> guestType;
        /// <summary>
        /// Author: Austin Berquam
        /// Created: 2019/02/23
        /// This constructor that sets up dummy data
        /// </summary>
        public MockGuestTypeAccessor()
        {
            guestType = new List<GuestType>
            {
                new GuestType {GuestTypeID = "GuestType1", Description = "guestType is a guestType"},
                new GuestType {GuestTypeID = "GuestType2", Description = "guestType is a guestType"},
                new GuestType {GuestTypeID = "GuestType3", Description = "guestType is a guestType"},
                new GuestType {GuestTypeID = "GuestType4", Description = "guestType is a guestType"}
            };
        }

        public int InsertGuestType(GuestType newGuestType)
        {
            int listLength = guestType.Count;
            guestType.Add(newGuestType);
            if (listLength == guestType.Count - 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int DeleteGuestType(string guestTypeID)
        {
            int rowsDeleted = 0;
            foreach (var type in guestType)
            {
                if (type.GuestTypeID == guestTypeID)
                {
                    int listLength = guestType.Count;
                    guestType.Remove(type);
                    if (listLength == guestType.Count - 1)
                    {
                        rowsDeleted = 1;
                    }
                }
            }

            return rowsDeleted;
        }

        public List<string> SelectAllTypes()
        {
            throw new NotImplementedException();
        }

        public List<GuestType> SelectGuestTypes(string status)
        {
            return guestType;
        }
    }
}
