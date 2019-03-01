/// <summary>
/// Austin Berquam
/// Created: 2019/02/06
/// 
/// Interface that implements Create and Delete functions for Guest Types
/// for manager classes.
/// </summary>

using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IGuestTypeManager
    {

        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Creates a new guest type
        /// </summary>
        bool CreateGuestType(GuestType guestType);
        List<string> RetrieveAllGuestTypes();

        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Deletes a guest type
        /// </summary>
        bool DeleteGuestType(string guestTypeID);
        List<GuestType> RetrieveAllGuestTypes(string status);
    }
}