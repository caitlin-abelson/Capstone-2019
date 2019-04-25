/// <summary>
/// Wes Richardson
/// Created: 2019/02/14
/// 
/// A class to check if room data being sent fits with the database type and length
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public static class RoomVerifier
    {
        private static bool roomIsGood = false;
        private static IRoomAccessor _roomAccessor;
        private static List<string> buildingsList;
        private static List<string> roomTypesList;
        private static List<string> statusIDList;
        private static Room roomToCheck;
        public static bool VerifyRoom(Room room, IRoomAccessor roomAccessor)
        {
            _roomAccessor = roomAccessor;
            roomToCheck = room;
            try
            {
                buildingsList = _roomAccessor.SelectBuildings();
                roomTypesList = _roomAccessor.SelectRoomTypes();
                statusIDList = _roomAccessor.SelectRoomStatusList();

            }
            catch (Exception)
            {

                throw;
            }

            CheckBuilding();
            CheckRoomType();
            CheckDescription();
            CheckCapacity();
            CheckPrice();
            CheckRoomStatusID();
            return roomIsGood;
        }


        // matches a room in the list
        public static void CheckBuilding()
        {
            foreach (var building in buildingsList)
            {
                if (building == roomToCheck.Building)
                {
                    roomIsGood = true;
                    break;
                }
                else
                {
                    roomIsGood = false;
                }
            }
            if (roomIsGood == false)
            {
                throw new ApplicationException("The building you have enter is not valid");
            }
        }
        // matches a type in the list
        public static void CheckRoomType()
        {
            foreach (var roomType in roomTypesList)
            {
                if (roomType == roomToCheck.RoomType)
                {
                    roomIsGood = true;
                    break;
                }
                else
                {
                    roomIsGood = false;
                }
            }
            if (roomIsGood == false)
            {
                throw new ApplicationException("The room type you have entered is not valid");
            }
        }
        // string 1000 char
        public static void CheckDescription()
        {
            if (roomToCheck.Description.Length <= 1000 && roomToCheck.Description != "")
            {
                roomIsGood = true;
            }
            else
            {
                roomIsGood = false;
                throw new ApplicationException("Room description should be 1 to 1000 characters in length");
            }
        }
        // minuim of 1
        public static void CheckCapacity()
        {
            if (roomToCheck.Capacity < 1)
            {
                throw new ApplicationException("A room should hold at least one person");
            }
        }
        // above 0
        public static void CheckPrice()
        {
            if (roomToCheck.Price <= 0.0M)
            {
                throw new ApplicationException("Room price cannot be zero or less");
            }
        }
        // matches a status in the list
        public static void CheckRoomStatusID()
        {
            foreach (var statusID in statusIDList)
            {
                if (roomToCheck.RoomStatus == statusID)
                {
                    roomIsGood = true;
                    break;
                }
                else
                {
                    roomIsGood = false;
                }
            }
            if (roomIsGood == false)
            {
                throw new ApplicationException("Room status is not valid");
            }
        }
    }
}
