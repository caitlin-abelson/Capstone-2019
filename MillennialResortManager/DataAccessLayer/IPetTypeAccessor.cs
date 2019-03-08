using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IPetTypeAccessor
    {
        int CreatePetType(PetType newPetType);
        int DeletePetType(string petTypeID);
        List<PetType> RetrievetAllPetTypes(string status);
        List<string> SelectAllPetTypeID();
    }
}