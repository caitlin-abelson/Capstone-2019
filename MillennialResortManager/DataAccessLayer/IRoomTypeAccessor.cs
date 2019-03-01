/// <summary>
/// Austin Berquam
/// Created: 2019/02/06
/// 
/// Interface that implements Create and Delete functions for Room Types
/// for accessor classes.
/// </summary>
using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IRoomTypeAccessor
    {
        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Creates a new room type
        /// </summary>
        List<RoomType> SelectRoomTypes(string status);
        int InsertRoomType(RoomType roomType);


        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Deletes a room type
        /// </summary>
        int DeleteRoomType(string roomTypeID);
        List<string> SelectAllTypes();



    }
}