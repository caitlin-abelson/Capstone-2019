using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Resort Vehicle Checkout Manager
    /// </summary>
    public class ResortVehicleCheckoutManager : IResortVehicleCheckoutManager
    {
        private readonly IResortVehicleCheckoutAccessor _resortVehicleCheckoutAccessor;
        private readonly IResortVehicleAccessor _resortVehicleAccessor;
		AppData.DataStoreType daotype;

        public ResortVehicleCheckoutManager(IResortVehicleCheckoutAccessor resortVehicleCheckoutAccessor = null
                                            , IResortVehicleAccessor resortVehicleAccessor = null)
        {
			daotype = AppData.DataStoreType.msssql;

			switch (daotype)
			{
				case AppData.DataStoreType.mock:
					throw new NotImplementedException();
					break;
				case AppData.DataStoreType.msssql:
				default:
					_resortVehicleCheckoutAccessor = new ResortVehicleCheckoutAccessor();
					_resortVehicleAccessor = new ResortVehicleAccessor();
					break;
			}
        }

        public ResortVehicleCheckoutManager()
		{
			_resortVehicleCheckoutAccessor = new ResortVehicleCheckoutAccessor();
			_resortVehicleAccessor = new ResortVehicleAccessor();
		}

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Adds vehicle checkout to database
        /// </summary>
        /// <param name="checkout">vehicle checkout</param>
        /// <returns>id to new vehicle checkout</returns>
        public int AddVehicleCheckout(ResortVehicleCheckout checkout)
        {
            int checkoutId;

            try
            {
                this.MeetsValidationCriteria(checkout, GetResortVehicleValidationCriteria());

                checkoutId = _resortVehicleCheckoutAccessor.AddVehicleCheckout(checkout);
            }
            catch (Exception)
            {
                throw;
            }

            return checkoutId;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Retrieves Vehicle Checkout By Id
        /// </summary>
        /// <param name="vehicleCheckoutId">Resort Vehicle Checkout Id</param>
        /// <returns>Resort Vehicle Checkout</returns>
        public ResortVehicleCheckout RetrieveVehicleCheckoutById(int vehicleCheckoutId)
        {
            ResortVehicleCheckout resortVehicleCheckout;

            try
            {
                resortVehicleCheckout = _resortVehicleCheckoutAccessor.RetrieveVehicleCheckoutById(vehicleCheckoutId);
            }
            catch (Exception)
            {
                throw;
            }

            return resortVehicleCheckout;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Retrieves all Vehicle Checkouts
        /// </summary>
        /// <returns>Resort Vehicle Checkout Collection</returns>
        public IEnumerable<ResortVehicleCheckout> RetrieveVehicleCheckouts()
        {
            IEnumerable<ResortVehicleCheckout> vehicleCheckouts;

            try
            {
                vehicleCheckouts = _resortVehicleCheckoutAccessor.RetrieveVehicleCheckouts();
            }
            catch (Exception)
            {
                throw;
            }

            return vehicleCheckouts;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Updates Vehicle Checkout
        /// </summary>
        /// <param name="old">Old Vehicle Checkout (database copy)</param>
        /// <param name="newResortVehicleCheckOut">New Vehicle Checkout (new copy)</param>
        public void UpdateVehicleCheckouts(ResortVehicleCheckout old, ResortVehicleCheckout newResortVehicleCheckOut)
        {
            try
            {
                this.MeetsValidationCriteria(newResortVehicleCheckOut, GetResortVehicleValidationCriteria());

                _resortVehicleCheckoutAccessor.UpdateVehicleCheckouts(old, newResortVehicleCheckOut);
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
        /// Deletes Vehicle Checkout from database
        /// </summary>
        /// <param name="vehicleCheckoutId">Vehicle Checkout Id</param>
        public void DeleteVehicleCheckout(int vehicleCheckoutId)
        {
            try
            {
                _resortVehicleCheckoutAccessor.DeleteVehicleCheckout(vehicleCheckoutId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/24
        ///
        /// Retrieves all available vehicles
        /// </summary>
        /// <returns>List of available resort vehicles</returns>
        public IEnumerable<ResortVehicle> RetrieveAvailableResortVehicles()
        {
            IEnumerable<ResortVehicle> availableVehicles = null;

            try
            {
                availableVehicles = _resortVehicleAccessor.RetrieveVehicles().Where(
                    x => x.ResortVehicleStatusId.Equals(new ResortVehicleStatus().Available));
            }
            catch (Exception)
            {
                throw;
            }

            return availableVehicles;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/24
        ///
        /// Retrieves a list of checked out vehicles
        /// </summary>
        /// <returns>A list of checked out vehicles</returns>
        public IEnumerable<ResortVehicleCheckoutDecorator> RetrieveCurrentlyCheckedOutVehicles()
        {
            List<ResortVehicleCheckoutDecorator> resortVehicleCheckoutsDecorator;

            try
            {
                var resortVehicleCheckouts = RetrieveVehicleCheckouts()?.Where(x => x.Returned == false);

                resortVehicleCheckoutsDecorator = new List<ResortVehicleCheckoutDecorator>();

                if (resortVehicleCheckouts == null)
                    return resortVehicleCheckoutsDecorator;

                resortVehicleCheckoutsDecorator.AddRange(resortVehicleCheckouts.Select(
                    item => new ResortVehicleCheckoutDecorator
                {
                    VehicleCheckoutId = item.VehicleCheckoutId,
                    EmployeeId = item.EmployeeId,
                    DateCheckedOut = item.DateCheckedOut,
                    DateReturned = item.DateReturned,
                    DateExpectedBack = item.DateExpectedBack,
                    Returned = item.Returned,
                    ResortVehicleId = item.ResortVehicleId
                }));
            }
            catch (Exception)
            {
                throw;
            }            

            return resortVehicleCheckoutsDecorator;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/24
        ///
        /// Performs vehicle checkout and changes Resort Vehicle
        /// Status and Available fields
        /// required
        /// </summary>
        /// <param name="vehicleCheckout">Vehicle to Checkout</param>
        public void CheckoutVehicle(ResortVehicleCheckout vehicleCheckout)
        {
            try
            {
                _resortVehicleCheckoutAccessor.AddVehicleCheckout(vehicleCheckout);

                CheckoutResortVehicleInResortVehiclesTable(vehicleCheckout.ResortVehicleId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CheckInVehicle(int vehicleCheckoutId)
        {
            var checkoutVehicleInDb = RetrieveVehicleCheckoutById(vehicleCheckoutId);

            var mutatedCheckOutVehicle = checkoutVehicleInDb.DeepClone();

            mutatedCheckOutVehicle.DateReturned = DateTime.Now;

            mutatedCheckOutVehicle.Returned = true;

            UpdateVehicleCheckouts(checkoutVehicleInDb, mutatedCheckOutVehicle);

            // ... update status for resort vehicle
            CheckInVehicleInResortVehicleTable(checkoutVehicleInDb.ResortVehicleId);
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/24
        /// 
        /// Changes status of vehicle in resort vehicle table
        /// </summary>
        /// <param name="resortVehicleId">Resort Vehicle Id</param>
        private void CheckInVehicleInResortVehicleTable(int resortVehicleId)
        {
            var resortVehicle = _resortVehicleAccessor.RetrieveVehicleById(resortVehicleId);

            var mutatedResortVehicle = resortVehicle.DeepClone();

            mutatedResortVehicle.Available = true;

            mutatedResortVehicle.ResortVehicleStatusId = new ResortVehicleStatus().Available;

            _resortVehicleAccessor.UpdateVehicle(resortVehicle, mutatedResortVehicle);
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/24
        ///
        /// Changes vehicle status and available fields
        /// </summary>
        /// <param name="resortVehicleId"></param>
        private void CheckoutResortVehicleInResortVehiclesTable(int resortVehicleId)
        {
            var resortVehicle = _resortVehicleAccessor.RetrieveVehicleById(resortVehicleId);

            var mutatedResortVehicle = resortVehicle.DeepClone();

            mutatedResortVehicle.ResortVehicleStatusId = new ResortVehicleStatus().InUse;

            mutatedResortVehicle.Available = false;

            _resortVehicleAccessor.UpdateVehicle(resortVehicle, mutatedResortVehicle);
        }


        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Validation Rules for Resort Vehicle Checkout
        /// </summary>
        /// <returns>Dictionary containing validation rules</returns>
        private Dictionary<string, ValidationCriteria> GetResortVehicleValidationCriteria()
        {
            return new Dictionary<string, ValidationCriteria>
            {
                {
                    nameof(ResortVehicleCheckout.VehicleCheckoutId),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = int.MaxValue}
                },
                {
                    nameof(ResortVehicleCheckout.EmployeeId),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = int.MaxValue}
                },
                {
                    nameof(ResortVehicleCheckout.DateCheckedOut),
                    new ValidationCriteria {CanBeNull = false}
                },
                {nameof(ResortVehicleCheckout.DateExpectedBack), new ValidationCriteria {CanBeNull = false}},
                {
                    nameof(ResortVehicleCheckout.ResortVehicleId),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = int.MaxValue}
                }
            };
        }
    }
}