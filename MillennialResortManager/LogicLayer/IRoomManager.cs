using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IRoomManager
    {
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/04
        /// 
        /// Updated to accomidate the number of rooms to be added
        /// </remarks>
        bool CreateRoom(Room room, int employeeID, int numOfRooms);
        Room RetreieveRoomByID(int roomID);
        List<string> RetrieveBuildingList();
        List<Room> RetrieveRoomList();
        List<string> RetrieveRoomTypeList();
        bool UpdateRoom(Room room);
        bool DeleteRoom(Room room);
        bool DeleteRoomByID(int roomID);
        List<string> RetrieveRoomStatusList();
    }
}