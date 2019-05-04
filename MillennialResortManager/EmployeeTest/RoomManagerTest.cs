//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using LogicLayer;
//using DataAccessLayer;
//using DataObjects;
//using System.Collections.Generic;

//namespace LogicLayer.Tests
//{
//    [TestClass]
//    public class RoomManagerTest
//    {
//        private static IRoomAccessor _roomAccessor = new MockRoomAccessor();
//        private static IRoomManager _roomManager = new RoomManager(_roomAccessor);
//        [TestMethod]
//        public void TestCreateRoomAllValid()
//        {
//            // arrange
//            Room room1 = BuildNewRoom();
//            Room room2 = BuildNewRoom();

//            // act
//            _roomManager.CreateRoom(room1, 100000);
//            Room room3 = _roomManager.RetreieveRoomByID(room1.RoomID);
//            room2.RoomID = room3.RoomID;

//            // assert
//            Assert.AreEqual(room2.RoomNumber, room3.RoomNumber);
//            Assert.AreEqual(room2.Building, room3.Building);
//            Assert.AreEqual(room2.RoomType, room3.RoomType);
//            Assert.AreEqual(room2.Description, room3.Description);
//            Assert.AreEqual(room2.Capacity, room3.Capacity);
//            Assert.AreEqual(room2.Available, room3.Available);
//            Assert.AreEqual(room2.Active, room3.Active);
//            Assert.AreEqual(room2.Price, room3.Price);
//            Assert.AreEqual(room2.RoomStatus, room3.RoomStatus);
//        }

//        [TestMethod]
//        public void TestCreateRoomRoomNumberGreaterThenFifteen()
//        {
//            // arrange
//            int roomNameLength = 16;
//            Room room = BuildNewRoom();
//            string newRoomNumber = CreateStringOfGivenLength(roomNameLength);
//            room.RoomNumber = newRoomNumber;

//            // assert
//            try
//            {
//                _roomManager.CreateRoom(room, 100000);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestCreateRoomRoomNumberIsEmpty()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            room.RoomNumber = "";

//            // assert
//            try
//            {
//                _roomManager.CreateRoom(room, 100000);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestCreateRoomBuildingNotOnList()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            room.Building = "Not A building";
//            // assert
//            try
//            {
//                _roomManager.CreateRoom(room, 100000);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestCreateRoomRoomRoomTypeNotOnList()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            room.RoomType = "Not A Room Type";
//            // assert
//            try
//            {
//                _roomManager.CreateRoom(room, 100000);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestCreateRoomDescriptionGreaterThenOneThousand()
//        {
//            // arrange
//            int roomDescriptionLength = 1001;
//            Room room = BuildNewRoom();
//            string newRoomDescription = CreateStringOfGivenLength(roomDescriptionLength);
//            room.Description = newRoomDescription;

//            // assert
//            try
//            {
//                _roomManager.CreateRoom(room, 100000);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestCreateRoomDescriptionEmpty()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            room.Description = "";

//            // assert
//            try
//            {
//                _roomManager.CreateRoom(room, 100000);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestCreateRoomPriceLessThenZero()
//        {
//            // arrange
//            decimal newPrice = -1.01M;
//            Room room = BuildNewRoom();
//            room.Price = newPrice;

//            // assert
//            try
//            {
//                _roomManager.CreateRoom(room, 100000);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestCreateRoomPriceIsZero()
//        {
//            // arrange
//            decimal newPrice = 0.0M;
//            Room room = BuildNewRoom();
//            room.Price = newPrice;

//            // assert
//            try
//            {
//                _roomManager.CreateRoom(room, 100000);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestCreateRoomStatusIDNotOnList()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            room.RoomStatus = "Not a status";
//            // assert
//            try
//            {
//                _roomManager.CreateRoom(room, 100000);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestRetrieveRoomStatusIDList()
//        {
//            // arrange
//            var RoomStatusID1 = CreateRoomStatusList();

//            // act
//            var RoomStatusID2 = _roomManager.RetrieveRoomStatusList();

//            // assert
//            for (int i = 0; i < RoomStatusID1.Count; i++)
//            {
//                Assert.AreEqual(RoomStatusID1[i], RoomStatusID2[i]);
//            }
//        }

//        [TestMethod]
//        public void TestRetrieveRoomByID()
//        {
//            // arrange
//            Room room1 = BuildNewRoom();
//            Room room2 = BuildNewRoom();

//            // act
//            _roomManager.CreateRoom(room1, 100000);
//            Room room3 = _roomManager.RetreieveRoomByID(room1.RoomID);

