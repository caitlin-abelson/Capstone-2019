/// <summary>
/// Austin Berquam
/// Created: 2019/02/23
/// 
/// This is a mock Data Accessor which implements IRoomTypeAccessor.  This is for testing purposes only.
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
    public class MockRoomTypeAccessor : IRoomTypeAccessor
    {
        private List<RoomType> type;

        /// <summary>
        /// Author: Austin Berquam
        /// Created: 2019/02/23
        /// This constructor that sets up dummy data
        /// </summary>
        public MockRoomTypeAccessor()
        {
            type = new List<RoomType>
            {
                new RoomType {RoomTypeID = "type1", Description = "type is a type"},
                new RoomType {RoomTypeID = "type2", Description = "type is a type"},
                new RoomType {RoomTypeID = "type3", Description = "type is a type"},
                new RoomType {RoomTypeID = "type4", Description = "type is a type"}
            };
        }
        public int DeleteRoomType(string roomTypeID)
        {
            int rowsDeleted = 0;
            foreach (var room in type)
            {
                if (room.RoomTypeID == roomTypeID)
                {
                    int listLength = type.Count;
                    type.Remove(room);
                    if (listLength == type.Count - 1)
                    {
                        rowsDeleted = 1;
                    }
                }
            }

            return rowsDeleted;
        }

        public int InsertRoomType(RoomType roomType)
        {
            int listLength = type.Count;
            type.Add(roomType);
            if (listLength == type.Count - 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public List<string> SelectAllTypes()
        {
            throw new NotImplementedException();
        }

        public List<RoomType> SelectRoomTypes(string status)
        {
            return type;
        }
    }
}
