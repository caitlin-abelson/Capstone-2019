using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Dalton Cleveland" created="2019/03/27">
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

		/// <summary author="Dalton Cleveland" created="2019/03/27">
		/// Constructor which allows us to implement which ever HouseKeepingRequest Accessor we need to use
		/// </summary>
		public HouseKeepingRequestManagerMSSQL(HouseKeepingRequestAccessorMock houseKeepingRequestAccessor)
        {
            _houseKeepingRequestAccessor = houseKeepingRequestAccessor;
        }

		/// <summary author="Dalton Cleveland" created="2019/03/27">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Dalton Cleveland" created="2019/03/27">
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
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}
            else
            {
                //Is Deactive so we purge it
                try
                {
                    _houseKeepingRequestAccessor.PurgeHouseKeepingRequest(HouseKeepingRequestID);
                }
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}
        }

		/// <summary author="Dalton Cleveland" created="2019/03/27">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Dalton Cleveland" created="2019/03/27">
		/// Retrieves all the HouseKeepingRequests in our system from a HouseKeepingRequestAccessor or an error if there was a problem
		/// </summary>
		public List<HouseKeepingRequest> RetrieveAllHouseKeepingRequests()
        {
            List<HouseKeepingRequest> houseKeepingRequests = new List<HouseKeepingRequest>();
            try
            {
                houseKeepingRequests = _houseKeepingRequestAccessor.RetrieveAllHouseKeepingRequests();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return houseKeepingRequests;
        }

		/// <summary author="Dalton Cleveland" created="2019/03/27">
		/// Returns a HouseKeepingRequest from a HouseKeepingRequestAccessor or throws an error if there was a problem
		/// </summary>
		public HouseKeepingRequest RetrieveHouseKeepingRequest(int HouseKeepingRequestID)
        {
            HouseKeepingRequest houseKeepingRequest = new HouseKeepingRequest();

            try
            {
                houseKeepingRequest = _houseKeepingRequestAccessor.RetrieveHouseKeepingRequest(HouseKeepingRequestID);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw new ArgumentException("HouseKeepingRequestID did not match any HouseKeepingRequests in our System");
            }

            return houseKeepingRequest;
        }
    }
}