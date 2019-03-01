/// <summary>
/// Austin Berquam
/// Created: 2019/02/06
/// 
/// Interface that implements Create and Delete functions for Guest Types
/// for accessor classes.
/// </summary>
using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IGuestTypeAccessor
    {

        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Creates a new guest type
        /// </summary>
        List<GuestType> SelectGuestTypes(string status);
        int InsertGuestType(GuestType guestType);

        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Deletes a guest type
        /// </summary>
        List<string> SelectAllTypes();
        int DeleteGuestType(string guestTypeID);
    }
}