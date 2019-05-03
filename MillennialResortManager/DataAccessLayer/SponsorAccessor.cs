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
    /// <summary>
    /// Author: Gunardi Saputra
    /// Created : 2019/02/20
    /// The SponsorAccessor is an implementation of the ISponsorAccessor and 
    /// is designed to access data from a MSSQL database 
    /// </summary>
    public class SponsorAccessor : ISponsorAccessor
    {
        public SponsorAccessor()
        {

        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 03/06/2019
        /// 
        /// The InsertSponsor method is for inserting a new sponsor into our records.
        /// 
        /// Updated: 04/19/2019
        /// Remove statusID
        /// </summary>
        /// <param name="newSpsonsor"></param>
        /// <returns></returns>
        public void InsertSponsor(Sponsor newSponsor)
        {

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_insert_sponsor";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            //cmd.Parameters.AddWithValue("@SponsorID", newSponsor.SponsorID);
            cmd.Parameters.AddWithValue("@Name", newSponsor.Name);
            cmd.Parameters.AddWithValue("@Address", newSponsor.Address);
            cmd.Parameters.AddWithValue("@City", newSponsor.City);
            cmd.Parameters.AddWithValue("@State", newSponsor.State);
            cmd.Parameters.AddWithValue("@PhoneNumber", newSponsor.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", newSponsor.Email);
            cmd.Parameters.AddWithValue("@ContactFirstName", newSponsor.ContactFirstName);
            cmd.Parameters.AddWithValue("@ContactLastName", newSponsor.ContactLastName);
            //cmd.Parameters.AddWithValue("@DateAdded", newSponsor.DateAdded);
            //cmd.Parameters.AddWithValue("@Active", newSponsor.Active);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
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
        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 03/06/2019
        /// 
        /// The SelectAllSponsorStatus() 
        /// 
        /// Updated: 04/19/2019
        /// Remove statusID
        /// </summary>
        /// <param name="newSpsonsor"></param>
        /// <returns></returns>

        //public List<string> SelectAllSponsorStatus()
        //{
        //    List<string> sponsorStatus = new List<string>();
        //    var conn = DBConnection.GetDbConnection();
        //    var cmd = new SqlCommand("sp_retrieve_all_statusid", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    try
        //    {
        //        conn.Open();
        //        var r = cmd.ExecuteReader();
        //        if (r.HasRows)
        //        {
        //            while (r.Read())
        //            {
        //                sponsorStatus.Add(r.GetString(0));
        //            }
        //        }
        //        r.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return sponsorStatus;
        //}

        public List<string> SelectAllStates()
        {
            List<string> allStates = new List<string>();
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_all_states", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        allStates.Add(r.GetString(0));
                    }
                }
                r.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return allStates;
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2/2/19
        /// 
        /// The DeactiveSponsor deactivates a sponsor from the database.
        /// </summary>
        /// <param name="SponsorID"></param>
        public void DeactivateSponsor(int SponsorID)
        {
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_deactivate_sponsor";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@SponsorID", SponsorID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
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
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/20
        /// 
        /// The DeleteSponsor deletes an inactive sponsor from the database.
        /// </summary>
        /// <param name="SponsorID"></param>
        public void DeleteSponsor(int SponsorID)
        {
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_delete_sponsor";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@SponsorID", SponsorID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
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
        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/20
        /// 
        /// This method reads all of the sponsors that are in the database.
        /// </summary>
        /// <returns></returns>
        public List<Sponsor> SelectAllSponsors()
        {
            List<Sponsor> sponsors= new List<Sponsor>();

            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_select_all_sponsors";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader2 = cmd.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        sponsors.Add(new Sponsor()
                        {
                            SponsorID = reader2.GetInt32(0),
                            Name = reader2.GetString(1),
                            Address = reader2.GetString(2),
                            City = reader2.GetString(3),
                            State = reader2.GetString(4),
                            PhoneNumber = reader2.GetString(5),
                            Email = reader2.GetString(6),
                            ContactFirstName = reader2.GetString(7),
                            ContactLastName = reader2.GetString(8),
                            DateAdded = reader2.GetDateTime(9),
                            Active = reader2.GetBoolean(10)
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
            return sponsors;
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/03/05
        /// 
        /// The UpdateSponsor updates a sponsor's information
        /// 
        /// Updated: 04/19/2019
        /// Remove statusID
        /// </summary>
        /// <param name="newSponsor"></param>
        /// <param name="oldSponsor"></param>
        public int UpdateSponsor(Sponsor oldSponsor, Sponsor newSponsor)
        {
            int rowsAffected = 0;
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_update_sponsor";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@SponsorID", oldSponsor.SponsorID);

            cmd.Parameters.AddWithValue("@OldName", oldSponsor.Name);
            cmd.Parameters.AddWithValue("@OldAddress", oldSponsor.Address);
            cmd.Parameters.AddWithValue("@OldCity", oldSponsor.City);
            cmd.Parameters.AddWithValue("@OldState", oldSponsor.State);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldSponsor.PhoneNumber);
            cmd.Parameters.AddWithValue("@OldEmail", oldSponsor.Email);
            cmd.Parameters.AddWithValue("@OldContactFirstName", oldSponsor.ContactFirstName);
            cmd.Parameters.AddWithValue("@OldContactLastName", oldSponsor.ContactLastName);
            //cmd.Parameters.AddWithValue("@OldStatusID", oldSponsor.StatusID);
            cmd.Parameters.AddWithValue("@OldDateAdded", oldSponsor.DateAdded);
            cmd.Parameters.AddWithValue("@OldActive", oldSponsor.Active);

            cmd.Parameters.AddWithValue("@Name", newSponsor.Name);
            cmd.Parameters.AddWithValue("@Address", newSponsor.Address);
            cmd.Parameters.AddWithValue("@City", newSponsor.City);
            cmd.Parameters.AddWithValue("@State", newSponsor.State);
            cmd.Parameters.AddWithValue("@PhoneNumber", newSponsor.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", newSponsor.Email);
            cmd.Parameters.AddWithValue("@ContactFirstName", newSponsor.ContactFirstName);
            cmd.Parameters.AddWithValue("@ContactLastName", newSponsor.ContactLastName);
            //cmd.Parameters.AddWithValue("@StatusID", newSponsor.StatusID);
            cmd.Parameters.AddWithValue("@DateAdded", newSponsor.DateAdded);
            cmd.Parameters.AddWithValue("@Active", newSponsor.Active);

            try
            {
                // open the connection
                conn.Open();

                // execute the command
                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/03/05
        /// 
        /// The SelectSponsor retrieve a sponsor's information
        /// 
        /// Updated: 04/19/2019
        /// Remove statusID
        /// </summary>

        public Sponsor SelectSponsor(int SponsorID)
        {
            Sponsor sponsor = new Sponsor();
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_select_sponsor";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@SponsorID", SponsorID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sponsor.SponsorID = reader.GetInt32(0);
                        sponsor.Name = reader.GetString(1);
                        sponsor.Address = reader.GetString(2);
                        sponsor.City = reader.GetString(3);
                        sponsor.State= reader.GetString(4);
                        sponsor.PhoneNumber = reader.GetString(5);
                        sponsor.Email = reader.GetString(6);
                        sponsor.ContactFirstName = reader.GetString(7);
                        sponsor.ContactLastName= reader.GetString(8);
                        //sponsor.StatusID = reader.GetString(9);
                        sponsor.DateAdded = reader.GetDateTime(9);
                        sponsor.Active = reader.GetBoolean(10);
                        break;
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
            return sponsor;
        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/03/05
        /// 
        /// The SelectAllVMSponsor retrieve view model sponsor's information
        /// 
        /// Updated: 04/19/2019
        /// Remove statusID
        /// </summary>
        public List<BrowseSponsor> SelectAllVMSponsors()
        {
            List<BrowseSponsor> sponsors= new List<BrowseSponsor>();

            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_retrieve_all_view_model_sponsors";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BrowseSponsor sponsor = new BrowseSponsor();
                        sponsor.SponsorID = reader.GetInt32(0);
                        sponsor.Name = reader.GetString(1);
                        sponsor.Address = reader.GetString(2);
                        sponsor.City= reader.GetString(3);
                        sponsor.State= reader.GetString(4);
                        sponsor.PhoneNumber= reader.GetString(5);
                        sponsor.Email = reader.GetString(6);
                        sponsor.ContactFirstName= reader.GetString(7);
                        sponsor.ContactLastName= reader.GetString(8);
                        //sponsor.StatusID= reader.GetString(9);
                        sponsor.DateAdded = reader.GetDateTime(9);
                        sponsor.Active = reader.GetBoolean(10);
                        sponsors.Add(sponsor);
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
            return sponsors;
        }

    }
}