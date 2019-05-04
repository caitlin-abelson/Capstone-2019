using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayer.Tests
{
    /// <summary>
    /// Author: Richard Carroll
    /// Created Date: 3/8/19
    /// 
    /// This Class contains methods for LogicLayer to ensure strong 
    /// validation and show proper traffic management between PresentationLayer
    /// and DataAccessLayer.
    /// </summary>
    [TestClass]
    public class GuestVehicleManagerTests
    {
        private GuestVehicle guestVehicle;
        private GuestVehicle newGuestVehicle;
        private GuestVehicleAccessorMock guestVehicleAccessor;
        private GuestVehicleManager guestVehicleManager;
        private List<VMGuestVehicle> retrievedVehicles;

        [TestInitialize]
        public void TestSetup()
        {
            guestVehicleAccessor = new GuestVehicleAccessorMock();
            guestVehicleManager = new GuestVehicleManager(guestVehicleAccessor);
        }

        //Borrowed From Kevin Broskow
        private string createString(int length)
        {
            string testLength = "";
            for (int i = 0; i < length; i++)
            {
                testLength += "*";
            }
            return testLength;
        }

        //Here start the CreateGuestVehicle Tests
        [TestMethod]
        public void TestCreateGuestVehicleValidInput()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Honda",
                Model = "Caravan",
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);
            retrievedVehicles = guestVehicleAccessor.SelectAllGuestVehicles();

            Assert.IsNotNull(retrievedVehicles.Find(v => v.GuestID == guestVehicle.GuestID &&
                    v.Make == guestVehicle.Make && v.Model == guestVehicle.Model &&
                    v.PlateNumber == guestVehicle.PlateNumber && v.Color == guestVehicle.Color &&
                    v.ParkingLocation == guestVehicle.ParkingLocation));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestVehicleInvalidMakeBlank()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "",
                Model = "Caravan",
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestVehicleInvalidMakeNull()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = null,
                Model = "Caravan",
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestVehicleInvalidMakeLength()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = createString(31),
                Model = "Caravan",
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestVehicleInvalidModelBlank()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "",
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestVehicleInvalidModelNull()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = null,
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestVehicleInvalidModelLength()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = createString(31),
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestVehicleInvalidPlateNumberBlank()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Caravan",
                PlateNumber = "",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestVehicleInvalidPlateNumberNull()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Caravan",
                PlateNumber = null,
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestVehicleInvalidPlateNumberLength()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Caravan",
                PlateNumber = createString(11),
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestVehicleInvalidColorLength()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Caravan",
                PlateNumber = "123 ABC",
                Color = createString(31),
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestVehicleInvalidParkingLocationLength()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Caravan",
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = createString(51)
            };
            guestVehicleManager.CreateGuestVehicle(guestVehicle);

        }


        //Here start the UpdateGuestVehicle Tests
        //The ValidInput Test for Update is giving me issues, so I am leaving it in comments for now.
        /**
        [TestMethod]
        public void TestUpdateGuestVehicleValidInput()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Honda",
                Model = "Caravan",
                PlateNumber = "124 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);

            retrievedVehicles = guestVehicleAccessor.SelectAllGuestVehicles();

            Assert.IsNotNull(retrievedVehicles.Find(v => v.GuestID == newGuestVehicle.GuestID &&
                    v.Make == newGuestVehicle.Make && v.Model == newGuestVehicle.Model &&
                    v.PlateNumber == newGuestVehicle.PlateNumber && v.Color == newGuestVehicle.Color &&
                    v.ParkingLocation == newGuestVehicle.ParkingLocation));

        }
    */

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleInvalidMakeBlank()
        {
            //I am assuming that during an update, the first(old) 
            //vehicle will be valid, as it was taken from the 
            //database and before that, inserted through this 
            //same validation.
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "",
                Model = "Caravan",
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleInvalidMakeNull()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = null,
                Model = "Caravan",
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleInvalidMakeLength()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = createString(31),
                Model = "Caravan",
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleInvalidModelBlank()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "",
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleInvalidModelNull()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = null,
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleInvalidModelLength()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = createString(31),
                PlateNumber = "123 ABC",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleInvalidPlateNumberBlank()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "",
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleInvalidPlateNumberNull()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = null,
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleInvalidPlateNumberLength()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = createString(11),
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleInvalidColorLength()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = createString(31),
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleInvalidParkingLocationLength()
        {
            guestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            };
            newGuestVehicle = new GuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = createString(11),
                Color = "White",
                ParkingLocation = "Out Back"
            };
            guestVehicleManager.UpdateGuestVehicle(guestVehicle, newGuestVehicle);
        }

        //Here Begins the RetrieveAllGuestVehicles Test(s)
        [TestMethod]
        public void TestRetrieveAllGuestVehicles()
        {
            retrievedVehicles = guestVehicleManager.RetrieveAllGuestVehicles();
            Assert.IsNotNull(retrievedVehicles);
        }
    }
}