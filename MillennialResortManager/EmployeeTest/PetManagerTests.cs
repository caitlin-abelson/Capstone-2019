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
            Assert.IsTrue(result);
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
            Assert.IsFalse(result);
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


















    }
}
