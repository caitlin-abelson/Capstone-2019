using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
	/// Kevin Broskow
	/// Created: 2019/01/20
	/// 
	/// Interface for accessing Product Data
	/// </summary>
    public interface iProductAccessor
    {
        int InsertProduct(Product newProduct);
        Product RetrieveProduct(int productID);
        List<Product> RetrieveAllProducts();
        void UpdateProduct(Product newProduct, Product oldProduct);
        void DeactivateProduct(Product deactivatingProduct);
        void DeleteProduct(Product purgingProduct);
        List<Product> RetrieveActiveProducts();
        List<Product> RetrieveDeactiveProducts();
    }
}
