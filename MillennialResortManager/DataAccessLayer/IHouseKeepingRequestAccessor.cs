using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 3/27/2019
    /// The IHouseKeepingRequestAccessor is an interface meant to be the standard for interacting with Data 
    /// in a storage medium regarding HouseKeepingRequests
    /// </summary>
    public interface IHouseKeepingRequestAccessor
    {
        void CreateHouseKeepingRequest(HouseKeepingRequest newHouseKeepingRequest);
        HouseKeepingRequest RetrieveHouseKeepingRequest(int HouseKeepingRequestID);
        List<HouseKeepingRequest> RetrieveAllHouseKeepingRequests();
        void UpdateHouseKeepingRequest(HouseKeepingRequest oldHouseKeepingRequest, HouseKeepingRequest newHouseKeepingRequest);
        void DeactivateHouseKeepingRequest(int HouseKeepingRequestID);
        void PurgeHouseKeepingRequest(int HouseKeepingRequestID);

    }
}
