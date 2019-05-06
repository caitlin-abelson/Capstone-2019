using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="Dalton Cleveland" created="2019/03/27">
	/// IHouseKeepingRequestManager is an interface meant to be the standard for interacting with HouseKeepingRequest in a storage medium
	/// </summary>
	public interface IHouseKeepingRequestManager
    {
        void AddHouseKeepingRequest(HouseKeepingRequest newHouseKeepingRequest);
        void EditHouseKeepingRequest(HouseKeepingRequest oldHouseKeepingRequest, HouseKeepingRequest newHouseKeepingRequest);
        HouseKeepingRequest RetrieveHouseKeepingRequest(int HouseKeepingRequestID);
        List<HouseKeepingRequest> RetrieveAllHouseKeepingRequests();
        void DeleteHouseKeepingRequest(int HouseKeepingRequestID, bool isActive);
    }
}