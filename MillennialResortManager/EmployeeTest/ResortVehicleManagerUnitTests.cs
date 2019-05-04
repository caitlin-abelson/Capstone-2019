using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayer.Tests
{
    [TestClass]
    public class ResortVehicleManagerUnitTests
    {
        private IResortVehicleManager _resortVehicleManager;
        private ResortVehicle _goodResortVehicle;
        private Employee _goodUser;

        #region Test Validation Rules (AddVehicle)

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithIdLessThanZero()
        {
            var badVehicle = _goodResortVehicle.DeepClone();

            badVehicle.Id = -1;

            _resortVehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithIdEqualToZero()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Id = 0;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception e)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with id less than zero threw an exception" + "\n"
                            + e.Message + "\n" + e.StackTrace);
            }
        }

        [TestMethod]
        public void TestAddVehicleWithIdGreaterThanZero()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Id = 1;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with id greater than zero threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithMakeNull()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Make = null;

            _resortVehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithMakeLengthEqualToZero()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Make = "";

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with " + nameof(goodVehicle.Make) + " length equal to 0 threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithMakeLengthOnUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Make = new string(new char[30]);

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with " + nameof(goodVehicle.Make) + " length on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithMakeLengthOverUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Make = new string(new char[31]);

            _resortVehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithModelNull()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Model = null;

            _resortVehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithModelLengthEqualToZero()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Model = "";

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with " + nameof(goodVehicle.Model) + " length equal to 0 threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithModelLengthOnUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Model = new string(new char[30]);

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with " + nameof(goodVehicle.Model) + " length on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithModelLengthOverUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Model = new string(new char[31]);

            _resortVehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithYearOfManufactureLessThanLowerBound()
        {
            var badVehicle = _goodResortVehicle.DeepClone();

            badVehicle.Year = 1899;

            _resortVehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithYearOfManufactureOnLowerBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Year = 1900;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception e)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with Year on lower bound threw an exception" + "\n"
                            + e.Message);
            }
        }

        [TestMethod]
        public void TestAddVehicleWithYearOfManufactureWithinBounds()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Year = DateTime.Now.Year;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with Year on within bounds threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithYearOfManufactureOnUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Year = 2200;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with Year on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithYearOfManufactureGreaterThanUpperBound()
        {
            var badVehicle = _goodResortVehicle.DeepClone();

            badVehicle.Year = 2201;

            _resortVehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithLicenseNull()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.License = null;

            _resortVehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithLicenseLengthLessThanLowerBound()
        {
            var bad = _goodResortVehicle.DeepClone();

            bad.License = new string(new char[1]{'A'});

           _resortVehicleManager.AddVehicle(bad);
        }

        [TestMethod]
        public void TestAddVehicleWithLicenseLengthOnLowerBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.License = new string(new char[2] { 'A', 'A' });

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail(
                    $"_resortVehicleManager.AddVehicle with {nameof(goodVehicle.License)} " + 
                           $"license on lower bound but with good content threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithLicenseLengthOnUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.License = new string(Enumerable.Range('A', 9).Select(x => (char)x).ToArray());

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail(
                    $"_resortVehicleManager.AddVehicle with {nameof(goodVehicle.License)}"
                           + $" length on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithLicenseLengthOverUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.License = new string(Enumerable.Range('A', 11).Select(x => (char)x).ToArray());

            _resortVehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithMileageLessThanLowerBound()
        {
            var badVehicle = _goodResortVehicle.DeepClone();

            badVehicle.Mileage = -1;

            _resortVehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithMileageOnLowerBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Mileage = 0;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail($"_resortVehicleManager.AddVehicle with Mileage on lower bound threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithMileageWithinBounds()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Mileage = 1;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail($"_resortVehicleManager.AddVehicle with Mileage on within bounds threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithMileageOnUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Mileage = 1000000;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail($"_resortVehicleManager.AddVehicle with Mileage on upper bound threw an exception");
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithMileageGreaterThanUpperBound()
        {
            var badVehicle = _goodResortVehicle.DeepClone();

            badVehicle.Mileage = 1000001;

            _resortVehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithCapacityLessThanLowerBound()
        {
            var badVehicle = _goodResortVehicle.DeepClone();

            badVehicle.Capacity = -1;

            _resortVehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithCapacityOnLowerBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Capacity = 0;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with Capacity on lower bound threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithCapacityWithinBounds()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Capacity = 1;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with Capacity on within bounds threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithCapacityOnUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Capacity = 200;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with Capacity on upper bound threw an exception");
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithCapacityGreaterThanUpperBound()
        {
            var badVehicle = _goodResortVehicle.DeepClone();

            badVehicle.Capacity = 201;

            _resortVehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithColorNull()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Color = null;

            _resortVehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithColorLengthEqualToZero()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Color = "";

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with " + nameof(goodVehicle.Color) + " length equal to 0 threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithColorLengthOnUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Color = new string(new char[30]);

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with " + nameof(goodVehicle.Color) + " length on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithColorLengthOverUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Color = new string(new char[31]);

            _resortVehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithPurchaseDateNull()
        {
            var badVehicle = _goodResortVehicle.DeepClone();

            badVehicle.PurchaseDate = null;

            _resortVehicleManager.AddVehicle(badVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithPurchaseDateNotNull()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.PurchaseDate = DateTime.Now;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with " + nameof(goodVehicle.PurchaseDate) + " not nulll threw an exception");
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithDescriptionNull()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Description = null;

            _resortVehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithDescriptionLengthEqualToZero()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Description = "";

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with " + nameof(goodVehicle.Description) + " length equal to 0 threw an exception");
            }
        }

        [TestMethod]
        public void TestAddVehicleWithDescriptionLengthOnUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Description = new string(new char[1000]);

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with " + nameof(goodVehicle.Description) + " length on upper bound threw an exception");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleWithDescriptionLengthOverUpperBound()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.Description = new string(new char[1001]);

            _resortVehicleManager.AddVehicle(goodVehicle);
        }

        [TestMethod]
        public void TestAddVehicleWithDeactivationDateNotNull()
        {
            var goodVehicle = _goodResortVehicle.DeepClone();

            goodVehicle.DeactivationDate = DateTime.Now;

            try
            {
                _resortVehicleManager.AddVehicle(goodVehicle);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.AddVehicle with " + nameof(goodVehicle.DeactivationDate) + " not null threw an exception");
            }
        }

        #endregion

        #region RetrieveVehiclesUnitTests

        /// <summary>
        /// Mock returns a null to make
        /// this test possible
        /// </summary>
        [TestMethod]
        public void TestRetrieveVehicleWithNullFromDbMustPassOnNullOrThrowException()
        {
            try
            {
                var vehicles = _resortVehicleManager.RetrieveVehicles();

                Assert.IsTrue(vehicles != null);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.RetrieveVehicles() threw an exception for null returned from database");
            }
        }

        #endregion

        #region DeactivateVehicleTests

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeactivateVehicleThrowsExceptionWhenActiveFieldIsInActive()
        {
            var badVehicle = _goodResortVehicle.DeepClone();

            badVehicle.Active = false;

            _resortVehicleManager.DeactivateVehicle(badVehicle, _goodUser);
        }

        [TestMethod]
        public void TestDeactivateVehicleDoesNotThrowsExceptionWhenActiveFieldIsActive()
        {
            var badVehicle = _goodResortVehicle.DeepClone();

            badVehicle.Active = true;

            try
            {
                _resortVehicleManager.DeactivateVehicle(badVehicle, _goodUser);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.DeactivateVehicle threw an exception when active was true");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeactivateVehicleThrowsExceptionWhenUserIsNotAdmin()
        {
            var roles = new List<Role>() { new Role() { RoleID = _roles.Maintenance.ToString() } };

            Employee userNotAdmin = newEmployee(0, "firstName", "lastName", roles);

            _resortVehicleManager.DeactivateVehicle(_goodResortVehicle, userNotAdmin);
        }

        [TestMethod]
        public void TestDeactivateVehicleThrowsExceptionWhenUserAdmin()
        {
            var roles = new List<string>() { _roles.Admin.ToString() };

            try
            {
                _resortVehicleManager.DeactivateVehicle(_goodResortVehicle, _goodUser);
            }
            catch (Exception)
            {
                Assert.Fail("_resortVehicleManager.DeactivateVehicle threw an exception with admin user");
            }
        }

        #endregion

        #region Setup Helpers

        [TestInitialize]
        public void Setup()
        {
            _resortVehicleManager = new ResortVehicleManager(new MockResortVehicleAccessor());

            _goodResortVehicle = new ResortVehicle()
            {
                Id = 0,
                Make = "",
                Model = "",
                Year = 1990,
                License = "IA 111",
                Mileage = 0,
                Capacity = 0,
                Color = "",
                PurchaseDate = DateTime.Now,
                Description = "",
                Active = true,
                DeactivationDate = null,
                Available = true,
                ResortVehicleStatusId = "",
                ResortPropertyId = 0
            };

            int userId = 0;
            var roles = new List<Role>() { new Role() { RoleID = _roles.Admin.ToString() } };
            _goodUser = new Employee()
            {
                EmployeeID = userId,
                FirstName = "firstName",
                LastName = "lastName",
                EmployeeRoles = roles
            };

        }

        public Employee newEmployee(int empId, string firstName, string lastName, List<Role> roles)
        {
            return new Employee()
            {
                EmployeeID = empId,
                FirstName = firstName,
                LastName = lastName,
                EmployeeRoles = roles
            };
        }

        #endregion
    }

    enum _roles
    {
        Maintenance,
        Admin
    }
}
