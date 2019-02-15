using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class ProductAccessorMock : iProductAccessor
    {
        private List<Product> _products;
        public ProductAccessorMock()
        {
            _products = new List<Product>();
            _products.Add(new Product() { ProductID = 100000, ItemType = "Food", RecipeID = 10000, CustomerPurchasable = true, Description = "This is a test description", OnHandQty = 2, Name = "Taco", ReorderQty = 5, DateActive = DateTime.Now, Active = true });
            _products.Add(new Product() { ProductID = 100001, ItemType = "Food", RecipeID = 10000, CustomerPurchasable = true, Description = "This is a test description", OnHandQty = 2, Name = "Burrito", ReorderQty = 5, DateActive = DateTime.Now, Active = true });
            _products.Add(new Product() { ProductID = 100002, ItemType = "Food", RecipeID = 10000, CustomerPurchasable = true, Description = "This is a test description", OnHandQty = 2, Name = "Nacho", ReorderQty = 5, DateActive = DateTime.Now, Active = true });
            _products.Add(new Product() { ProductID = 100003, ItemType = "Food", RecipeID = 10000, CustomerPurchasable = true, Description = "This is a test description", OnHandQty = 2, Name = "Tbone Steak", ReorderQty = 5, DateActive = DateTime.Now, Active = true });
            _products.Add(new Product() { ProductID = 100004, ItemType = "Food", RecipeID = 10000, CustomerPurchasable = true, Description = "This is a test description", OnHandQty = 2, Name = "Mashed Potatoes", ReorderQty = 5, DateActive = DateTime.Now, Active = true });
        }
        public void DeactivateProduct(Product deactivatingProduct)
        {
            Product p = null;
            p = _products.Find(x => x.ProductID == deactivatingProduct.ProductID);
            if (p == null || p.ProductID == -1)
            {
                throw new ArgumentException("Product not found in system");
            }
            else
            {
                p.Active = false;
            }
        }

        public void DeleteProduct(Product purgingProduct)
        {
            bool foundProduct = true;
            foreach (var product in _products)
            {
                if (product.ProductID == purgingProduct.ProductID)
                {
                    product.Active = false;
                    foundProduct = false;

                    _products.Remove(_products.Find(x => x.ProductID == purgingProduct.ProductID));
                    break;
                }

            }
            if (!foundProduct)
            {
                throw new ArgumentException("No product was found in the system");
            }
        }

        public int InsertProduct(Product newProduct)
        {
            _products.Add(newProduct);
            return newProduct.ProductID;
        }

        public List<Product> RetrieveActiveProducts()
        {
            throw new NotImplementedException();
        }

        public List<Product> RetrieveAllProducts()
        {
            return _products;
        }

        public List<Product> RetrieveDeactiveProducts()
        {
            throw new NotImplementedException();
        }

        public Product RetrieveProduct(int productID)
        {
            Product p = new Product();
            p = _products.Find(x => x.ProductID == productID);
            if (p == null)
            {
                throw new ArgumentException("ProductID did not match any Product in the system");
            }
            return p;
        }

        public void UpdateProduct(Product newProduct, Product oldProduct)
        {
            foreach (var product in _products)
            {
                if(product.ProductID == oldProduct.ProductID)
                {
                    product.ItemType = newProduct.ItemType;
                    product.RecipeID = newProduct.RecipeID;
                    product.CustomerPurchasable = newProduct.CustomerPurchasable;
                    product.Description = newProduct.Description;
                    product.OnHandQty = newProduct.OnHandQty;
                    product.Name = newProduct.Name;
                    product.ReorderQty = newProduct.ReorderQty;
                }
            }
        }
        
    }
}
