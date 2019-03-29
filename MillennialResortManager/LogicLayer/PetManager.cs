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
        public int CreatePet(Pet newPet) // Edited on 3/17/19 by Matt H.
        {
            int result = 0;

            try
            {
                result = _petAccessor.InsertPet(newPet);
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

        ///  @Author Matthew Hill
        ///  @Created 3/10/19
        //AddPetImageFilename(string filename, int petID)
        /// <summary>
        /// Method for adding a pet image filename.
        /// </summary>
        /// <param name="string filename, int petID">The Add calls CreatePetImageFilename(filename, petID).</param>
        /// <returns>result</returns>
        public bool AddPetImageFilename(string filename, int petID)
        {
            bool result = false;

            try
            {
                result = (1 == _petAccessor.CreatePetImageFilename(filename, petID));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        ///  @Author Matthew Hill
        ///  @Created 3/10/19
        //EditPetImageFilename(int petID, string oldFilename, string newFilename)
        /// <summary>
        /// Method for updating the filename associated with a specific pet.
        /// </summary>
        /// <param name="int petID, string oldFilename, string newFilename">The Edit calls UpdatePetImageFilename(int petID, string oldFilename, string newFilename).</param>
        /// <returns>result</returns>
        public bool EditPetImageFilename(int petID, string oldFilename, string newFilename)
        {
            bool result = false;

            try
            {
                result = (1 == _petAccessor.UpdatePetImageFilename(petID, oldFilename, newFilename));
            }
            catch (Exception)
            {
                throw;

            }
            return result;
        }

        ///  @Author Matthew Hill
        ///  @Created 3/10/19
        //RetrievePetImageFilenameByPetID(int petID)
        /// <summary>
        /// Method for retrieving a pet image filename by petID.
        /// </summary>
        /// <param name="int petID">The Retrieve calls RetrievePetImageFilenameByPetID(petID).</param>
        /// <returns>result</returns>
        public string RetrievePetImageFilenameByPetID(int petID)
        {
            string filename;

            try
            {
                filename = _petAccessor.RetrievePetImageFilenameByPetID(petID);
            }
            catch (Exception)
            {
                throw;
            }
            return filename;
        }

    }
}
