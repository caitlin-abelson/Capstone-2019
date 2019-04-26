using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IPetManager
    {
        int CreatePet(Pet newPet);  // Edited on 3/21/19 by Matt Hill.
        List<Pet> RetrieveAllPets();
        bool UpdatePet(Pet oldPet, Pet newPet);
        bool DeletePet(int petID);
        bool AddPetImageFilename(string filename, int petID);
        bool EditPetImageFilename(int petID, string oldFilename, string newFilename);
        string RetrievePetImageFilenameByPetID(int petID);
    }
}