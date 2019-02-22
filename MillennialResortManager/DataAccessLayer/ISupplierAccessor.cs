using System;
using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface ISupplierAccessor
    {
        void InsertSupplier(Supplier newSupplier);
        Supplier SelectSupplier(int id);
        List<Supplier> SelectAllSuppliers();
        void UpdateSupplier(Supplier newSupplier, Supplier oldSuppliers);
        void DeleteSupplier(Supplier supplier);
        void DeactivateSupplier(Supplier supplier);
    }
}
