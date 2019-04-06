/// <summary>
/// Wes Richardson
/// Created: 2019/02/12
/// 
/// A Interface for Creating, Reading, Updating, and Deleteing 
/// Data related to Resort Rooms
/// </summary>
/// <remarks>
/// Wes Richardson
/// Updated: 2019/02/20
/// 
/// Added SelectRoomStatusList
/// Update to reflect changes to Room Object and Needed Look up properties
/// </remarks>
using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IRoomAccessor
    {
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/04
        /// 
        /// Updated to accomidate the number of rooms to be added
        /// </remarks>
        int InsertNewRoom(Room room, int employeeID, int numOfRooms);
        List<string> SelectBuildings();
        Room SelectRoomByID(int roomID);
        List<Room> SelectRoomList();
        List<string> SelectRoomTypes();
        int UpdateRoom(Room room);
        int DeleteRoom(Room room);
        int DeleteRoomByID(int roomID);
        List<string> SelectRoomStatusList();
    }
}