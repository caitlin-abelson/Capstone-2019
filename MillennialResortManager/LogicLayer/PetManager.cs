using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class PetManager : IPetManager
    {

        /// <summary>
        /// @Author Craig Barkley
        /// @Created2/7/2019
        /// 
        /// This class is for the Pets in the logic layer
        /// </summary>
        /// <param name="newPet"></param>
        /// <returns></returns>
        /// 

        private IPetAccessor _petAccessor;

        public PetManager()
        {
            _petAccessor = new PetAccessor();
        }

        public PetManager(IPetAccessor petAccessor)
        {
            _petAccessor = petAccessor;
        }

        //CreatePet(Pet newPet)
        /// <summary>
        /// Method for creating a new Pet
        /// </summary>
        /// <param name="Pet newPet">The Create Pet is passes the new pet to the InsertPet.</param>
        /// <returns>Result</returns>
        //Method for creating a new Pet
        public bool CreatePet(Pet newPet)
        {
            bool result = false;

            try
            {
                result = (1 == _petAccessor.InsertPet(newPet));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        //RetrieveAllPets()
        /// <summary>
        /// Method for retrieving a Pet.
        /// </summary>
        /// <param name="()">The RetrieveAllPets calls SelectAllPets().</param>
        /// <returns>Pets</returns>
        public List<Pet> RetrieveAllPets()
        {
            List<Pet> Pets = null;

            try
            {
                Pets = _petAccessor.SelectAllPets();
            }
            catch (Exception)
            {
                throw;
            }

            return Pets;
        }

        //UpdatePet(Pet oldPet, Pet newPet)
        /// <summary>
        /// Method for updating a Pet.
        /// </summary>
        /// <param name="Pet oldPet, Pet newPet">The UpdatePet calls UpdatePet(oldPet, newPet).</param>
        /// <returns>Pets</returns>
        public bool UpdatePet(Pet oldPet, Pet newPet)
        {
            bool result = false;

            try
            {
                result = (1 == _petAccessor.UpdatePet(oldPet, newPet));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }



        //RetrievePetByID(int PetID)


        //DeletePet(int PetID)
        /// <summary>
        /// Method for deleting a Pet.
        /// </summary>
        /// <param name="int petID">The Delete calls DeletePet(oldPet, newPet).</param>
        /// <returns>result</returns>
        public bool DeletePet(int petID)
        {
            bool result = false;
            try
            {
                result = (1 == _petAccessor.DeletePet(petID));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    
    }
}
