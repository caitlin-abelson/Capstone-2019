using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{

    /// <summary>
    /// Craig Barkley
    /// Created: 2019/02/28
    /// 
    /// Creates a new pet type
    /// </summary>
    public interface IPetTypeManager
    {
        bool AddPetType(PetType newPetType);
        bool DeletePetType(string petTypeID);
        List<string> RetrieveAllPetTypes();
        List<PetType> RetrieveAllPetTypes(string status);
    }
}