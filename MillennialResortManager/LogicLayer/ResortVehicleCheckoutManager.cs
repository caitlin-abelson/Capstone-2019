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
    /// Resort Vehicle Checkout Manager
    /// </summary>
    public class ResortVehicleCheckoutManager : IResortVehicleCheckoutManager
    {
        private readonly IResortVehicleCheckoutAccessor _resortVehicleCheckoutAccessor;

        public ResortVehicleCheckoutManager(IResortVehicleCheckoutAccessor resortVehicleCheckoutAccessor)
        {
            _resortVehicleCheckoutAccessor = resortVehicleCheckoutAccessor;
        }

        public ResortVehicleCheckoutManager() : this(new ResortVehicleCheckoutAccessor()){ }

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