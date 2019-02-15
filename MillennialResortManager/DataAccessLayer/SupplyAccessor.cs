using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;


namespace DataAccessLayer
{
    /// <summary>
    /// Caitlin Abelson
    /// Created: 1/21/19
    /// 
    /// The SupplyAccessor class implements the ISuppyAccessor interface which holds all of the methods needed
    /// the CRUD functions of a Supplier when accessing the database.
    /// </summary>
    public class SupplyAccessor : ISupplyAccessor
    {
        public SupplyAccessor()
        {

        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/22/19
        /// 
        /// RetrieveAllSuppliers reads in the data objects from the stored procedure sp_retrieve_suppliers
        /// so that they can be listed onto a data grid.
        /// </summary>
        /// <returns></returns>
        /// 
        public List<Suppliers> RetrieveAllSuppliers()
        {
            List<Suppliers> suppliers = new List<Suppliers>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_suppliers";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Suppliers supplier = new Suppliers();
                        supplier.Name = reader.GetString(0);
                        supplier.ContactFirstName = reader.GetString(1);
                        supplier.ContactLastName = reader.GetString(2);
                        supplier.PhoneNumber = reader.GetString(3);
                        supplier.SupplierEmail = reader.GetString(4);
                        supplier.DateAdded = reader.GetDateTime(5);
                        supplier.Address = reader.GetString(6);
                        supplier.City = reader.GetString(7);
                        supplier.State = reader.GetString(8);
                        supplier.Country = reader.GetString(9);
                        supplier.ZipCode = reader.GetString(10);
                        supplier.Description = reader.GetString(11);
                        supplier.Active = reader.GetBoolean(12);
                        suppliers.Add(supplier);
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

            return suppliers;


        }



        public void CreateSupplier(Suppliers newSupplier)
        {
            throw new NotImplementedException();
        }

        public void UpdateSupplier(Suppliers newSupplier, Suppliers oldSuppliers)
        {
            throw new NotImplementedException();
        }

        public Suppliers RetrieveSupplier()
        {
            throw new NotImplementedException();
        }

        public void PurgeSupplier()
        {
            throw new NotImplementedException();
        }

        public void DeactiveSupplier()
        {
            throw new NotImplementedException();
        }
    }
}
