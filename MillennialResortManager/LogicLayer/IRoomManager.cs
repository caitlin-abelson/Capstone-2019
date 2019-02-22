using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IRoomManager
    {
        bool CreateRoom(Room room);
        Room RetreieveRoomByID(int roomID);
        List<string> RetrieveBuildingList();
        List<Room> RetrieveRoomList();
        List<string> RetrieveRoomTypeList();
        bool UpdateRoom(Room room);
        bool DeleteRoom(Room room);
        bool DeleteRoomByID(int roomID);
        List<string> RetrieveRoomStatusList();
        List<int> RetrieveOfferingIDList();
        List<int> RetrieveResortPropertyIDList();
    }
}