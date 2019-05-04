using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayer.Tests
{
    [TestClass]
    public class SupplierManagerTests
    {

        private ISupplierManager _supplierManager;
        private List<Supplier> _suppliers;

        private SupplierAccessorMock _supplierAccessor;


        [TestInitialize]
        public void TestInitialize()
        {
            _supplierAccessor = new SupplierAccessorMock();
            _supplierManager = new SupplierManager(_supplierAccessor);
            _suppliers = null;
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/12
        /// 
        /// Add a valid Supplier.
        /// </summary>
        [TestMethod]
        public void TestAddSupplierValid()
        {

            // Arrange.
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "52404",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Valid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            // Act.
            try
            {
                _supplierManager.CreateSupplier(testSupplier);

                _suppliers = _supplierManager.RetrieveAllSuppliers();
            }
            catch (Exception)
            {

                throw;
            }

            // Assert.
            Assert.IsNotNull(_suppliers.Find(x =>
               x.SupplierID == testSupplier.SupplierID &&
               x.Name == testSupplier.Name &&
               x.Address == testSupplier.Address &&
               x.City == testSupplier.City &&
               x.State == testSupplier.State &&
               x.PostalCode == testSupplier.PostalCode &&
               x.Country == testSupplier.Country &&
               x.ContactFirstName == testSupplier.ContactFirstName &&
               x.ContactLastName == testSupplier.ContactLastName &&
               x.PhoneNumber == testSupplier.PhoneNumber &&
               x.Email == testSupplier.Email &&
               x.Description == testSupplier.Description &&
               x.Active == testSupplier.Active &&
               x.DateAdded == testSupplier.DateAdded)
            );

        }


        /// <summary>
        /// James Heim
        /// 2019/02/14
        /// 
        /// Attempt to create a Supplier with a postal code that is too short.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidPostalCodeTooShort()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                // Postal Code is of Length 4 instead of 5.
                PostalCode = "1234",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);

        }

        /// <summary>
        /// James Heim
        /// 2019/02/14
        /// 
        /// Attempt to create a Supplier with a postal code that is too long.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidPostalCodeTooLong()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                // Postal Code is of Length 6 instead of 5.
                PostalCode = "123456",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Name field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddSupplierInvalidNameNull()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = null,
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Name field empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidNameEmptyString()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Name field over 50 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidNameTooLong()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_NAME_MAX_LENGTH + 1; i++)
            {
                testSupplier.Name += "A";
            }

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Address field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddSupplierInvalidAddressNull()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = null,
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Address field empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidAddressEmpty()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Address field over 150 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidAddressTooLong()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_ADDRESS_MAX_LENGTH + 1; i++)
            {
                testSupplier.Address += "A";
            }

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with City field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddSupplierInvalidCityNull()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St",
                City = null,
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with City field empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidCityEmptyString()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with City field over 50 characters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidCityTooLong()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_CITY_MAX_LENGTH + 1; i++)
            {
                testSupplier.City += "A";
            }

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with State field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddSupplierInvalidStateNull()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St",
                City = "Cedar Rapids",
                State = null,
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with State field empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidStateEmptyString()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with State field too short.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidStateTooShort()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "A",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with State field over 2 characters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidStateTooLong()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                City = "Cedar Rapids",
                State = "Iow",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Country field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddSupplierInvalidCountryNull()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = null,
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with State field empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidCountryEmptyString()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Country field over 25 characters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidCountryTooLong()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_COUNTRY_MAX_LENGTH + 1; i++)
            {
                testSupplier.Country += "A";
            }

            _supplierManager.CreateSupplier(testSupplier);

        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with PhoneNumber field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddSupplierInvalidPhoneNumberNull()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = null,
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with PhoneNumber field empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidPhoneEmptyString()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with PhoneNumber field too short.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidPhoneNumberTooShort()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "123456789",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with PhoneNumber field too long.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidPhoneNumberTooLong()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "12345678901",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with non-numbers in the
        /// Phone Number field.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidPhoneNumberNonNumbers()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "123AB6789%",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }
        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Email field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddSupplierInvalidEmailNull()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1234567890",
                Email = null,
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Email field empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidEmailEmptyString()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1234567890",
                Email = "",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Email field too short (less than 5 characters).
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidEmailTooShort()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1234567890",
                Email = "a@dd",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Email field too long.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidEmailTooLong()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                Email = "abc@def.com",
                PhoneNumber = "1234567890",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };


            for (int i = 0; i < SupplierValidator.SUPPLIER_EMAIL_MAX_LENGTH + 1; i++)
            {
                testSupplier.Email += "m";

            }

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with an email that doesn't
        /// have an @ sign.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidEmailNoAtSign()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                Email = "abcdef.com",
                PhoneNumber = "1234567890",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with an email that doesn't
        /// have a period.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidEmailNoPeriod()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                Email = "abc@defcom",
                PhoneNumber = "1234567890",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with 
        /// Contact First Name field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddSupplierInvalidContactFirstNameNull()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = null,
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with ContactFirstName field empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidContactFirstNameEmptyString()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with ContactFirstName field over 50 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidContactFirstNameTooLong()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_CONTACT_FIRST_NAME_MAX_LENGTH + 1; i++)
            {
                testSupplier.ContactFirstName += "A";
            }

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with 
        /// Contact Last Name field NULL.
        /// Should throw ArgumentNullException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddSupplierInvalidContactLastNameNull()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = null,
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with ContactLastName field empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidContactLastNameEmptyString()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with ContactLastName field over 100 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidContactLastNameTooLong()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_CONTACT_LAST_NAME_MAX_LENGTH + 1; i++)
            {
                testSupplier.ContactLastName += "A";
            }

            _supplierManager.CreateSupplier(testSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to create a supplier with Description field over 1000 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddSupplierInvalidDescriptionTooLong()
        {
            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_DESCRIPTION_MAX_LENGTH + 1; i++)
            {
                testSupplier.Description += "A";
            }

            _supplierManager.CreateSupplier(testSupplier);
        }

        [TestMethod]
        public void TestRetrieveSupplier()
        {
            // Arrange.
            int testSupplierID = 100045;

            Supplier testSupplier = new Supplier()
            {
                SupplierID = testSupplierID,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(testSupplier);

            // Act.
            Supplier retrievedSupplier = _supplierManager.RetrieveSupplier(testSupplierID);

            // Assert.
            Assert.AreEqual(retrievedSupplier, testSupplier);
        }

        [TestMethod]
        public void TestRetrieveAllSuppliers()
        {
            // Arrange.
            List<Supplier> suppliers = new List<Supplier>();

            // Supplier 1
            var supplier1 = (new Supplier()
            {
                SupplierID = 1,
                Name = "Supplier 1",
                Address = "123 Test Road",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "52404",
                Country = "United States",
                ContactFirstName = "Bob",
                ContactLastName = "Trapp",
                PhoneNumber = "1235554567",
                Email = "bob.trapp@test.com",
                Description = "Bob teaches Java",
                Active = true
            });

            // Supplier 2
            var supplier2 = (new Supplier()
            {
                SupplierID = 2,
                Name = "Supplier 2",
                Address = "123 Test Road",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "52404",
                Country = "United States",
                ContactFirstName = "Jim",
                ContactLastName = "Glasgow",
                PhoneNumber = "1235554567",
                Email = "Jim.Glasgow@test.com",
                Description = "Jim teaches Everything",
                Active = true
            });

            suppliers.Add(supplier1);
            suppliers.Add(supplier2);

            _supplierAccessor.InsertSupplier(supplier1);
            _supplierAccessor.InsertSupplier(supplier2);

            // Act.
            try
            {
                _suppliers = _supplierManager.RetrieveAllSuppliers();
            }
            catch (Exception)
            {

                throw;
            }


            // Assert.
            CollectionAssert.AreEqual(_suppliers, suppliers);
        }

        [TestMethod]
        public void TestUpdateSupplierValid()
        {
            // Arrange.
            // Original Supplier.
            var originalSupplier = (new Supplier()
            {
                SupplierID = 1,
                Name = "Supplier 1",
                Address = "123 Test Road",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "52404",
                Country = "United States",
                ContactFirstName = "Bob",
                ContactLastName = "Trapp",
                PhoneNumber = "1235554567",
                Email = "bob.trapp@test.com",
                Description = "Bob teaches Java",
                Active = true
            });

            _supplierManager.CreateSupplier(originalSupplier);

            // Modified Supplier.
            var modifiedSupplier = (new Supplier()
            {
                SupplierID = 1,
                Name = "Supplier 1!",
                Address = "123 Test Rd",
                City = "Cedar Bend",
                State = "IA",
                PostalCode = "52402",
                Country = "United States",
                ContactFirstName = "Bobbi",
                ContactLastName = "T.",
                PhoneNumber = "1235554568",
                Email = "bobbi.trapp@test.com",
                Description = "Bob is a good teacher.",
                Active = true
            });

            // Act.
            _supplierManager.UpdateSupplier(modifiedSupplier, originalSupplier);

            _suppliers = _supplierManager.RetrieveAllSuppliers();

            // Assert.
            Assert.IsNotNull(_suppliers.Find(x =>
               x.SupplierID == modifiedSupplier.SupplierID &&
               x.Name == modifiedSupplier.Name &&
               x.Address == modifiedSupplier.Address &&
               x.City == modifiedSupplier.City &&
               x.State == modifiedSupplier.State &&
               x.PostalCode == modifiedSupplier.PostalCode &&
               x.Country == modifiedSupplier.Country &&
               x.ContactFirstName == modifiedSupplier.ContactFirstName &&
               x.ContactLastName == modifiedSupplier.ContactLastName &&
               x.PhoneNumber == modifiedSupplier.PhoneNumber &&
               x.Email == modifiedSupplier.Email &&
               x.Description == modifiedSupplier.Description &&
               x.Active == modifiedSupplier.Active &&
               x.DateAdded == modifiedSupplier.DateAdded));
        }

        /// <summary>
        /// James Heim
        /// 2019/02/14
        /// 
        /// Attempt to update a Supplier with a postal code that is too short.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidPostalCodeTooShort()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                // Postal Code is of Length 4 instead of 5.
                PostalCode = "1234",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);

        }

        /// <summary>
        /// James Heim
        /// 2019/02/14
        /// 
        /// Attempt to update a Supplier with a postal code that is too long.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidPostalCodeTooLong()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                // Postal Code is of Length 6 instead of 5.
                PostalCode = "123456",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Name field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSupplierInvalidNameNull()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = null,
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Name field empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidNameEmptyString()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Name field over 50 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidNameTooLong()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_NAME_MAX_LENGTH + 1; i++)
            {
                testSupplier.Name += "A";
            }

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Address field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSupplierInvalidAddressNull()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = null,
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Address field empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidAddressEmpty()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Address field over 150 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidAddressTooLong()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_ADDRESS_MAX_LENGTH + 1; i++)
            {
                testSupplier.Address += "A";
            }

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with City field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSupplierInvalidCityNull()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St",
                City = null,
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with City field empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidCityEmptyString()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with City field over 50 characters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidCityTooLong()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_CITY_MAX_LENGTH + 1; i++)
            {
                testSupplier.City += "A";
            }

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with State field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSupplierInvalidStateNull()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St",
                City = "Cedar Rapids",
                State = null,
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with State field empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidStateEmptyString()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with State field too short.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidStateTooShort()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "A",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with State field over 2 characters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidStateTooLong()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                City = "Cedar Rapids",
                State = "Iow",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Country field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSupplierInvalidCountryNull()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = null,
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with State field empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidCountryEmptyString()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Country field over 25 characters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidCountryTooLong()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_COUNTRY_MAX_LENGTH + 1; i++)
            {
                testSupplier.Country += "A";
            }

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);

        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with PhoneNumber field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSupplierInvalidPhoneNumberNull()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = null,
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with PhoneNumber field empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidPhoneEmptyString()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with PhoneNumber field too short.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidPhoneNumberTooShort()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "123456789",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with PhoneNumber field too long.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidPhoneNumberTooLong()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "12345678901",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with non-numbers in the
        /// Phone Number field.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidPhoneNumberNonNumbers()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "123AB6789%",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }
        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Email field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSupplierInvalidEmailNull()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1234567890",
                Email = null,
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Email field empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidEmailEmptyString()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1234567890",
                Email = "",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Email field too short (less than 5 characters).
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidEmailTooShort()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Main St.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1234567890",
                Email = "a@dd",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Email field too long.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidEmailTooLong()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                Email = "abc@def.com",
                PhoneNumber = "1234567890",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_EMAIL_MAX_LENGTH - 10; i++)
            {
                testSupplier.Email += "m";
            }

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with an email that doesn't
        /// have an @ sign.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidEmailNoAtSign()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                Email = "abcdef.com",
                PhoneNumber = "1234567890",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with an email that doesn't
        /// have a period.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidEmailNoPeriod()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Rapids Rd",
                State = "IA",
                City = "Cedar Rapids",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                Email = "abc@defcom",
                PhoneNumber = "1234567890",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with 
        /// Contact First Name field NULL.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSupplierInvalidContactFirstNameNull()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = null,
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with ContactFirstName field empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidContactFirstNameEmptyString()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with ContactFirstName field over 50 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidContactFirstNameTooLong()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_CONTACT_FIRST_NAME_MAX_LENGTH + 1; i++)
            {
                testSupplier.ContactFirstName += "A";
            }

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with 
        /// Contact Last Name field NULL.
        /// Should throw ArgumentNullException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSupplierInvalidContactLastNameNull()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = null,
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with ContactLastName field empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidContactLastNameEmptyString()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with ContactLastName field over 100 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidContactLastNameTooLong()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_CONTACT_LAST_NAME_MAX_LENGTH + 1; i++)
            {
                testSupplier.ContactLastName += "A";
            }

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/14
        /// 
        /// Attempt to update a supplier with Description field over 1000 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSupplierInvalidDescriptionTooLong()
        {
            Supplier originalSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(originalSupplier);

            Supplier testSupplier = new Supplier()
            {
                SupplierID = 99999,
                Name = "TEST SUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Active = true,
                DateAdded = DateTime.Now
            };

            for (int i = 0; i < SupplierValidator.SUPPLIER_DESCRIPTION_MAX_LENGTH + 1; i++)
            {
                testSupplier.Description += "A";
            }

            _supplierManager.UpdateSupplier(testSupplier, originalSupplier);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/15
        /// 
        /// Active records should be marked Inactive when passed into the Delete function.
        /// 
        /// <remarks>
        /// Updated by James Heim
        /// Updated on 2019/02/21
        /// Properly deactivates the record instead of calling delete.
        /// </remarks>
        /// </summary>
        [TestMethod]
        public void TestDeactivate()
        {
            // Arrange.
            int supplierID = 999991;

            Supplier supplier = new Supplier()
            {
                SupplierID = supplierID,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = true,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(supplier);

            // Act.
            _supplierManager.DeactivateSupplier(supplier);

            // Assert.
            Assert.IsFalse(_supplierManager.RetrieveSupplier(supplierID).Active);

        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/15
        /// 
        /// Inactive Suppliers should be deleted when passed into the Delete function.
        /// An ArgumentNullException should be thrown when we try to retrieve a non-existant 
        /// record
        /// that doesn't exist.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestDelete()
        {

            int supplierID = 999991;

            Supplier supplier = new Supplier()
            {
                SupplierID = supplierID,
                Name = "TESTSUPPLIER",
                Address = "123 Test Dr.",
                City = "Cedar Rapids",
                State = "IA",
                PostalCode = "12345",
                Country = "United States",
                ContactFirstName = "TEST",
                ContactLastName = "DUDE",
                PhoneNumber = "1395552424",
                Email = "test@dude.com",
                Description = "Invalid Supplier",
                Active = false,
                DateAdded = DateTime.Now
            };

            _supplierManager.CreateSupplier(supplier);


            _supplierManager.DeleteSupplier(supplier);

            _supplierManager.RetrieveSupplier(supplierID);

        }

    }
}
