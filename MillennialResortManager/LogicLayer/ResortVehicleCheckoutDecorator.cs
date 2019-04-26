using System;
using DataObjects;

namespace LogicLayer
{
    public class ResortVehicleCheckoutDecorator : ResortVehicleCheckout
    {
        private readonly ResortVehicleManager _resortVehicleManager;
        private readonly EmployeeManager _employeeManager;

        public ResortVehicleCheckoutDecorator() : this(new ResortVehicleManager(), new EmployeeManager()) { }

        public ResortVehicleCheckoutDecorator(ResortVehicleManager resortVehicleManager
            , EmployeeManager employeeManager)
        {
            _resortVehicleManager = resortVehicleManager;
            _employeeManager = employeeManager;
        }

        public string ResortVehicleMake
        {
            get
            {
                try
                {
                    return _resortVehicleManager.RetrieveVehicleById(ResortVehicleId)?.Make;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string ResortVehicleModel
        {
            get
            {
                try
                {
                    return _resortVehicleManager.RetrieveVehicleById(ResortVehicleId)?.Model;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string EmployeeFirstName
        {
            get
            {
                try
                {
                    return _employeeManager.RetrieveEmployeeInfo(EmployeeId)?.FirstName;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string EmployeeLastName
        {
            get
            {
                try
                {
                    return _employeeManager.RetrieveEmployeeInfo(EmployeeId)?.LastName;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
    }
}