using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Resort Vehicle Status Manager
    /// </summary>
    public class ResortVehicleStatusManager : IResortVehicleStatusManager
    {
        private readonly IResortVehicleStatusAccessor _resortVehicleStatusAccessor;

        public ResortVehicleStatusManager(IResortVehicleStatusAccessor resortVehicleStatusAccessor)
        {
            _resortVehicleStatusAccessor = resortVehicleStatusAccessor;
        }

        public ResortVehicleStatusManager() : this(new ResortVehicleStatusAccessor()) { }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Adds Resort Vehicle Status to database
        /// </summary>
        /// <param name="status">Resort Vehicle Status</param>
        /// <returns>New Resort Vehicle Status Id</returns>
        public int AddResortVehicleStatus(ResortVehicleStatus status)
        {
            int statusId;

            try
            {
                this.MeetsValidationCriteria(status, GetResortVehicleValidationCriteria());

                statusId = _resortVehicleStatusAccessor.AddResortVehicleStatus(status);
            }
            catch (Exception)
            {
                throw;
            }

            return statusId;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Retrieves Resort Vehicle Status by id
        /// </summary>
        /// <param name="id">Resort Vehicle Status Id</param>
        /// <returns>Resort Vehicle Status Object</returns>
        public ResortVehicleStatus RetrieveResortVehicleStatusById(string id)
        {
            ResortVehicleStatus resortVehicleStatus;

            try
            {
                resortVehicleStatus = _resortVehicleStatusAccessor.RetrieveResortVehicleStatusById(id);
            }
            catch (Exception)
            {
                throw;
            }

            return resortVehicleStatus;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Retrieves all Resort Vehicle Statuses
        /// </summary>
        /// <returns>Resort Vehicle Status Collection</returns>
        public IEnumerable<ResortVehicleStatus> RetrieveResortVehicleStatuses()
        {
            IEnumerable<ResortVehicleStatus> resortVehicleStatuses;

            try
            {
                resortVehicleStatuses = _resortVehicleStatusAccessor.RetrieveResortVehicleStatuses();
            }
            catch (Exception)
            {
                throw;
            }

            return resortVehicleStatuses;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Updates Resort Vehicle Status
        /// </summary>
        /// <param name="oldStatus">Old Vehicle Status (database copy)</param>
        /// <param name="newStatus">New Vehicle Status (new copy)</param>
        public void UpdateResortVehicleStatus(ResortVehicleStatus oldStatus, ResortVehicleStatus newStatus)
        {
            try
            {
                this.MeetsValidationCriteria(newStatus, GetResortVehicleValidationCriteria());

                _resortVehicleStatusAccessor.UpdateResortVehicleStatus(oldStatus, newStatus);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Deletes Resort Vehicle Status from database
        /// </summary>
        /// <param name="id">Resort Vehicle Status Id</param>
        public void DeleteResortVehicleStatus(string id)
        {
            try
            {
                _resortVehicleStatusAccessor.DeleteResortVehicleStatus(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Validation Rules for Resort Vehicle Status
        /// </summary>
        /// <returns>Dictionary containing validation rules</returns>
        private Dictionary<string, ValidationCriteria> GetResortVehicleValidationCriteria()
        {
            return new Dictionary<string, ValidationCriteria>
            {
                {
                    nameof(ResortVehicleStatus.Id),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = int.MaxValue}
                },
                {
                    nameof(ResortVehicleStatus.Description),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = 1000}
                }
            };
        }
    }
}