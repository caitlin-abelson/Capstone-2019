using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
	/// <summary author="Francis Mingomba" created="2019/04/03">
	/// Resort Vehicle Manager
	/// </summary>
	public class ResortVehicleManager : IResortVehicleManager
    {
        private readonly IResortVehicleAccessor _resortVehicleAccessor;
        private readonly IResortPropertyAccessor _resortPropertyAccessor;
        private readonly IResortVehicleStatusAccessor _resortVehicleStatusAccessor;

        public ResortVehicleManager(IResortVehicleAccessor resortVehicleAccessor
                                    , IResortPropertyAccessor resortPropertyAccessor = null
                                    , IResortVehicleStatusAccessor resortVehicleStatusAccessor = null)
        {
            _resortVehicleAccessor = resortVehicleAccessor;
            _resortPropertyAccessor = resortPropertyAccessor ?? new ResortPropertyAccessor();
            _resortVehicleStatusAccessor = resortVehicleStatusAccessor ?? new ResortVehicleStatusAccessor();
        }

        public ResortVehicleManager() : this(new ResortVehicleAccessor()) { }

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Adds vehicle to database
		/// </summary>
		/// <param name="resortVehicle">resort vehicle</param>
		public void AddVehicle(ResortVehicle resortVehicle)
        {
            try
            {
                this.MeetsValidationCriteria(resortVehicle, GetResortVehicleValidationCriteria());

                _resortVehicleAccessor.AddVehicle(resortVehicle);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Retrieves vehicles from database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ResortVehicle> RetrieveVehicles()
        {
            IEnumerable<ResortVehicle> vehicles;

            try
            {
                // avoid sending null to presentation layer
                vehicles = _resortVehicleAccessor.RetrieveVehicles()
                            ?? new List<ResortVehicle>();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return vehicles;
        }

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Checks employee privileges and deactivates vehicles
		/// if employee has privileges to do it
		/// </summary>
		/// <param name="resortVehicle">resort vehicle</param>
		/// <param name="employee">employee performing operation</param>
		public void DeactivateVehicle(ResortVehicle resortVehicle, Employee employee = null)
        {
            if (!employee.HasRoles(out string errorStr, "Admin"))
                throw new ApplicationException(errorStr);

            // make sure vehicle is not active
            if (!resortVehicle.Active)
                throw new ApplicationException("Vehicle already inactive");

            // make sure vehicle is not in use
            if(resortVehicle.ResortVehicleStatusId == new ResortVehicleStatus().InUse)
                throw new ApplicationException("Vehicle currently in use");

            try
            {
                _resortVehicleAccessor.DeactivateVehicle(resortVehicle.Id);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Checks employee privileges and activates vehicles
		/// if employee has privileges to do it
		/// </summary>
		/// <param name="resortVehicle">resort vehicle</param>
		/// <param name="employee">employee performing operation</param>
		public void ActivateVehicle(ResortVehicle resortVehicle, Employee employee = null)
        {
            try
            {
                if (resortVehicle == null)
                    throw new ApplicationException("Vehicle cannot be null");

                if (!employee.HasRoles(out string errorStr, "Admin"))
                    throw new ApplicationException(errorStr);

                var newVehicle = resortVehicle.DeepClone();

                newVehicle.Active = true;

                newVehicle.DeactivationDate = null;

                _resortVehicleAccessor.UpdateVehicle(resortVehicle, newVehicle);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Retrieves vehicles by id
		/// </summary>
		/// <param name="id">vehicle id</param>
		/// <returns>returns resort vehicle</returns>
		public ResortVehicle RetrieveVehicleById(int id)
        {
            ResortVehicle resortVehicle;

            try
            {
                resortVehicle = _resortVehicleAccessor.RetrieveVehicleById(id);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return resortVehicle;
        }

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Performs validation
		/// Updates vehicle in database
		/// </summary>
		/// <param name="oldResortVehicle">old resort vehicle (database copy)</param>
		/// <param name="newResortVehicle">new resort vehicle (updated copy)</param>
		public void UpdateVehicle(ResortVehicle oldResortVehicle, ResortVehicle newResortVehicle)
        {
            try
            {
                this.MeetsValidationCriteria(newResortVehicle, GetResortVehicleValidationCriteria());

                _resortVehicleAccessor.UpdateVehicle(oldResortVehicle, newResortVehicle);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Deletes resort vehicle from database.
		/// This function checks employee privileges
		/// before functionality is granted
		/// </summary>
		/// <param name="resortVehicle"></param>
		/// <param name="employee"></param>
		public void DeleteVehicle(ResortVehicle resortVehicle, Employee employee = null)
        {
            try
            {
                // make sure vehicle is inactive
                if (resortVehicle.Active)
                    throw new ApplicationException("Vehicle is active. Deactivate first");

                if (!employee.HasRoles(out string errorStr, "Admin"))
                    throw new ApplicationException(errorStr);

                _resortVehicleAccessor.DeleteVehicle(resortVehicle.Id);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Returns a collection of resort properties
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ResortProperty> RetrieveResortProperties()
        {
            try
            {
                return _resortPropertyAccessor.RetrieveResortProperties();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Francis Mingomba" created="2019/04/15">
		/// Returns a collection of resort vehicle statuses
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ResortVehicleStatus> RetrieveResortVehicleStatuses()
        {
            try
            {
                return _resortVehicleStatusAccessor.RetrieveResortVehicleStatuses();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Validation Rules for ResortVehicle
		/// </summary>
		/// <returns>Dictionary containing validation rules</returns>
		private Dictionary<string, ValidationCriteria> GetResortVehicleValidationCriteria()
        {
            return new Dictionary<string, ValidationCriteria>
            {
                {
                    nameof(ResortVehicle.Id),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = int.MaxValue}
                },
                {
                    nameof(ResortVehicle.Make),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = 30}
                },
                {
                    nameof(ResortVehicle.Model),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = 30}
                },
                {
                    nameof(ResortVehicle.Year),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 1900, UpperBound = 2200}
                },
                {
                    nameof(ResortVehicle.License),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 2, UpperBound = 10,
                        RegexExpression = @"^[A-Z 0-9 \-]*$"}
                },
                {
                    nameof(ResortVehicle.Mileage),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = 1000000}
                },
                {
                    nameof(ResortVehicle.Capacity),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = 200}
                },
                {
                    nameof(ResortVehicle.Color),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = 30}
                },
                {
                    nameof(ResortVehicle.PurchaseDate),
                    new ValidationCriteria {CanBeNull = false}
                },
                {
                    nameof(ResortVehicle.Description),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = 1000}
                },
                {
                    nameof(ResortVehicle.ResortVehicleStatusId),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = 25}
                },
                {
                    nameof(ResortVehicle.ResortPropertyId),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = int.MaxValue}
                },
            };
        }
    }
}
