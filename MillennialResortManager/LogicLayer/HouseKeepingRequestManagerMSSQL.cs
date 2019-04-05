using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 3/27/2019
    /// HouseKeepingRequestManagerMSSQL Is an implementation of the IHouseKeepingRequestManager Interface meant to interact with the MSSQL HouseKeepingRequestAccessor
    /// </summary>
    public class HouseKeepingRequestManagerMSSQL : IHouseKeepingRequestManager
    {
        private IHouseKeepingRequestAccessor _houseKeepingRequestAccessor;
        /// <summary>
        /// Constructor which allows us to implement the HouseKeepingRequest Accessor methods
        /// </summary>
        public HouseKeepingRequestManagerMSSQL()
        {
            _houseKeepingRequestAccessor = new HouseKeepingRequestAccessorMSSQL();
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Constructor which allows us to implement which ever HouseKeepingRequest Accessor we need to use
        /// </summary>
        public HouseKeepingRequestManagerMSSQL(HouseKeepingRequestAccessorMock houseKeepingRequestAccessor)
        {
            _houseKeepingRequestAccessor = houseKeepingRequestAccessor;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Passes along a HouseKeepingRequest object to our HouseKeepingRequestAccessorMSSQL to be stored in our database
        public void AddHouseKeepingRequest(HouseKeepingRequest newHouseKeepingRequest)
        {
            try
            {
                //Double Check the HouseKeepingRequest is Valid
                if (!newHouseKeepingRequest.IsValid())
                {
                    throw new ArgumentException("Data for this HouseKeepingRequest is not valid");
                }
                _houseKeepingRequestAccessor.CreateHouseKeepingRequest(newHouseKeepingRequest);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Delete HouseKeepingRequest will determine whether the HouseKeepingRequest needs to be deleted or deactivated and request deactivation or deletion from a HouseKeepingRequest Accessor
        /// </summary>
        public void DeleteHouseKeepingRequest(int HouseKeepingRequestID, bool isActive)
        {
            if (isActive)
            {
                //Is Active so we just deactivate it
                try
                {
                    _houseKeepingRequestAccessor.DeactivateHouseKeepingRequest(HouseKeepingRequestID);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                //Is Deactive so we purge it
                try
                {
                    _houseKeepingRequestAccessor.PurgeHouseKeepingRequest(HouseKeepingRequestID);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Sends Existing HouseKeepingRequest data along with the new HouseKeepingRequest data to HouseKeepingRequestAccessor. Returns an error if update fails 
        /// </summary>
        public void EditHouseKeepingRequest(HouseKeepingRequest oldHouseKeepingRequest, HouseKeepingRequest newHouseKeepingRequest)
        {
            try
            {
                if (!newHouseKeepingRequest.IsValid())
                {
                    throw new ArgumentException("Data for this new HouseKeepingRequest is not valid");
                }
                _houseKeepingRequestAccessor.UpdateHouseKeepingRequest(oldHouseKeepingRequest, newHouseKeepingRequest);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Retrieves all the HouseKeepingRequests in our system from a HouseKeepingRequestAccessor or an error if there was a problem
        /// </summary>
        public List<HouseKeepingRequest> RetrieveAllHouseKeepingRequests()
        {
            List<HouseKeepingRequest> houseKeepingRequests = new List<HouseKeepingRequest>();
            try
            {
                houseKeepingRequests = _houseKeepingRequestAccessor.RetrieveAllHouseKeepingRequests();
            }
            catch (Exception)
            {
                throw;
            }
            return houseKeepingRequests;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Returns a HouseKeepingRequest from a HouseKeepingRequestAccessor or throws an error if there was a problem
        /// </summary>
        public HouseKeepingRequest RetrieveHouseKeepingRequest(int HouseKeepingRequestID)
        {
            HouseKeepingRequest houseKeepingRequest = new HouseKeepingRequest();
            try
            {
                houseKeepingRequest = _houseKeepingRequestAccessor.RetrieveHouseKeepingRequest(HouseKeepingRequestID);
            }
            catch (Exception)
            {
                throw new ArgumentException("HouseKeepingRequestID did not match any HouseKeepingRequests in our System");
            }
            return houseKeepingRequest;

        }
    }
}
