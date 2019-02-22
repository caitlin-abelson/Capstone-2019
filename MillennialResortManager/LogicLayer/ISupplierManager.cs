/// <summary>
/// Caitlin Abelson
/// Created: 1/22/19
/// 
/// This is the interface for the Suppliers that provide goods and services for the resort.
/// </summary>

using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ISupplierManager
    {
        void CreateSupplier(Supplier newSupplier);
        Supplier RetrieveSupplier(int id);
        List<Supplier> RetrieveAllSuppliers();
        void UpdateSupplier(Supplier newSupplier, Supplier oldSupplier);
        void DeleteSupplier(Supplier supplier);

        void DeactivateSupplier(Supplier supplier);

    }
}
