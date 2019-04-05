using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;


namespace DataAccessLayer
{
    /// <summary>
    /// Author: Craig Barkley
    /// Created : 2/21/2019
    /// Here are the Test Methods for PetManager
    /// </summary>
    public class PetAccessorMock: IPetAccessor
    {
        private List<Pet> _pets;
        private List<int> _AllPets;
        //private List<VMBrowsePet> _vmBrowsePets;


        public PetAccessorMock()
        {
            _pets = new List<Pet>();
            _pets.Add(new Pet() { PetID = 100000, PetName = "JimDog", Gender = "Male", Species = "Labradoors", PetTypeID = "snoopdoggy singer", GuestID = 100000 });
            _pets.Add(new Pet() { PetID = 100001, PetName = "RayDog", Gender = "Male", Species = "Labradoors", PetTypeID = "snoopdoggy keyboards", GuestID = 100001 });
            _pets.Add(new Pet() { PetID = 100002, PetName = "JohnDog", Gender = "Male", Species = "Labradoors", PetTypeID = "snoopdoggy drums", GuestID = 100002 });
            _pets.Add(new Pet() { PetID = 100003, PetName = "RobbieDog", Gender = "Male", Species = "Labradoors", PetTypeID = "snoopdoggy guitar", GuestID = 100003 });
       

            _AllPets = new List<int>();

            foreach(var pet in _pets)
            {
                _AllPets.Add(pet.GuestID);
            }
        }



        public int DeletePet(int PetID)
        {
            throw new NotImplementedException();
        }

        public int InsertPet(Pet newPet)
        {
            if (newPet == null)

                return 0;

            _pets.Add(newPet);

            return 1;
        }

        public List<Pet> SelectAllPets(int PetID)
        {
            return _pets;
        }

        public List<Pet> SelectAllPets()
        {
            return _pets;
        }

        public int UpdatePet(Pet oldPet, Pet newPet)
        {
            throw new NotImplementedException();
        }

        ///  @Author Matthew Hill
        ///  @Created 3/16/19
        //RetrievePetImageFilenameByPetID(int petID)
        /// <summary>
        /// Mock method for testing RetrievePetImageFilenameByPetID
        /// </summary>
        /// <param name="int petID">The params to be passed to Retrieve.</param>
        /// <returns>int</returns>
        public string RetrievePetImageFilenameByPetID(int petID)
        {
            Pet testPet = new Pet();
            testPet.PetID = 100099;
            testPet.imageFilename = "testPetImg.jpg";

            if (testPet.PetID != petID)
            {
                throw new ArgumentException();
            }
            else
            {
                return testPet.imageFilename;
            }
        }


        ///  @Author Matthew Hill
        ///  @Created 3/10/19
        //CreatePetImageFilename(string filename, int petID)
        /// <summary>
        /// Mock method for testing CreatePetImageFilename
        /// </summary>
        /// <param name="string filename, int petID">The params to be passed to Create.</param>
        /// <returns>int</returns>
        public int CreatePetImageFilename(string filename, int petID)
        {
            if (filename == null)
            {
                return 0;
            }
            Pet myPet = new Pet() { imageFilename = filename, PetID = petID };

            if (myPet.imageFilename != filename)
            {
                return 0;
            }

            return 1;
        }


        ///  @Author Matthew Hill
        ///  @Created 3/16/19
        //UpdatePetImageFilename(int petID, string oldFilename, string newFilename)
        /// <summary>
        /// Mock method for testing UpdatePetImageFilename
        /// </summary>
        /// <param name="int petID, string oldFilename, string newFilename">The params to be passed to Update.</param>
        /// <returns>int</returns>
        public int UpdatePetImageFilename(int petID, string oldFilename, string newFilename)
        {
            Pet testPet = new Pet();
            testPet.PetID = 100099;
            testPet.imageFilename = "testPetImg.jpg";

            if (petID != testPet.PetID)
            {
                return 0;
            }
            else if (oldFilename != testPet.imageFilename)
            {
                return 0;
            }

            testPet.imageFilename = newFilename;
            return 1;
        }
    }
}