//            // assert
//            Assert.AreEqual(room1.RoomNumber, room3.RoomNumber);
//            Assert.AreEqual(room1.Building, room3.Building);
//            Assert.AreEqual(room1.RoomType, room3.RoomType);
//            Assert.AreEqual(room1.Description, room3.Description);
//            Assert.AreEqual(room1.Capacity, room3.Capacity);
//            Assert.AreEqual(room1.Available, room3.Available);
//            Assert.AreEqual(room1.Active, room3.Active);
//            Assert.AreEqual(room1.Price, room3.Price);
//            Assert.AreEqual(room1.ResortPropertyID, room3.ResortPropertyID);
//            Assert.AreEqual(room1.RoomStatus, room3.RoomStatus);
//            Assert.AreEqual(room1.OfferingID, room3.OfferingID);
//        }

//        [TestMethod]
//        public void TestRetrieveBuildingList()
//        {
//            // arrange
//            var buildingIDList1 = CreateBuildingIDList();

//            // act
//            var buildingIDList2 = _roomManager.RetrieveBuildingList();

//            // assert
//            for (int i = 0; i < buildingIDList1.Count; i++)
//            {
//                Assert.AreEqual(buildingIDList1[i], buildingIDList2[i]);
//            }

//        }

//        [TestMethod]
//        public void TestRetrieveRoomList()
//        {
//            // arrange
//            var roomList1 = CreateRooms();

//            // act
//            var roomList2 = _roomManager.RetrieveRoomList();

//            // assert
//            for (int i = 0; i < roomList1.Count; i++)
//            {
//                Assert.AreEqual(roomList1[i].RoomNumber, roomList2[i].RoomNumber);
//                Assert.AreEqual(roomList1[i].Building, roomList2[i].Building);
//                Assert.AreEqual(roomList1[i].RoomType, roomList2[i].RoomType);
//                Assert.AreEqual(roomList1[i].Description, roomList2[i].Description);
//                Assert.AreEqual(roomList1[i].Capacity, roomList2[i].Capacity);
//                Assert.AreEqual(roomList1[i].Available, roomList2[i].Available);
//                Assert.AreEqual(roomList1[i].Active, roomList2[i].Active);
//            }

//        }

//        [TestMethod]
//        public void TestRoomTypeList()
//        {
//            // arrange
//            var roomTypeIDList1 = CreateRoomTypeIDList();

//            // act
//            var roomTypeIDList2 = _roomManager.RetrieveRoomTypeList();

//            // assert
//            for (int i = 0; i < roomTypeIDList1.Count; i++)
//            {
//                Assert.AreEqual(roomTypeIDList1[i], roomTypeIDList2[i]);
//            }
//        }

//        [TestMethod]
//        public void TestUpdateRoomAllValid()
//        {
//            // arrange
//            Room room1;
//            Room room2;
//            room1 = _roomManager.RetreieveRoomByID(100000);
//            room2 = new Room()
//            {
//                RoomID = room1.RoomID,
//                RoomNumber = room1.RoomNumber,
//                Building = room1.Building,
//                RoomType = room1.RoomType,
//                Description = room1.Description,
//                Capacity = room1.Capacity,
//                Available = room1.Available,
//                Price = room1.Price,
//                Active = room1.Active,
//                OfferingID = room1.OfferingID,
//                RoomStatus = room1.RoomStatus,
//                ResortPropertyID = room1.ResortPropertyID
//            };

//            // act
//            room2.RoomType = "Test Room Type 4";
//            room2.Capacity = 6;
//            bool results = _roomManager.UpdateRoom(room2);
//            var room3 = _roomManager.RetreieveRoomByID(room2.RoomID);

//            // assert
//            Assert.AreEqual(room2.RoomNumber, room3.RoomNumber);
//            Assert.AreEqual(room2.Building, room3.Building);
//            Assert.AreEqual(room2.RoomType, room3.RoomType);
//            Assert.AreEqual(room2.Description, room3.Description);
//            Assert.AreEqual(room2.Capacity, room3.Capacity);
//            Assert.AreEqual(room2.Available, room3.Available);
//            Assert.AreEqual(room2.Price, room3.Price);
//            Assert.AreEqual(room2.Active, room3.Active);
//            Assert.AreEqual(room2.OfferingID, room3.OfferingID);
//            Assert.AreEqual(room2.RoomStatus, room3.RoomStatus);
//            Assert.AreEqual(room2.ResortPropertyID, room3.ResortPropertyID);

//        }

//        [TestMethod]
//        public void TestUpdateRoomRoomNumberGreaterThenFifteen()
//        {
//            // arrange
//            int roomNameLength = 16;
//            Room room = BuildNewRoom();
//            _roomManager.CreateRoom(room, 100000);
//            string newRoomNumber = CreateStringOfGivenLength(roomNameLength);
//            room.RoomNumber = newRoomNumber;

