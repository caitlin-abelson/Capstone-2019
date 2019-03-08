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
    }
}
