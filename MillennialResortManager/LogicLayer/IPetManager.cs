using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IPetManager
    {
        bool CreatePet(Pet newPet);
        List<Pet> RetrieveAllPets();
        bool UpdatePet(Pet oldPet, Pet newPet);
        bool DeletePet(int petID);
    }
}