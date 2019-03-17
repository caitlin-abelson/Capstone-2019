using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Permissions;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class VehicleManagerUnitTests
    {
        private IVehicleManager vehicleManager;
        private Vehicle _goodVehicle;
        private User _goodUser;

        #region AddVehicleUnitTests

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithIdLessThanZero()
        {
            var badVehicle = _goodVehicle.DeepClone();

            badVehicle.Id = -1;

            vehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithIdEqualToZero()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Id = 0;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with id less than zero threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithIdGreaterThanZero()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Id = 1;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with id greater than zero threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithMakeNull()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Make = null;

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithMakeLengthEqualToZero()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Make = "";

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.Make) +" length equal to 0 threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithMakeLengthOnUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Make = new string(new char[30]);

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.Make) + " length on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithMakeLengthOverUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Make = new string(new char[31]);

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithModelNull()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Model = null;

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithModelLengthEqualToZero()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Model = "";

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.Model) + " length equal to 0 threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithModelLengthOnUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Model = new string(new char[30]);

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.Model) + " length on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithModelLengthOverUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Model = new string(new char[31]);

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithYearOfManufactureLessThanLowerBound()
        {
            var badVehicle = _goodVehicle.DeepClone();

            badVehicle.YearOfManufacture = 1899;

            vehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithYearOfManufactureOnLowerBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.YearOfManufacture = 1900;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with YearOfManufacture on lower bound threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithYearOfManufactureWithinBounds()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.YearOfManufacture = DateTime.Now.Year;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with YearOfManufacture on within bounds threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithYearOfManufactureOnUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.YearOfManufacture = 2200;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with YearOfManufacture on upper bound threw an exception");
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithYearOfManufactureGreaterThanUpperBound()
        {
            var badVehicle = _goodVehicle.DeepClone();

            badVehicle.YearOfManufacture = 2201;

            vehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithLicenseNull()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.License = null;

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithLicenseLengthEqualToZero()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.License = "";

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.License) + " length equal to 0 threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithLicenseLengthOnUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.License = new string(new char[10]);

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.License) + " length on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithLicenseLengthOverUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.License = new string(new char[11]);

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithMileageLessThanLowerBound()
        {
            var badVehicle = _goodVehicle.DeepClone();

            badVehicle.Mileage = -1;

            vehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithMileageOnLowerBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Mileage = 0;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with Mileage on lower bound threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithMileageWithinBounds()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Mileage = 1;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with Mileage on within bounds threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithMileageOnUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Mileage = 1000000;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with Mileage on upper bound threw an exception");
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithMileageGreaterThanUpperBound()
        {
            var badVehicle = _goodVehicle.DeepClone();

            badVehicle.Mileage = 1000001;

            vehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithVinNull()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Vin = null;

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithVinLengthEqualToZero()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Vin = "";

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.Vin) + " length equal to 0 threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithVinLengthOnUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Vin = new string(new char[17]);

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.Vin) + " length on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithVinLengthOverUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Vin = new string(new char[18]);

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithCapacityLessThanLowerBound()
        {
            var badVehicle = _goodVehicle.DeepClone();

            badVehicle.Capacity = -1;

            vehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithCapacityOnLowerBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Capacity = 0;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with Capacity on lower bound threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithCapacityWithinBounds()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Capacity = 1;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with Capacity on within bounds threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithCapacityOnUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Capacity = 200;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with Capacity on upper bound threw an exception");
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithCapacityGreaterThanUpperBound()
        {
            var badVehicle = _goodVehicle.DeepClone();

            badVehicle.Capacity = 201;

            vehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithColorNull()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Color = null;

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithColorLengthEqualToZero()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Color = "";

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.Color) + " length equal to 0 threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithColorLengthOnUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Color = new string(new char[30]);

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.Color) + " length on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithColorLengthOverUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Color = new string(new char[31]);

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithPurchaseDateNull()
        {
            var badVehicle = _goodVehicle.DeepClone();

            badVehicle.PurchaseDate = null;

            vehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithPurchaseDateNotNull()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.PurchaseDate = DateTime.Now;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.PurchaseDate) + " not nulll threw an exception");
            }
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithDescriptionNull()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Description = null;

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithDescriptionLengthEqualToZero()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Description = "";

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.Description) + " length equal to 0 threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithDescriptionLengthOnUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Description = new string(new char[1000]);

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.Description) + " length on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithDescriptionLengthOverUpperBound()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.Description = new string(new char[1001]);

            vehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithDeactivationDateNotNull()
        {
            var goodVehicle = _goodVehicle.DeepClone();

            goodVehicle.DeactivationDate = DateTime.Now;

            try
            {
                vehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.AddVehicle with " + nameof(goodVehicle.DeactivationDate) + " not null threw an exception");
            }
        }

        #endregion

        #region RetrieveVehiclesUnitTests

        /// <summary>
        /// Mock returns a null to make
        /// this test possible
        /// </summary>
        [TestMethod]
        public void RetrieveVehicleWithNullFromDbMustPassOnNullOrThrowException()
        {
            try
            {
                var vehicles = vehicleManager.RetrieveVehicles();
            
                Assert.IsTrue(vehicles != null);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.RetrieveVehicles() threw an expection for null returned from database");
            } 
        }

        #endregion

        #region DeactivateVehicleTests

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void DeactivateVehicleThrowsExceptionWhenActiveFieldIsInActive()
        {
            var badVehicle = _goodVehicle.DeepClone();

            badVehicle.Active = false;

            vehicleManager.DeactivateVehicle(badVehicle, _goodUser);
        }

        [TestMethod]
        public void DeactivateVehicleDoesNotThrowsExceptionWhenActiveFieldIsActive()
        {
            var badVehicle = _goodVehicle.DeepClone();

            badVehicle.Active = true;

            try
            {
                vehicleManager.DeactivateVehicle(badVehicle, _goodUser);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.DeactivateVehicle threw an exception when active was true");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void DeactivateVehicleThrowsExceptionWhenUserIsNotAdmin()
        {
            var roles = new List<string>() { _roles.Maintenance.ToString() };

            var userNotAdmin = new User(0, "firstName", "lastName", roles);

            vehicleManager.DeactivateVehicle(_goodVehicle, userNotAdmin);
        }

        [TestMethod]
        public void DeactivateVehicleThrowsExceptionWhenUserAdmin()
        {
            var roles = new List<string>() { _roles.Admin.ToString() };

            try
            {
                vehicleManager.DeactivateVehicle(_goodVehicle, _goodUser);
            }
            catch (Exception)
            {
                Assert.Fail("vehicleManager.DeactivateVehicle threw an exception with admin user");
            }
        }

        #endregion


        [TestInitialize]
        public void Setup()
        {
            vehicleManager = new VehicleManager(new MockVehicleAccessor());

            _goodVehicle = new Vehicle()
            {
                Id = 0,
                Make = "",
                Model = "",
                YearOfManufacture = 1990,
                License = "",
                Mileage = 0,
                Vin = "",
                Capacity = 0,
                Color = "",
                PurchaseDate = DateTime.Now,
                Description = "",
                Active = true,
                DeactivationDate = null
            };

            int userId = 0;
            var roles = new List<string>() { _roles.Admin.ToString()};
            _goodUser = new User(userId, "firstName", "lastName", roles);
        }
    }

    enum _roles
    {
        Maintenance,
        Admin
    }
}
