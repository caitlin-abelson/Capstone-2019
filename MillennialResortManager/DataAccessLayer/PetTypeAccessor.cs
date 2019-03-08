using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class PetTypeAccessor : IPetTypeAccessor
    {
        /// <summary>
        ///  @Author Craig Barkley
        ///  @Created 2/10/2019
        ///  
        /// Class for the stored procedure data for Pet Types
        /// </summary>
        /// <param name="newPetType"></param>
        /// <returns></returns>
        /// 
        public  List<string> SelectAllPetTypeID()
        {
            var petTypes = new List<string>();

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_select_pet_type_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        petTypes.Add(read.GetString(0));
                    }
                }
                read.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return petTypes;
        }

        //DeletePetType(string petTypeID)
        /// <summary>
        /// Method that gets Pets by way of int petID.
        /// </summary>
        /// <param name="int petID">The ID of the Pet is used to get a list of pets.</param>
        /// <returns>Pets</returns>
        public int DeletePetType(string petTypeID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_delete_pet_type_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PetTypeID", petTypeID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        //CreatePetType(PetType newPetType)
        /// <summary>
        /// Method that Creates pets by way of newPetType.
        /// </summary>
        /// <param name="newPetType">The ID of the Pet is used to get a list of pets.</param>
        /// <returns>Rows</returns>
        public int CreatePetType(PetType newPetType)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_create_pet_type", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Parameters for new Event Request          
            cmd.Parameters.AddWithValue("@PetTypeID", newPetType.PetTypeID);
            cmd.Parameters.AddWithValue("@Description", newPetType.Description);
            

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }


            return rows;
        }

        //RetrievetAllPetTypes(string status)
        /// <summary>
        /// Method that Retrieves pets by way of status.
        /// </summary>
        /// <param name="string status">The status of the Pet is used to get a list of pets and descriptions.</param>
        /// <returns>petTypes</returns>
        public List<PetType> RetrievetAllPetTypes(string status)
        {
            List<PetType> petTypes = new List<PetType>();

            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand("sp_retrieve_all_pet_type", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        petTypes.Add(new PetType()
                        {
                            PetTypeID = read.GetString(0),
                            Description = read.GetString(1)
                            
                        });

                    }
                }
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return petTypes;
        }
    }
}


