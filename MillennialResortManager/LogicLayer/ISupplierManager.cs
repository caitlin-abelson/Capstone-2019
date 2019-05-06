using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="Caitlin Abelson" created="2019/01/22">
	/// This is the interface for the Suppliers that provide goods and services for the resort.
	/// </summary>
	public interface ISupplierManager
	{
		bool ActivateSupplier(Supplier supplier);
		void CreateSupplier(Supplier newSupplier);
		Supplier RetrieveSupplier(int id);
		List<Supplier> RetrieveAllSuppliers();
		void UpdateSupplier(Supplier newSupplier, Supplier oldSupplier);
		bool DeleteSupplier(Supplier supplier);
		bool DeactivateSupplier(Supplier supplier);
	}
}