using System;
using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer
{
    interface ISupplyAccessor
    {
        List<Suppliers> RetrieveAllSuppliers();
        void CreateSupplier(Suppliers newSupplier);
        void UpdateSupplier(Suppliers newSupplier, Suppliers oldSuppliers);
        Suppliers RetrieveSupplier();
        void PurgeSupplier();
        void DeactiveSupplier();
    }
}
