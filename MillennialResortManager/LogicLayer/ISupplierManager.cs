/// <summary>
/// Caitlin Abelson
/// Created: 1/22/19
/// 
/// This is the interface for the Suppliers that provide goods and services for the resort.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Author: Caitlin Abelson
    /// Created Date: 1/25/19
    /// 
    /// The ISupplierManager is the interface for Supplier and hold all the CRUD methods for the logic layer.
    /// </summary>
    interface ISupplierManager
    {
        bool AddSupplier(Suppliers newSupplier);
        void EditSupplier(Suppliers newSupplier, Suppliers oldSuppliers);
        Suppliers GetSupplier();
        List<Suppliers> GetAllSuppliers();
        void DeleteSupplier();

        
    }
}
