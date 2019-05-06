using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Austin Berquam" created="2019/02/06">
	/// Interface that implements Create and Delete functions for Room Types
	/// for manager classes.
	/// </summary>
	public interface IRoomType
	{
		/// <summary author="Austin Berquam" created="2019/02/06">
		/// Creates a new room type
		/// </summary>
		bool CreateRoomType(RoomType roomType);
        List<RoomType> RetrieveAllRoomTypes(string status);
		/// <summary author="Austin Berquam" created="2019/02/06">
		/// Deletes a room type
		/// </summary>
		bool DeleteRoomType(string roomTypeID);
        List<string> RetrieveAllRoomTypes();
    }
}