//            // assert
//            try
//            {
//                _roomManager.UpdateRoom(room);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestUpdateRoomRoomNumberIsEmpty()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            _roomManager.CreateRoom(room, 100000);
//            room.RoomNumber = "";

//            // assert
//            try
//            {
//                _roomManager.UpdateRoom(room);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestUpdateRoomBuildingNotOnList()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            _roomManager.CreateRoom(room, 100000);
//            room.Building = "Not a Buidling";

//            // assert
//            try
//            {
//                _roomManager.UpdateRoom(room);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestUpdateRoomRoomRoomTypeNotOnList()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            _roomManager.CreateRoom(room, 100000);
//            room.RoomType = "Not a room type";

//            // assert
//            try
//            {
//                _roomManager.UpdateRoom(room);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestUpdateRoomDescriptionGreaterThenOneThousand()
//        {
//            // arrange
//            int roomDescriptionLength = 1001;
//            Room room = BuildNewRoom();
//            _roomManager.CreateRoom(room, 100000);
//            string newRoomDescription = CreateStringOfGivenLength(roomDescriptionLength);
//            room.Description = newRoomDescription;

//            // assert
//            try
//            {
//                _roomManager.UpdateRoom(room);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestUpdateRoomDescriptionEmpty()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            _roomManager.CreateRoom(room, 100000);
//            room.Description = "";

//            // assert
//            try
//            {
//                _roomManager.UpdateRoom(room);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestUpdateRoomPriceLessThenZero()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            _roomManager.CreateRoom(room, 100000);
//            room.Price = -1.0M;

//            // assert
//            try
//            {
//                _roomManager.UpdateRoom(room);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestUpdateRoomPriceIsZero()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            _roomManager.CreateRoom(room, 100000);
//            room.Price = 0.0M;

//            // assert
//            try
//            {
//                _roomManager.UpdateRoom(room);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestUpdateRoomStatusIDNotOnList()
//        {
//            // arrange
//            Room room = BuildNewRoom();
//            _roomManager.CreateRoom(room, 100000);
//            room.RoomStatus = "Not a Status";

//            // assert
//            try
//            {
//                _roomManager.UpdateRoom(room);
//                Assert.Fail();
//            }
//            catch (ApplicationException)
//            {
//                Assert.IsTrue(true);
//            }
//            catch (Exception)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestDeleteRoom()
//        {
//            // arrange
//            int idToCheck = 100002;
//            var room = _roomManager.RetreieveRoomByID(idToCheck);


//            // act
//            bool results = _roomManager.DeleteRoom(room);

//            // assert
//            var roomList = _roomManager.RetrieveRoomList();
//            var room2 = roomList.Find(r => r.RoomID == idToCheck);

//            Assert.IsTrue(results && room2 == null);
//        }

//        [TestMethod]
//        public void TestDeleteRoomByID()
//        {
//            // arrange
//            int idToCheck = 100005;
//            var room = _roomManager.RetreieveRoomByID(idToCheck);

//            // act
//            bool results = _roomManager.DeleteRoomByID(room.RoomID);

//            // assert
//            var roomList = _roomManager.RetrieveRoomList();
//            var room2 = roomList.Find(r => r.RoomID == idToCheck);

//            Assert.IsTrue(results && room2 == null);

//        }
//        private Room BuildNewRoom()
//        {
//            Room room = new Room()
//            {
//                RoomNumber = "201",
//                Building = "Test Building 1",
//                RoomType = "Test Room Type 1",
//                Description = "Room 201 in Building 1",
//                Capacity = 4,
//                Available = true,
//                Price = 200.00M,
//                Active = true,
//                OfferingID = 100002,
//                RoomStatus = "Ready",
//                ResortPropertyID = 100003
//            };
//            return room;
//        }
//        private List<string> CreateBuildingIDList()
//        {
//            var buildingList = new List<string>();
//            for (int i = 1; i < 5; i++)
//            {
//                buildingList.Add("Test Building " + i);
//            }
//            return buildingList;
//        }
//        private List<string> CreateRoomTypeIDList()
//        {

