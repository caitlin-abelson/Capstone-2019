/// <summary>
/// Wes Richardson
/// Created: 2019/02/12
/// 
/// A test class using the IRoomAccessor Interface to interact with mock data
/// </summary>
/// <remarks>
/// Wes Richardson
/// Updated: 2019/02/20
/// 
/// Update to reflect changes to Room Object and Needed Look up properties
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class MockRoomAccessor : IRoomAccessor
    {
        private List<Room> roomList;
        private List<string> buildingList;
        private List<string> roomTypeList;
        private List<string> roomStatusList;
        private List<int> offeringIDList;
        private List<int> resortPropertyIDList;
        private int nextRoomID = 100000;
        private int nextofferingIDAndPropertyID;

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/12
        /// Constructor for to Create the mock data
        /// 
        /// </summary>
        public MockRoomAccessor()
        {
            BuildRooms();
            BuildBuildings();
            BuildRoomTypes();
            BuildRoomStatusTypes();
            BuildProperyAndOfferingIDs();
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/12
        /// 
        /// </summary>
        /// <param name="room"></param>
        /// <returns>A 1 when a room was added</returns>
        public int InsertNewRoom(Room room)
        {
            room.RoomID = nextRoomID;
			nextRoomID++;
            roomList.Add(room);
            return 1;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/12
        /// 
        /// </summary>
        /// <returns>A list of Mock Data Buildings</returns>
        public List<string> SelectBuildings()
        {
            return buildingList;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/12
        /// 
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns>A Mock room based on the roomID</returns>
        public Room SelectRoomByID(int roomID)
        {
            Room room = roomList.Find(x => x.RoomID == roomID);
            if(room != null)
            {
                return room;
            }
            else
            {
                throw new ApplicationException("No room with ID of " + roomID);
            }
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/12
        /// 
        /// </summary>
        /// <returns>A list of Mock Data rooms</returns>
        public List<Room> SelectRoomList()
        {
            return roomList;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/12
        /// 
        /// </summary>
        /// <returns>A list of Mock Data room types</returns>
        public List<string> SelectRoomTypes()
        {
            return roomTypeList;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/12
        /// 
        /// </summary>
        /// <param name="room"></param>
        /// <returns>The numbers of rows affected in the Mock Data</returns>
        public int UpdateRoom(Room room)
        {
            int rowAffected = 0;
            for (int i = 0; i < roomList.Count; i++)
            {
                if(room.RoomID == roomList[i].RoomID)
                {
                    roomList.RemoveAt(i);
                    roomList.Add(room);
                    roomList.OrderBy(x => x.RoomID);
                    rowAffected = 1;
                }
            }
            return rowAffected;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/12
        /// 
        /// Deletes a Room From the Mock Data
        /// </summary>
        /// <param name="room"></param>
        /// <returns>The number of rows affected</returns>
        public int DeleteRoom(Room room)
        {
            return DeleteRoomByID(room.RoomID);
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/12
        /// 
        /// Deletes the Room with the given ID from the Mock Data
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns>The number of rows affected</returns>
        public int DeleteRoomByID(int roomID)
        {
            int rows = 0;
            bool results = roomList.Remove(roomList.Where(r => r.RoomID == roomID).First());
            if(results == true)
            {
                rows = 1;
            }
            return rows;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/20
        /// 
        /// </summary>
        /// <returns>A list of Room Status</returns>
        public List<string> SelectRoomStatusList()
        {
            return roomStatusList;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/20
        /// 
        /// </summary>
        /// <returns>A list of Offering IDs</returns>
        public List<int> SelectOfferingIDList()
        {
            return offeringIDList;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/20
        /// 
        /// </summary>
        /// <returns>A list of Properting IDs</returns>
        public List<int> SelectResortProperyIDList()
        {
            return resortPropertyIDList;
        }
        private void BuildRooms()
        {
            roomList = new List<Room>();
            Room room0 = new Room
            {
                RoomID = nextRoomID,
                RoomNumber = "101",
                Building = "Test Building 1",
                RoomType = "Test Room Type 1",
                Description = "Test Room " + nextRoomID,
                Capacity = 2,
                Available = true,
                Price = 200.00M,
                Active = true,
                OfferingID = 100002,
                RoomStatus = "Ready",
                ResortPropertyID = 100003
            };
            roomList.Add(room0);
            nextRoomID++;
            Room room1 = new Room
            {
                RoomID = nextRoomID,
                RoomNumber = "101",
                Building = "Test Building 2",
                RoomType = "Test Room Type 2",
                Description = "Test Room " + nextRoomID,
                Capacity = 2,
                Available = true,
                Price = 200.00M,
                Active = true,
                OfferingID = 100002,
                RoomStatus = "Ready",
                ResortPropertyID = 100003
            };
            roomList.Add(room1);
            nextRoomID++;
            Room room2 = new Room
            {
                RoomID = nextRoomID,
                RoomNumber = "101",
                Building = "Test Building 3",
                RoomType = "Test Room Type 3",
                Description = "Test Room " + nextRoomID,
                Capacity = 2,
                Available = true,
                Price = 200.00M,
                Active = true,
                OfferingID = 100002,
                RoomStatus = "Occupied",
                ResortPropertyID = 100003
            };
            roomList.Add(room2);
            nextRoomID++;
            Room room3 = new Room
            {
                RoomID = nextRoomID,
                RoomNumber = "101",
                Building = "Test Building 4",
                RoomType = "Test Room Type 4",
                Description = "Test Room " + nextRoomID,
                Capacity = 2,
                Available = true,
                Price = 200.00M,
                Active = true,
                OfferingID = 100002,
                RoomStatus = "Needs Cleaning",
                ResortPropertyID = 100003
            };
            roomList.Add(room3);
            nextRoomID++;
            Room room4 = new Room
            {
                RoomID = nextRoomID,
                RoomNumber = "102",
                Building = "Test Building 1",
                RoomType = "Test Room Type 1",
                Description = "Test Room " + nextRoomID,
                Capacity = 2,
                Available = true,
                Price = 200.00M,
                Active = true,
                OfferingID = 100002,
                RoomStatus = "Murder Scene",
                ResortPropertyID = 100003
            };
            roomList.Add(room4);
            nextRoomID++;
            Room room5 = new Room
            {
                RoomID = nextRoomID,
                RoomNumber = "103",
                Building = "Test Building 3",
                RoomType = "Test Room Type 2",
                Description = "Test Room " + nextRoomID,
                Capacity = 2,
                Available = false,
                Price = 200.00M,
                Active = true,
                OfferingID = 100002,
                RoomStatus = "Needs Fumigation",
                ResortPropertyID = 100003
            };
            roomList.Add(room5);
            nextRoomID++;
            Room room6 = new Room
            {
                RoomID = nextRoomID,
                RoomNumber = "104",
                Building = "Test Building 4",
                RoomType = "Test Room Type 2",
                Description = "Test Room " + nextRoomID,
                Capacity = 2,
                Available = true,
                Price = 200.00M,
                Active = true,
                OfferingID = 100002,
                RoomStatus = "Needs Inspection",
                ResortPropertyID = 100003
            };
            roomList.Add(room6);
            nextRoomID++;
            Room room7 = new Room
            {
                RoomID = nextRoomID,
                RoomNumber = "101",
                Building = "Test Building 3",
                RoomType = "Test Room Type 3",
                Description = "Test Room " + nextRoomID,
                Capacity = 2,
                Available = true,
                Price = 200.00M,
                Active = true,
                OfferingID = 100002,
                RoomStatus = "Jim Quote Needed",
                ResortPropertyID = 100003
            };
            roomList.Add(room7);
            nextRoomID++;
        }

        private void BuildBuildings()
        {
            buildingList = new List<string>();
            for (int i = 1; i < 5; i++)
            {
                buildingList.Add("Test Building " + i);
            }
        }

        private void BuildRoomTypes()
        {
            roomTypeList = new List<string>();
            for (int i = 1; i < 5; i++)
            {
                roomTypeList.Add("Test Room Type " + i);
            }
        }

        private void BuildRoomStatusTypes()
        {
            roomStatusList = new List<string>();
            roomStatusList.Add("Ready");
            roomStatusList.Add("Occupied");
            roomStatusList.Add("Needs Cleaning");
            roomStatusList.Add("Murder Scene");
            roomStatusList.Add("After Rock Star Cleaning");
            roomStatusList.Add("Needs Fumigation");
            roomStatusList.Add("Needs Inspection");
            roomStatusList.Add("Jim Quote Needed");
        }

        private void BuildProperyAndOfferingIDs()
        {
            offeringIDList = new List<int>();
            resortPropertyIDList = new List<int>();

            for (nextofferingIDAndPropertyID = 100000;  nextofferingIDAndPropertyID < 100009; nextofferingIDAndPropertyID++)
            {
                offeringIDList.Add(nextofferingIDAndPropertyID);
                resortPropertyIDList.Add(nextofferingIDAndPropertyID);
            }
        }
    }
}
