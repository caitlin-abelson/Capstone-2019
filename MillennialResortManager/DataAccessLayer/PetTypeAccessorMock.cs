using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class PetTypeAccessorMock : IPetTypeAccessor
    {
        /// <summary>
        /// Craig Barkley
        /// Created: 2019/02/28
        /// 
        /// This is a mock Data Accessor which implements IPetTypeAccessor.  This is for testing purposes only.
        /// </summary>
        /// 

        private List<PetType> petType;
        /// <summary>
        /// Author: Craig Barkley
        /// Created: 2019/02/28
        /// This constructor that sets up dummy data
        /// </summary>
        public PetTypeAccessorMock()
        {
            petType = new List<PetType>
            {
                new PetType {PetTypeID = "PetType1", Description = "petType is a petType"},
                new PetType {PetTypeID = "PetType2", Description = "petType is a petType"},
                new PetType {PetTypeID = "PetType3", Description = "petType is a petType"},
                new PetType {PetTypeID = "PetType4", Description = "petType is a petType"}
            };
        }

        public int CreatePetType(PetType newPetType)
        {
            int listLength = petType.Count;
            petType.Add(newPetType);
            if (listLength == petType.Count - 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int DeletePetType(string petTypeID)
        {
            int rowsDeleted = 0;
            foreach (var type in petType)
            {
                if (type.PetTypeID == petTypeID)
                {
                    int listLength = petType.Count;
                    petType.Remove(type);
                    if (listLength == petType.Count - 1)
                    {
                        rowsDeleted = 1;
                    }
                }
            }

            return rowsDeleted;
        }

        public List<string> SelectAllPetTypeID()
        {
            throw new NotImplementedException();
        }

        public List<PetType> RetrievetAllPetTypes(string status)
        {
            return petType;
        }
    }
}