//            var roomTypeList = new List<string>();
//            for (int i = 1; i < 5; i++)
//            {
//                roomTypeList.Add("Test Room Type " + i);
//            }
//            return roomTypeList;
//        }
//        private List<Room> CreateRooms()
//        {
//            int nextRoomID = 100000;
//            List<Room> roomList = new List<Room>();
//            Room room0 = new Room
//            {
//                RoomID = nextRoomID,
//                RoomNumber = "101",
//                Building = "Test Building 1",
//                RoomType = "Test Room Type 1",
//                Description = "Test Room " + nextRoomID,
//                Capacity = 2,
//                Available = true,
//                Price = 200.00M,
//                Active = true,
//                OfferingID = 100002,
//                RoomStatus = "Ready",
//                ResortPropertyID = 100003
//            };
//            roomList.Add(room0);
//            nextRoomID++;
//            Room room1 = new Room
//            {
//                RoomID = nextRoomID,
//                RoomNumber = "101",
//                Building = "Test Building 2",
//                RoomType = "Test Room Type 2",
//                Description = "Test Room " + nextRoomID,
//                Capacity = 2,
//                Available = true,
//                Price = 200.00M,
//                Active = false,
//                OfferingID = 100002,
//                RoomStatus = "Ready",
//                ResortPropertyID = 100003
//            };
//            roomList.Add(room1);
//            nextRoomID++;
//            Room room2 = new Room
//            {
//                RoomID = nextRoomID,
//                RoomNumber = "101",
//                Building = "Test Building 3",
//                RoomType = "Test Room Type 3",
//                Description = "Test Room " + nextRoomID,
//                Capacity = 2,
//                Available = true,
//                Price = 200.00M,
//                Active = true,
//                OfferingID = 100002,
//                RoomStatus = "Occupied",
//                ResortPropertyID = 100003
//            };
//            roomList.Add(room2);
//            nextRoomID++;
//            Room room3 = new Room
//            {
//                RoomID = nextRoomID,
//                RoomNumber = "101",
//                Building = "Test Building 4",
//                RoomType = "Test Room Type 4",
//                Description = "Test Room " + nextRoomID,
//                Capacity = 2,
//                Available = true,
//                Price = 200.00M,
//                Active = true,
//                OfferingID = 100002,
//                RoomStatus = "Needs Cleaning",
//                ResortPropertyID = 100003
//            };
//            roomList.Add(room3);
//            nextRoomID++;
//            Room room4 = new Room
//            {
//                RoomID = nextRoomID,
//                RoomNumber = "102",
//                Building = "Test Building 1",
//                RoomType = "Test Room Type 1",
//                Description = "Test Room " + nextRoomID,
//                Capacity = 2,
//                Available = true,
//                Price = 200.00M,
//                Active = true,
//                OfferingID = 100002,
//                RoomStatus = "Murder Scene",
//                ResortPropertyID = 100003
//            };
//            roomList.Add(room4);
//            nextRoomID++;
//            Room room5 = new Room
//            {
//                RoomID = nextRoomID,
//                RoomNumber = "103",
//                Building = "Test Building 3",
//                RoomType = "Test Room Type 2",
//                Description = "Test Room " + nextRoomID,
//                Capacity = 2,
//                Available = false,
//                Price = 200.00M,
//                Active = true,
//                OfferingID = 100002,
//                RoomStatus = "Needs Fumigation",
//                ResortPropertyID = 100003
//            };
//            roomList.Add(room5);
//            nextRoomID++;
//            Room room6 = new Room
//            {
//                RoomID = nextRoomID,
//                RoomNumber = "104",
//                Building = "Test Building 4",
//                RoomType = "Test Room Type 2",
//                Description = "Test Room " + nextRoomID,
//                Capacity = 2,
//                Available = true,
//                Price = 200.00M,
//                Active = true,
//                OfferingID = 100002,
//                RoomStatus = "Needs Inspection",
//                ResortPropertyID = 100003
//            };
//            roomList.Add(room6);
//            nextRoomID++;
//            Room room7 = new Room
//            {
//                RoomID = nextRoomID,
//                RoomNumber = "101",
//                Building = "Test Building 3",
//                RoomType = "Test Room Type 3",
//                Description = "Test Room " + nextRoomID,
//                Capacity = 2,
//                Available = true,
//                Price = 200.00M,
//                Active = true,
//                OfferingID = 100002,
//                RoomStatus = "Jim Quote Needed",
//                ResortPropertyID = 100003
//            };
//            roomList.Add(room7);
//            nextRoomID++;
//            return roomList;
//        }
//        private string CreateStringOfGivenLength(int length)
//        {
//            string newString = new string('*', length);

//            return newString;
//        }
//        private List<string> CreateRoomStatusList()
//        {
//            List<string> roomStatusList = new List<string>();
//            roomStatusList = new List<string>();
//            roomStatusList.Add("Ready");
//            roomStatusList.Add("Occupied");
//            roomStatusList.Add("Needs Cleaning");
//            roomStatusList.Add("Murder Scene");
//            roomStatusList.Add("After Rock Star Cleaning");
//            roomStatusList.Add("Needs Fumigation");
//            roomStatusList.Add("Needs Inspection");
//            roomStatusList.Add("Jim Quote Needed");

//            return roomStatusList;
//        }
//    }
//}
