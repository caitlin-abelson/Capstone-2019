using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 3/27/2019
    /// This is a mock Data accessor which implements the IHouseKeepingRequestAccessor interface.
    /// </summary>
    public class HouseKeepingRequestAccessorMock : IHouseKeepingRequestAccessor
    {
        private List<HouseKeepingRequest> _houseKeepingRequests;

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// This constructor sets up all of our dummy data we will be using
        /// </summary>
        public HouseKeepingRequestAccessorMock()
        {
            _houseKeepingRequests = new List<HouseKeepingRequest>();
            _houseKeepingRequests.Add(new HouseKeepingRequest() { HouseKeepingRequestID = 100000, BuildingNumber = 1, RoomNumber = 1, Description = "Test Description Number 1", WorkingEmployeeID = 100001, Active = true});
            _houseKeepingRequests.Add(new HouseKeepingRequest() { HouseKeepingRequestID = 100001, BuildingNumber = 2, RoomNumber = 2, Description = "Test Description Number 2", WorkingEmployeeID = 100002, Active = true});
            _houseKeepingRequests.Add(new HouseKeepingRequest() { HouseKeepingRequestID = 100002, BuildingNumber = 3, RoomNumber = 3, Description = "Test Description Number 3", WorkingEmployeeID = 100003, Active = true });
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// This will create a request using the data provided in the newRequest
        /// </summary>
        public void CreateHouseKeepingRequest(HouseKeepingRequest newHouseKeepingRequest)
        {
            _houseKeepingRequests.Add(newHouseKeepingRequest);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// This will search for a request with a matching request ID from our mock data
        /// </summary>
        public HouseKeepingRequest RetrieveHouseKeepingRequest(int HouseKeepingRequestID)
        {
            HouseKeepingRequest h = new HouseKeepingRequest();
            h = _houseKeepingRequests.Find(x => x.HouseKeepingRequestID == HouseKeepingRequestID);
            if (h == null)
            {
                throw new ArgumentException("HouseKeepingRequestID did not match any HouseKeepingRequest in our System");
            }

            return h;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// This will simply return a list with all of our request data
        /// </summary>
        public List<HouseKeepingRequest> RetrieveAllHouseKeepingRequests()
        {
            return _houseKeepingRequests;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// This will update an existing housekeeping request or throw a new ArgumentException
        /// </summary>
        public void UpdateHouseKeepingRequest(HouseKeepingRequest oldHouseKeepingRequest, HouseKeepingRequest newHouseKeepingRequest)
        {
            bool updated = false;
            foreach (var hr in _houseKeepingRequests)
            {
                if (hr.HouseKeepingRequestID == oldHouseKeepingRequest.HouseKeepingRequestID)
                {
                    hr.BuildingNumber = newHouseKeepingRequest.BuildingNumber;
                    hr.RoomNumber = newHouseKeepingRequest.RoomNumber;
                    hr.WorkingEmployeeID = newHouseKeepingRequest.WorkingEmployeeID;
                    hr.Description = newHouseKeepingRequest.Description;
                    hr.Active = newHouseKeepingRequest.Active;
                    updated = true;
                    break;
                }
            }
            if (!updated)
            {
                throw new ArgumentException("No House Keeping Request was found to update");
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// This will deactivate the request which has a matching ID to the one that was provided 
        /// </summary>
        public void DeactivateHouseKeepingRequest(int HouseKeepingRequestID)
        {
            bool foundHouseKeepingRequest = false;
            foreach (var hr in _houseKeepingRequests)
            {
                if (hr.HouseKeepingRequestID == HouseKeepingRequestID)
                {
                    hr.Active = false;
                    foundHouseKeepingRequest = true;
                    break;
                }
            }
            if (!foundHouseKeepingRequest)
            {
                throw new ArgumentException("No HouseKeepingRequest was found with that ID");
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// This will completely purge a HouseKeepingRequest from our system or throw an exception if we cannot find it
        public void PurgeHouseKeepingRequest(int HouseKeepingRequestID)
        {
            try
            {
                RetrieveHouseKeepingRequest(HouseKeepingRequestID);
            }
            catch (Exception)
            {
                throw new ArgumentException("No Work Order was found with that ID");
            }
            _houseKeepingRequests.Remove(_houseKeepingRequests.Find(x => x.HouseKeepingRequestID == HouseKeepingRequestID));
        }
    }
}
