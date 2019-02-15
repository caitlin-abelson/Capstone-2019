using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using System.Collections;

namespace LogicLayer
{
    /// <summary>
	/// Kevin Broskow
	/// Created: 2019/01/20
	/// 
	///Manages products using MSSQL 
	/// </summary>
    public class ProductManagerMSSQL : iProductManager
    {
        private iProductAccessor _productAccessor;

        public ProductManagerMSSQL()
        {
            _productAccessor = new ProductAccessorMSSQL();
        }

        public ProductManagerMSSQL(ProductAccessorMock productAccessor)
        {
            _productAccessor = productAccessor;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/23
        /// 
        /// Method used to add a product to the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="newProduct">A fully created product data object to be inserted into the database</param>
        /// <returns>void</returns>	
        public int CreateProduct(Product newProduct)
        {
            int productID;
            try
            {
                if (!newProduct.IsValid())
                {
                    throw new ArgumentException("Data for this product is invalid");
                }
                 productID = _productAccessor.InsertProduct(newProduct);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return productID;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/23
        /// 
        /// Method used to deactivate a product
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="selectedProduct">The product to be deactivated in the database</param>
        /// <returns>void</returns>	
        public void DeactivateProduct(Product selectedProduct)
        {
            try
            {
                _productAccessor.DeactivateProduct(selectedProduct);
            }
            catch ( Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/23
        /// 
        /// Method used to delete a product from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="selectedProduct">The product to be deleted from the database</param>
        /// <returns>void</returns>	
        public void DeleteProduct(Product selectedProduct)
        {
            try
            {
                _productAccessor.DeleteProduct(selectedProduct);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/30
        /// 
        /// Used to edit a specific product record in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="newProduct">A data object of type Product to be update</param>
        /// <param name="oldProduct">A data object of type Product to assure there are no concurrency issues</param>
        /// <returns>Void</returns>	
        public void UpdateProduct(Product newProduct, Product oldProduct)
        {
            try
            {
                if (!newProduct.IsValid())
                {
                    throw new ArgumentException("Data for this product is invalid");
                }
                _productAccessor.UpdateProduct(newProduct, oldProduct);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/30
        /// 
        /// Used to retrieve all active Products from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <returns>List<Product></returns>
        public List<Product> RetrieveActiveProducts()
        {
            List<Product> activeProducts = new List<Product>();
            try
            {
                activeProducts = _productAccessor.RetrieveActiveProducts();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return activeProducts;
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/30
        /// 
        /// Used to retrieve all Products from database.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <returns>List<Product></returns>	
        public List<Product> RetrieveAllProducts()
        {
            List<Product> allProducts = new List<Product>();
            try
            {
                allProducts = _productAccessor.RetrieveAllProducts();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return allProducts;
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/30
        /// 
        /// Used to retrieve all deactive Products from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <returns>List<Product></returns>	
        public List<Product> RetrieveDeactiveProducts()
        {
            List<Product> deactiveProducts = new List<Product>();
            try
            {
                deactiveProducts = _productAccessor.RetrieveDeactiveProducts();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return deactiveProducts;
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/30
        /// 
        /// Used to retrieve a specific Product from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="productID">The unique identifier for a product in the database</param>
        /// <returns>Void</returns>	
        public Product RetrieveProduct(int productID)
        {
            Product product;
            try
            {
                product = _productAccessor.RetrieveProduct(productID);
            }
            catch (Exception)
            {
                throw new ArgumentException("ProductID did not match and Product in our system");
            }
            return product;
        }
    }
}
