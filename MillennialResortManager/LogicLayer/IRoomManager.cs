using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IRoomManager
    {
        bool CreateRoom(Room room, int employeeID);
        Room RetreieveRoomByID(int roomID);
        List<string> RetrieveBuildingList();
        List<Room> RetrieveRoomList();
        List<string> RetrieveRoomTypeList();
        /// <summary>
        /// Danielle Russo
        /// Updated: 2019/04/15
        /// 
        /// Updated to add the newRoom info
        /// </summary>
        bool UpdateRoom(Room selectedRoom, Room newRoom);
        bool DeleteRoom(Room room);
        bool DeleteRoomByID(int roomID);
        List<string> RetrieveRoomStatusList();

        /// <summary>
        /// Danielle Russo
        /// Updated: 2019/04/04
        /// 
        /// Updated to accomidate the number of rooms to be added
        /// </summary>
        List<Room> RetrieveRoomListByBuildingID(string buildingID);
    }
}