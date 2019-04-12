/// <summary>
/// Wes Richardson
/// Created: 2019/01/24
/// 
/// Manager to pass data from the DataAcces and Presentation Namespaces
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class RoomManager : IRoomManager
    {

        private IRoomAccessor _roomAccessor;

        /// <summary>
        /// Dani Russo
        /// Updated: 2019/04/05
        /// 
        /// _roomAccessor is assigned to actual accessor, not mock accessor
        /// </summary>
        public RoomManager()
        {
            // used for database access, to be used when Intergartion testing is ready
            _roomAccessor = new RoomAccessor();
        }


        public RoomManager(IRoomAccessor rA)
        {
            _roomAccessor = rA;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// Retrieves a List of the buildings for the Resort
        /// <returns>List of building</returns>
        /// </summary>
        public List<string> RetrieveBuildingList()
        {
            List<string> buildingIDList = new List<string>();

            try
            {
                buildingIDList = _roomAccessor.SelectBuildings();
            }
            catch (Exception)
            {

                throw;
            }
            return buildingIDList;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// Retrieves a List of the Room Types the Hotel Has
        /// <returns>List of room types</returns>
        /// </summary>
        public List<string> RetrieveRoomTypeList()
        {
            List<string> roomTypeIDList = new List<string>();

            try
            {
                roomTypeIDList = _roomAccessor.SelectRoomTypes();
            }
            catch (Exception)
            {

                throw;
            }
            return roomTypeIDList;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// Inserts a new Room into the database
        /// <param name="room">A room object</param>
        /// 
        /// <returns>A bool if the room was created</returns>
        /// </summary>
        public bool CreateRoom(Room room, int employeeID)
        {
            int rows = 0;
            bool roomCreated = false;

            try
            {
                RoomVerifier.VerifyRoom(room, _roomAccessor);
                rows = _roomAccessor.InsertNewRoom(room, employeeID);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE"))
                {
                    throw new ApplicationException("Room already exists.", ex);
                }
                throw;
            }
            if (rows > 0)
            {
                roomCreated = true;
            }

            return roomCreated;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/30
        /// 
        /// Retreieves a Room By its ID
        /// <param name="roomID">The ID of the room to retrieve</param>
        /// <returns>A room</returns>
        /// </summary>
        public Room RetreieveRoomByID(int roomID)
        {
            Room room = null;
            try
            {
                room = _roomAccessor.SelectRoomByID(roomID);
            }
            catch (Exception)
            {

                throw;
            }

            return room;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/30
        /// 
        /// Updates A room
        /// <param name="room">A room to update</param>
        /// <returns>A bool teling if the room was updated</returns>
        /// </summary>
        public bool UpdateRoom(Room room)
        {
            bool results = false;
            int rows = 0;

            try
            {
                RoomVerifier.VerifyRoom(room, _roomAccessor);
                rows = _roomAccessor.UpdateRoom(room);
            }
            catch (Exception)
            {

                throw;
            }

            if (rows > 0)
            {
                results = true;
            }
            return results;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/07
        /// 
        /// Retrieves a list of Rooms
        /// 
        /// <returns>List of Room</returns>
        /// </summary>
        public List<Room> RetrieveRoomList()
        {
            List<Room> roomList = null;
            try
            {
                roomList = _roomAccessor.SelectRoomList();
            }
            catch (Exception)
            {

                throw;
            }
            return roomList;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/14
        /// 
        /// Deletes the Given Room
        /// </summary>
        /// <param name="room"></param>
        /// <returns>The number of rows affected</returns>
        public bool DeleteRoom(Room room)
        {
            bool results = false;
            try
            {
                results = DeleteRoomByID(room.RoomID);
            }
            catch (Exception)
            {

                throw;
            }
            return results;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/14
        /// 
        /// Deletes the room with the given ID
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns>The number of rows affected</returns>
        public bool DeleteRoomByID(int roomID)
        {
            // Needs to Be writen
            bool results = false;
            int rows = 0;
            try
            {
                rows = _roomAccessor.DeleteRoomByID(roomID);
                if (rows > 0)
                {
                    results = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return results;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/20
        /// 
        /// Retrieves a list of Room Statuses
        /// 
        /// <returns>List of Room Statuses</returns>
        /// </summary>
        public List<string> RetrieveRoomStatusList()
        {
            List<string> statusList = null;
            try
            {
                statusList = _roomAccessor.SelectRoomStatusList();
            }
            catch (Exception)
            {

                throw;
            }

            return statusList;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/04/10
        /// 
        /// </summary>
        /// <returns>A list of Rooms in a selected building</returns>
        public List<Room> RetrieveRoomListByBuildingID(string buildingID)
        {
            List<Room> rooms = null;

            try
            {
                rooms = _roomAccessor.SelectRoomsByBuildingID(buildingID);
            }
            catch (Exception)
            {

                throw;
            }

            return rooms;
        }
    }
}
