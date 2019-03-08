using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// James Heim
    /// Created 2019/02/12
    /// 
    /// Mock Data Accessor for Supplier Unit Testing.
    /// </summary>
    public class SupplierAccessorMock : ISupplierAccessor
    {

        List<Supplier> _suppliers;

        /// <summary>
        /// James Heim
        /// Created 2019/02/12
        /// 
        /// Constructor to create mock data.
        /// </summary>
        public SupplierAccessorMock()
        {
            _suppliers = new List<Supplier>();
            
            

        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-08
        /// 
        /// Activate the supplier.
        /// </summary>
        /// <param name="supplier"></param>
        public int ActivateSupplier(Supplier supplier)
        {
            var selectedSupplier = _suppliers.Find(x => x.SupplierID == supplier.SupplierID);
            selectedSupplier.Active = true;

            return 1;
        }

        public void InsertSupplier(Supplier newSupplier)
        {
            _suppliers.Add(newSupplier);
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-23
        /// 
        /// Select the Supplier by its ID.
        /// </summary>
        public Supplier SelectSupplier(int id)
        {
            return _suppliers.Find(x => x.SupplierID == id);
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-23
        /// 
        /// Select all Suppliers.
        /// </summary>
        public List<Supplier> SelectAllSuppliers()
        {
            return _suppliers;
        }

        public void UpdateSupplier(Supplier newSupplier, Supplier oldSuppliers)
        {
            var index = _suppliers.FindIndex(x => x.SupplierID == newSupplier.SupplierID);

            _suppliers[index] = newSupplier;

        }


        /// <summary>
        /// Author James Heim
        /// Created 2019-02-23
        /// 
        /// Deactivate the Supplier.
        /// </summary>
        public int DeactivateSupplier(Supplier supplier)
        {
            supplier.Active = false;

            var index = _suppliers.FindIndex(x => x.SupplierID == supplier.SupplierID);
            _suppliers[index] = supplier;

            return 1;
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-23
        /// 
        /// Delete the Supplier.
        /// </summary>
        /// <param name="supplier"></param>
        public int DeleteSupplier(Supplier supplier)
        {
            var index = _suppliers.FindIndex(x => x.SupplierID == supplier.SupplierID);
            _suppliers.RemoveAt(index);

            return 1;
        }
    }
}
