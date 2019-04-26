using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataAccessLayer;
using DataObjects;


namespace UnitTest
{
    /// <summary>
    /// Author: Craig Barkley
    /// Created : 2/21/2019
    /// Here are the Test Methods for PetManager
    /// </summary>
    [TestClass]
    public class PetManagerTests
    {
        private IPetManager _petManager;

        private List<Pet> _pets;

        private PetAccessorMock _pet;


        public void testSetUp()
        {
            _pet = new PetAccessorMock();
            _petManager = new PetManager(_pet);
            _pets = new List<Pet>();
            _pets = _petManager.RetrieveAllPets();
        }

        [TestMethod]
        public void CanCreatePet()
        {
            //Arrange
            var mockPetAccessor = new PetAccessorMock();
            var petManager = new PetManager(mockPetAccessor);
            var newPet = new Pet();
            //Act
            var result = petManager.CreatePet(newPet);
            //Assert
            Assert.AreEqual(1, result); // edited on 3/17/119 by Matt H.
        }

        [TestMethod]
        public void CreatePetReturnsFalseForNull()
        {
            //Arrange
            var mockPetAccessor = new PetAccessorMock();
            var petManager = new PetManager(mockPetAccessor);
            //Act
            var result = petManager.CreatePet(null);
            //Assert
            Assert.AreEqual(0, result); // edited on 3/17/19 by Matt H.
        }

        [TestMethod]
        public void CanRetrieveAllPets()
        {
            //Arrange
            var mockPetAccessor = new PetAccessorMock();
            var petManager = new PetManager(mockPetAccessor);
            //Act
            var result = petManager.RetrieveAllPets();
            //Assert
            Assert.AreEqual(4, result.Count);
            petManager.CreatePet(new Pet());
            petManager.CreatePet(new Pet());
            result = petManager.RetrieveAllPets();
            Assert.AreEqual(6, result.Count);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CanDeletePet()
        {

            int petID = 999991;

            Pet pet = new Pet()
            {
                PetID = petID,
                PetName = "PetName",
                Gender = "Male",
                Species = "Lion",
                PetTypeID = "Cat",
                GuestID = 123456

    };

            _petManager.CreatePet(pet);


            _petManager.DeletePet(999991);

            _petManager.RetrieveAllPets();

        }

        /// <summary>
        /// Author: Matthew Hill
        /// Created : 3/16/2019
        /// Test method for successfully creating a pet image.
        /// </summary>
        [TestMethod]
        public void CanCreatePetImage()
        {
            //Arrange
            var mockPetAccessor = new PetAccessorMock();
            var petManager = new PetManager(mockPetAccessor);
            Pet newPet = new Pet();
            newPet.PetID = 100099;
            newPet.imageFilename = "pet999.jpg";
            //Act
            petManager.CreatePet(newPet);
            var result = petManager.AddPetImageFilename(newPet.imageFilename, newPet.PetID);
            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Author: Matthew Hill
        /// Created : 3/16/2019
        /// Test method for creating a pet image using a null filename.
        /// </summary>
        [TestMethod]
        public void CreatePetImageReturnsFalseForNullFilename()
        {
            //Arrange
            var mockPetAccessor = new PetAccessorMock();
            var petManager = new PetManager(mockPetAccessor);
            //Act
            var result = petManager.AddPetImageFilename(null, 100100);
            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Author: Matthew Hill
        /// Created : 3/16/2019
        /// Test method for successfully retrieving a pet image by pet id.
        /// </summary>
        [TestMethod]
        public void CanRetrievePetImageByID()
        {
            //Arrange
            var mockPetAccessor = new PetAccessorMock();
            var petManager = new PetManager(mockPetAccessor);
            //Act
            int testID = 100099;
            string expectedString = "testPetImg.jpg";
            var result = petManager.RetrievePetImageFilenameByPetID(testID);
            //Assert
            Assert.AreEqual(expectedString, result);

        }

        /// <summary>
        /// Author: Matthew Hill
        /// Created : 3/16/2019
        /// Test method for retrieving a pet image by invalid pet id.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RetrievePetImageByInvalidID()
        {
            //Arrange
            var mockPetAccessor = new PetAccessorMock();
            var petManager = new PetManager(mockPetAccessor);
            //Act
            int invalidTestID = 110000;
            var result = petManager.RetrievePetImageFilenameByPetID(invalidTestID);
        }

        /// <summary>
        /// Author: Matthew Hill
        /// Created : 3/16/2019
        /// Test method for successfully updating a pet image.
        /// </summary>
        [TestMethod]
        public void CanUpdatePetImage()
        {
            //Arrange
            var mockPetAccesspr = new PetAccessorMock();
            var petManager = new PetManager(mockPetAccesspr);
            //Act
            int testID = 100099;
            string testOldFilename = "testPetImg.jpg";
            string testNewFilename = "testPetImgTwo.jpg";
            var result = petManager.EditPetImageFilename(testID, testOldFilename, testNewFilename);
            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Author: Matthew Hill
        /// Created : 3/16/2019
        /// Test method for updating a pet image with an invalid old filename.
        /// </summary>
        [TestMethod]
        public void UpdatePetImageWithInvalidOldFilenameReturnsFalse()
        {
            //Arrange
            var mockPetAccessor = new PetAccessorMock();
            var petManager = new PetManager(mockPetAccessor);
            //Act
            int testID = 100099;
            string testOldFilename = "wrongfilebud.jpg";
            string testNewFilename = "testPetImgTwo.jpg";
            var result = petManager.EditPetImageFilename(testID, testOldFilename, testNewFilename);
            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Author: Matthew Hill
        /// Created : 3/16/2019
        /// Test method for updating a pet image with an invalid pet id.
        /// </summary>
        [TestMethod]
        public void UpdatePetImageWithInvalidIDReturnsFalse()
        {
            //Arrange
            var mockPetAccessor = new PetAccessorMock();
            var petManager = new PetManager(mockPetAccessor);
            //Act
            int testID = 199999;
            string testOldFilename = "testPetimg.jpg";
            string testNewFilename = "testPetImgTwo.jpg";
            var result = petManager.EditPetImageFilename(testID, testOldFilename, testNewFilename);
            //Assert
            Assert.IsFalse(result);
        }


















    }
}
