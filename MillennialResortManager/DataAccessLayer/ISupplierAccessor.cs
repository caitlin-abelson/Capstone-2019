using System;
using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface ISupplierAccessor
    {
        int ActivateSupplier(Supplier supplier);
        void InsertSupplier(Supplier newSupplier);
        Supplier SelectSupplier(int id);
        List<Supplier> SelectAllSuppliers();
        void UpdateSupplier(Supplier newSupplier, Supplier oldSuppliers);
        int DeleteSupplier(Supplier supplier);
        int DeactivateSupplier(Supplier supplier);
    }
}
