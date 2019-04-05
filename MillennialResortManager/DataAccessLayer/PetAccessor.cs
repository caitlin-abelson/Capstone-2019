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
    public class PetAccessor : IPetAccessor
    {

        /// <summary>
        ///  @Author Craig Barkley
        ///  @Created 2/07/2019
        /// <param name="newPet"></param>
        /// 
        /// Class for the stored procedure data for Pet Access
        /// </summary>
        //For creating a new Pet
        public int InsertPet(Pet newPet)
        {
            int newPetID = 0; // Edited on 3/17/19 by Matt Hill.

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_insert_pet", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Parameters for new Event Request
            cmd.Parameters.AddWithValue("@PetName", newPet.PetName);
            cmd.Parameters.AddWithValue("@Gender", newPet.Gender);
            cmd.Parameters.AddWithValue("@Species", newPet.Species);
            cmd.Parameters.AddWithValue("@PetTypeID", newPet.PetTypeID);
            cmd.Parameters.AddWithValue("@GuestID", newPet.GuestID);

            try
            {
                conn.Open();
                newPetID = Convert.ToInt32(cmd.ExecuteScalar()); // Edited on 3/17/19 by Matt H.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }


            return newPetID;
        }
        
        //SelectAllPets()
        /// <summary>
        /// Method that Selects All Pet Types to a list.
        /// </summary>
        /// <param name="">The ID of the Appointment Type are retrieved.</param>
        /// <returns> Pets </returns>
        public List<Pet> SelectAllPets()
        {
            List<Pet> Pets = new List<Pet>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_all_pets";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        Pets.Add(new Pet()
                        {
                            PetID = read.GetInt32(0),
                            PetName = read.GetString(1),
                            Gender = read.GetString(2),
                            Species = read.GetString(3),
                            PetTypeID = read.GetString(4),
                            GuestID = read.GetInt32(5)
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

            return Pets;
        }

        //UpdatePet(Pet newPet, Pet oldPet)
        /// <summary>
        /// Method that Updates oldPet and newPet details.
        /// </summary>
        /// <param name="Pet oldPet, Pet newPet">The ID of the Pet oldPet, Pet newPet are updated.</param>
        /// <returns>rows</returns>
        public int UpdatePet(Pet oldPet, Pet newPet)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_update_pet", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Parameters for new Event Request
            cmd.Parameters.AddWithValue("@PetID", oldPet.PetID);

            cmd.Parameters.AddWithValue("@newPetName", newPet.PetName);
            cmd.Parameters.AddWithValue("@newGender", newPet.Gender);
            cmd.Parameters.AddWithValue("@newSpecies", newPet.Species);
            cmd.Parameters.AddWithValue("@newPetTypeID", newPet.PetTypeID);
            cmd.Parameters.AddWithValue("@newGuestID", newPet.GuestID);

            cmd.Parameters.AddWithValue("@oldPetName", oldPet.PetName);
            cmd.Parameters.AddWithValue("@oldGender", oldPet.Gender);
            cmd.Parameters.AddWithValue("@oldSpecies", oldPet.Species);
            cmd.Parameters.AddWithValue("@oldPetTypeID", oldPet.PetTypeID);
            cmd.Parameters.AddWithValue("@oldGuestID", oldPet.GuestID);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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

        //SelectAllPets(int petID)
        /// <summary>
        /// Method that gets Pets by way of int petID.
        /// </summary>
        /// <param name="int petID">The ID of the Pet is used to get a list of pets.</param>
        /// <returns>Pets</returns>
        public List<Pet> SelectAllPets(int petID)
        {
            List<Pet> Pet = new List<Pet>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_pet_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@PetID", petID);

            try
            {
                conn.Open();

                var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        Pet.Add(new Pet()
                        {
                          //PetID = read.GetInt32(0),
                            PetName = read.GetString(1),
                            Gender = read.GetString(2),
                            Species = read.GetString(3),
                            PetTypeID = read.GetString(4),
                            GuestID = read.GetInt32(5)                            
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

            return Pet;
        }

        //DeletePet(int PetID)
        //SelectPetByID(int PetID)
        /// <summary>
        /// Method that Deletes Pets by way of int petID.
        /// </summary>
        /// <param name="int petID">The ID of the Pet is used to get a list of pets.</param>
        /// <returns>Rows</returns>
        public int DeletePet(int PetID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_delete_pet", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PetID", PetID);

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

        ///  @Author Matthew Hill
        ///  @Created 3/10/19
        //RetrievePetImageFilenameByPetID(int petID)
        /// <summary>
        /// Method that retrieves a filename by petID
        /// </summary>
        /// <param name="int petID">The ID of the Pet is used to get a filename</param>
        /// <returns>Filename</returns>
        public string RetrievePetImageFilenameByPetID(int petID)
        {
            string filename;

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_select_pet_image_filename_by_pet_id", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PetID", petID);

            try
            {
                conn.Open();
                filename = (string)cmd.ExecuteScalar();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return filename;
        }

        ///  @Author Matthew Hill
        ///  @Created 3/10/19
        //CreatePetImageFilename(string filename, int petID)
        /// <summary>
        /// Method that creates a new Pet Image filename record.
        /// </summary>
        /// <param name="string filename, int petID">The filename to insert into the database and pet id to associate the image with.</param>
        /// <returns>Filename</returns>
        public int CreatePetImageFilename(string filename, int petID)
        {
            int rows;

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_insert_pet_image_filename", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Filename", filename);
            cmd.Parameters.AddWithValue("@PetID", petID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        ///  @Author Matthew Hill
        ///  @Created 3/10/19
        //UpdatePetImageFilename(int petID, string oldFilename)
        /// <summary>
        /// Method that creates a new Pet Image filename record.
        /// </summary>
        /// <param name="string filename, int petID">The filename to insert into the database and pet id to associate the image with.</param>
        /// <returns>Filename</returns>
        public int UpdatePetImageFilename(int petID, string oldFilename, string newFilename)
        {
            int rows;

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_update_pet_image_filename", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PetID", petID);
            cmd.Parameters.AddWithValue("@OldFilename", oldFilename);
            cmd.Parameters.AddWithValue("@NewFilename", newFilename);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }











    }
}
