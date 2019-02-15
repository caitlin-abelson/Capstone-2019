/// <summary>
/// Caitlin Abelson
/// Created: 1/22/19
/// 
/// This class holds all of the CRUD methods and implements the interface iSupplierManager.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class SupplierManager : ISupplierManager
    {
        private SupplyAccessor _supplyAccessor;
        public SupplierManager()
        {
            _supplyAccessor = new SupplyAccessor();
        }
        public bool AddSupplier(Suppliers newSupplier)
        {
            throw new NotImplementedException();
        }

        public void DeleteSupplier()
        {
            throw new NotImplementedException();
        }

        public void EditSupplier(Suppliers newSupplier, Suppliers oldSuppliers)
        {
            throw new NotImplementedException();
        }


        public Suppliers RetrieveSupplier()
        {
            throw new NotImplementedException();
        }


        public Suppliers GetSupplier()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// 
        /// RetrieveAllSuppliers takes the list that was returned from the DataAccess Layer
        /// and pushes it to the presentation layer so that it can be displayed in the data grid
        /// </summary>
        /// <returns></returns>
        public List<Suppliers> GetAllSuppliers()
        {
            List<Suppliers> suppliers = null;

            try
            {
                suppliers = _supplyAccessor.RetrieveAllSuppliers();
            }
            catch (Exception)
            {
                throw;
            }

            return suppliers;
        }
    }
}
