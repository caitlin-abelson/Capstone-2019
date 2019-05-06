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
		/// <updates>
		/// <update author="Danielle Russo" date="2019/04/15">
		/// Updated to add the newRoom info
		/// </update>
		/// </updates>
		bool UpdateRoom(Room selectedRoom, Room newRoom);
        bool DeleteRoom(Room room);
        bool DeleteRoomByID(int roomID);
        List<string> RetrieveRoomStatusList();
		/// <updates>
		/// <update author="Danielle Russo" date="2019/04/04">
		/// Updated to accomidate the number of rooms to be added
		/// </update>
		/// </updates>
		List<Room> RetrieveRoomListByBuildingID(string buildingID);
    }
}