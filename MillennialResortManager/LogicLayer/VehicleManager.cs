using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LogicLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/2/23
    /// 
    /// Class for Shuttel Véhicule Manager
    /// </summary>
    public class VehicleManager : IVehicleManager
    {
        readonly IVehicleAccessor _vehicleAccessor;

        public VehicleManager(IVehicleAccessor vehicleAccessor)
        {
            _vehicleAccessor = vehicleAccessor;
        }

        public VehicleManager() : this(new VehicleAccessor()){}

        /// <summary>
        /// Description: Adds vehicle to database
        /// Exceptions: Thrown down the stack
        /// </summary>
        /// <param name="vehicle">Vehicle Object</param>
        public void AddVehicle(Vehicle vehicle)
        {
            try
            {
                MeetsVehicleValidationCriteria(vehicle);

                _vehicleAccessor.AddVehicle(vehicle);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Description: Retrieves all vehicles from database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vehicle> RetrieveVehicles()
        {
            IEnumerable<Vehicle> vehicles;

            try
            {
                vehicles = _vehicleAccessor.RetrieveVehicles();

                // avoid sending null to presentation layer
                if(vehicles == null)
                    vehicles = new List<Vehicle>();
            }
            catch (Exception)
            {
                throw;
            }

            return vehicles;
        }

        /// <summary>
        /// Description: Deactivates Vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        public void DeactivateVehicle(Vehicle vehicle, User user = null)
        {
            // make sure vehice is not active
            if (!vehicle.Active)
            {
                throw new ApplicationException("Vehicle already inactive");
            }

            if (user != null)
            {
                // Check for Admin privilges
                if (user.Roles != null)
                {
                    if (!user.Roles.Contains("Admin"))
                    {
                        throw new ApplicationException("You do not have permissions to deactivate. Your current roles: :(" + user.Roles.ToArray().ToString());
                    }
                }
            }
            else
            {
                // user was not provided. do nothing.
            }

            try
            {
                _vehicleAccessor.DeactivateVehicle(vehicle.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Activates vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="user"></param>
        public void ActivateVehicle(Vehicle vehicle, User user = null)
        {
            try
            {
                if (vehicle == null)
                {
                    throw new ApplicationException("Vehice cannot be null");
                }

                if (user != null)
                {
                    // .. check the role
                    if (user.Roles != null)
                    {
                        if (!user.Roles.Contains("Admin"))
                        {
                            throw new ApplicationException("You do not have system privilege to delete");
                        }
                    }
                }
                else
                {
                    // .. user wasn't provided, no way to check user role
                }

                var newVehicle = vehicle.DeepClone();

                newVehicle.Active = true;

                newVehicle.DeactivationDate = null;

                _vehicleAccessor.UpdateVehicle(vehicle, newVehicle);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Description: Retrieves vehicle by id
        /// </summary>
        /// <param name="id">Id of Vehicle</param>
        public Vehicle RetrieveVehicleById(int id)
        {
            Vehicle vehicle;

            try
            {
                vehicle = _vehicleAccessor.RetrieveVehicleById(id);

            }
            catch (Exception)
            {
                throw;
            }

            return vehicle;
        }

        /// <summary>
        /// Updates vehicle
        /// </summary>
        /// <param name="oldVehicle">Old vehicle before update</param>
        /// <param name="newVehicle">New vehicle after update</param>
        public void UpdateVehicle(Vehicle oldVehicle, Vehicle newVehicle)
        {
            try
            {
                MeetsVehicleValidationCriteria(newVehicle);

                _vehicleAccessor.UpdateVehicle(oldVehicle, newVehicle);
            }
            catch (Exception)
            {
                throw;
            } 
        }

        /// <summary>
        /// Deletes vehicle
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="user"></param>
        public void DeleteVehicle(Vehicle vehicle, User user = null)
        {
            try
            {
                // make sure vehicle is inactive
                if (vehicle.Active)
                {
                    throw new ApplicationException("Vehicle is active. Deactivate first");
                }

                if (user != null)
                {
                    // .. check the role
                    if (user.Roles != null)
                    {
                        if (!user.Roles.Contains("Admin"))
                        {
                            throw new ApplicationException("You do not have system privilege to delete");
                        }
                    }
                }
                else
                {
                    // .. user wasn't provided, no way to check user role
                }

                _vehicleAccessor.DeleteVehicle(vehicle.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Validates vehicle properties
        /// </summary>
        /// <param name="vehicle"></param>
        private static void MeetsVehicleValidationCriteria(Vehicle vehicle)
        {
            string validationErrorMessage = "";

            if (vehicle == null)
            {
                validationErrorMessage += "Vehicle cannot be null\n";
            }
            else
            {
                PropertyInfo[] properties = typeof(Vehicle).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    // validation on strings
                    if (property.PropertyType == typeof(string))
                    {
                        if (property.GetValue(vehicle) == null)
                        {
                            validationErrorMessage += property.Name + " cannot be null" + "\n";
                        }
                        else
                        {
                            // Make, Model, Color
                            if (property.Name.Equals(nameof(vehicle.Make))
                                  || property.Name.Equals(nameof(vehicle.Model))
                                  || property.Name.Equals(nameof(vehicle.Color)))
                            {
                                int min = 0;
                                int max = 30;
                                var value = (string)property.GetValue(vehicle);
                                if (!Enumerable.Range(min, max + 1).Contains(value.Length))
                                    validationErrorMessage += property.Name + " length must be between " + min + " and " + max + "\n";
                            }

                            // License
                            if (property.Name.Equals(nameof(vehicle.License)))
                            {
                                int min = 0;
                                int max = 10;
                                var value = (string)property.GetValue(vehicle);
                                if (!Enumerable.Range(min, max + 1).Contains(value.Length))
                                    validationErrorMessage += property.Name + " length must be between " + min + " and " + max + "\n";
                            }

                            // Vin
                            if (property.Name.Equals(nameof(vehicle.Vin)))
                            {
                                int min = 0;
                                int max = 17;
                                var value = (string)property.GetValue(vehicle);
                                if (!Enumerable.Range(min, max + 1).Contains(value.Length))
                                    validationErrorMessage += property.Name + " length must be between " + min + " and " + max + "\n";
                            }

                            // Description
                            if (property.Name.Equals(nameof(vehicle.Description)))
                            {
                                int min = 0;
                                int max = 1000;
                                var value = (string)property.GetValue(vehicle);
                                if (!Enumerable.Range(min, max + 1).Contains(value.Length))
                                    validationErrorMessage += property.Name + " length must be between " + min + " and " + max + "\n";

                            }
                        }
                    }

                    // validation on ints
                    if(property.PropertyType == typeof(int))
                    {
                        // Vehicle Id
                        if (property.Name.Equals(nameof(vehicle.Id)))
                        {
                            if ((int)property.GetValue(vehicle) < 0)
                                validationErrorMessage += property.Name + " cannot be less 0" + "\n";
                        }

                        // Year of Manufacture
                        if (property.Name.Equals(nameof(vehicle.YearOfManufacture)))
                        {
                            int min = 1900;
                            int max = 300; // Increment 300 years from 1900
                            if (!Enumerable.Range(min, max + 1).Contains((int)property.GetValue(vehicle)))
                                validationErrorMessage += property.Name + " length must be between " + min + " and " + max + "\n";
                        }

                        // Mileage
                        if (property.Name.Equals(nameof(vehicle.Mileage)))
                        {
                            int min = 0;
                            int max = 1000000;

                            if ((int)property.GetValue(vehicle) < 0)
                                validationErrorMessage += property.Name + " length cannot be less 0" + "\n";
                            else if (!Enumerable.Range(min, max + 1).Contains((int)property.GetValue(vehicle)))
                                validationErrorMessage += property.Name + " must be between " + min + " and " + max + "\n";
                        }

                        // Capacity
                        if (property.Name.Equals(nameof(vehicle.Capacity)))
                        {
                            int min = 0;
                            int max = 200;
                            if ((int)property.GetValue(vehicle) < 0)
                                validationErrorMessage += property.Name + " length cannot be less 0" + "\n";
                            else if (!Enumerable.Range(min, max + 1).Contains((int)property.GetValue(vehicle)))
                                validationErrorMessage += property.Name + ": what kind of vehicle is that!, length must be between " + min + " and " + max + "\n";
                        }
                    }

                    // validation of dates
                    if (property.PropertyType == typeof(DateTime))
                    {
                        if(property.Name.Equals(nameof(vehicle.PurchaseDate)))
                            if (property.GetValue(vehicle) == null)
                                validationErrorMessage += property.Name + " cannot be null" + "\n";
                    }

                }
            }

            if (validationErrorMessage.Length != 0)
            {
                throw new ArgumentException(validationErrorMessage);
            }
        }
    }
}
