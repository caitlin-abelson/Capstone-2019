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

        string RetrievePetImageFilenameByPetID(int petID); // Added on 3/21/19 by Matt Hill.

        int CreatePetImageFilename(string filename, int petID); // Added on 3/21/19 by Matt Hill.

        int UpdatePetImageFilename(int petID, string oldFilename, string newFilename); // Added on 3/21/19 by Matt Hill.
    }
}