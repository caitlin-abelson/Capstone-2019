using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Kevin Broskow
    /// Created: 2019/01/20
    /// 
    /// Interface for the Product managers.
    /// </summary>
    public interface IProductManager
    {
        int CreateProduct(Product newProduct);
        void UpdateProduct(Product newProduct, Product oldProduct);
        Product RetrieveProduct(int productID);
        List<Product> RetrieveAllProducts();
        void DeactivateProduct(Product selectedProduct);
        void DeleteProduct(Product selectedProduct);
    }
}
