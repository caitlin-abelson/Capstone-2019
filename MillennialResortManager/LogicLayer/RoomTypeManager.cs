using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Austin Berquam
    /// Created: 2019/02/06
    /// 
    /// Used to manage the Room Type table
    /// and the stored procedures as well
    /// </summary
    public class RoomTypeManager : IRoomType
    {
        IRoomTypeAccessor roomTypeAccessor;

        public RoomTypeManager()
        {
            roomTypeAccessor = new RoomTypeAccessor();
        }
        public RoomTypeManager(MockRoomTypeAccessor mock)
        {
            roomTypeAccessor = new MockRoomTypeAccessor();
        }
        /// <summary>
        /// Method that collects the RoomType from the accessor
        /// </summary>
        /// <returns> List of GuestTypes </returns>
        public List<RoomType> RetrieveAllRoomTypes(string status)
        {
            List<RoomType> types = null;
            if (status != "")
            {
                try
                {
                    types = roomTypeAccessor.SelectRoomTypes(status);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return types;
        }

        /// <summary>
        /// Method that creates a guestType through the accessor
        /// </summary>
        /// <param name="roomType">Object roomtype to create</param>
        /// <returns> bool on if the room was created </returns>
        public bool CreateRoomType(RoomType roomType)
        {
            ValidationExtensionMethods.ValidateID(roomType.RoomTypeID);
            ValidationExtensionMethods.ValidateDescription(roomType.Description);
            bool result = false;

            try
            {
                result = (1 == roomTypeAccessor.InsertRoomType(roomType));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Method that deletes a guestType through the accessor
        /// </summary>
        /// <param name="roomTypeID">ID roomtype to delete</param>
        /// <returns> bool on if the room was deleted </returns>
        public bool DeleteRoomType(string roomTypeID)
        {
            bool result = false;

            try
            {
                result = (1 == roomTypeAccessor.DeleteRoomType(roomTypeID));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Method that retrieves all room types and stores it in a list
        /// </summary>
        /// <returns> RoomTypes in a List returns>
        public List<string> RetrieveAllRoomTypes()
        {
            List<string> types = null;
            try
            {
                types = roomTypeAccessor.SelectAllTypes();
            }
            catch (Exception)
            {
                throw;
            }
            return types;
        }
    }
}
