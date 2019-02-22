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

        public void InsertSupplier(Supplier newSupplier)
        {
            _suppliers.Add(newSupplier);
        }

        public Supplier SelectSupplier(int id)
        {
            return _suppliers.Find(x => x.SupplierID == id);
        }

        public List<Supplier> SelectAllSuppliers()
        {
            return _suppliers;
        }

        public void UpdateSupplier(Supplier newSupplier, Supplier oldSuppliers)
        {
            var index = _suppliers.FindIndex(x => x.SupplierID == newSupplier.SupplierID);

            _suppliers[index] = newSupplier;

        }

        public void DeactivateSupplier(Supplier supplier)
        {
            supplier.Active = false;

            var index = _suppliers.FindIndex(x => x.SupplierID == supplier.SupplierID);
            _suppliers[index] = supplier;
        }

        public void DeleteSupplier(Supplier supplier)
        {
            var index = _suppliers.FindIndex(x => x.SupplierID == supplier.SupplierID);
            _suppliers.RemoveAt(index);
        }
    }
}
