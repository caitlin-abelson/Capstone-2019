using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IPetAccessor
    {
        int InsertPet(Pet newPet);
        List<Pet> SelectAllPets();

        int UpdatePet(Pet oldPet, Pet newPet);

        List<Pet> SelectAllPets(int petID);

        int DeletePet(int PetID);
    }
}