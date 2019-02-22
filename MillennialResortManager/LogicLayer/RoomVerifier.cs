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
        private static List<int> offeringIDList;
        private static List<int> propertyIDList;
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
                offeringIDList = _roomAccessor.SelectOfferingIDList();
                propertyIDList = _roomAccessor.SelectResortProperyIDList();

            }
            catch (Exception)
            {

                throw;
            }

            CheckRoomNumber();
            CheckBuilding();
            CheckRoomType();
            CheckDescription();
            CheckCapacity();
            CheckPrice();
            CheckOfferingID();
            CheckRoomStatusID();
            CheckResortPropertyID();
            return roomIsGood;
        }
        // string 15 char
        public static void CheckRoomNumber()
        {
            if(roomToCheck.RoomNumber.Length <= 15 && roomToCheck.RoomNumber != "")
            {
                roomIsGood = true;
            }
            else
            {
                roomIsGood = false;
                throw new ApplicationException("Room number should be 1 to 15 characters in length.");
            }
        }
        // matches a room in the list
        public static void CheckBuilding()
        {
            foreach (var building in buildingsList)
            {
                if(building == roomToCheck.Building)
                {
                    roomIsGood = true;
                    break;
                }
                else
                {
                    roomIsGood = false;
                }
            }
            if(roomIsGood == false)
            {
                throw new ApplicationException("The building you have enter is not valid");
            }
        }
        // matches a type in the list
        public static void CheckRoomType()
        {
            foreach (var roomType in roomTypesList)
            {
                if(roomType == roomToCheck.RoomType)
                {
                    roomIsGood = true;
                    break;
                }
                else
                {
                    roomIsGood = false;
                }
            }
            if(roomIsGood == false)
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
            if(roomToCheck.Capacity < 1)
            {
                throw new ApplicationException("A room should hold at least one person");
            }
        }
        // above 0
        public static void CheckPrice()
        {
            if(roomToCheck.Price <= 0.0M)
            {
                throw new ApplicationException("Room price cannot be zero or less");
            }
        }
        // matches OfferingID in the list
        public static void CheckOfferingID()
        {
            foreach (var offeringID in offeringIDList)
            {
                if(roomToCheck.OfferingID == offeringID)
                {
                    roomIsGood = true;
                    break;
                }
                else
                {
                    roomIsGood = false;
                }
            }
            if(roomIsGood == false)
            {
                throw new ApplicationException("Offering ID is not valid");
            }
        }
        // matches a status in the list
        public static void CheckRoomStatusID()
        {
            foreach (var statusID in statusIDList)
            {
                if(roomToCheck.RoomStatus == statusID)
                {
                    roomIsGood = true;
                    break;
                }
                else
                {
                    roomIsGood = false;
                }
            }
            if(roomIsGood == false)
            {
                throw new ApplicationException("Room status is not valid");
            }
        }
        // matches a ProperyID in the list
        public static void CheckResortPropertyID()
        {
            foreach (var propertyID in propertyIDList)
            {
                if(roomToCheck.ResortPropertyID == propertyID)
                {
                    roomIsGood = true;
                    break;
                }
                else
                {
                    roomIsGood = false;
                }
            }
            if(roomIsGood == false)
            {
                throw new ApplicationException("The resort property you have entered is not valid");
            }
        }
    }
}
