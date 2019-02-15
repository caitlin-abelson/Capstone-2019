using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ProductManagerTests
    {
        private List<Product> _products;
        private iProductManager _productManager;
        private ProductAccessorMock _prod;

        [TestInitialize]
        public void testSetupMSSQL()
        {
            _prod = new ProductAccessorMock();
            _productManager = new ProductManagerMSSQL(_prod);
            _products = new List<Product>();
            _products = _productManager.RetrieveAllProducts();
        }

        private string createString(int length)
        {
            string testLength = "";
            for (int i = 0; i < length; i++)
            {
                testLength += "*";
            }
            return testLength;
        }

        private void setProduct(Product old, Product newProduct)
        {
            newProduct.Name = old.Name;
            newProduct.ItemType = old.ItemType;
            newProduct.ProductID = old.ProductID;
            newProduct.Description = old.Description;
            newProduct.CustomerPurchasable = old.CustomerPurchasable;
            newProduct.DateActive = old.DateActive;
            newProduct.Active = old.Active;
            newProduct.OnHandQty = old.OnHandQty;
            newProduct.RecipeID = old.RecipeID;
            newProduct.ReorderQty = old.ReorderQty;
        }

        [TestMethod]
        public void TestRetrieveAllProducts()
        {
            //Arrange
            List<Product> products = null;
            //Act
            products = _productManager.RetrieveAllProducts();
            //Assert
            CollectionAssert.Equals(_products, products);
        }

        [TestMethod]
        public void TestCreateProductValidInput()
        {
            //arrange
            Product newProduct = new Product() { ItemType = "Food", Description = "Hi", OnHandQty = 3, Name = "Taco", ReorderQty = 1, DateActive = DateTime.Now, CustomerPurchasable = true, RecipeID = 100000 };
            //Act
            _productManager.CreateProduct(newProduct);
            //Assert
            _products = _productManager.RetrieveAllProducts();

            Assert.IsNotNull(_products.Find(x => x.ItemType == newProduct.ItemType &&
                x.Description == newProduct.Description && x.OnHandQty == newProduct.OnHandQty && x.Name == newProduct.Name &&
                x.ReorderQty == newProduct.ReorderQty && x.DateActive == newProduct.DateActive && x.CustomerPurchasable == newProduct.CustomerPurchasable &&
                x.RecipeID == newProduct.RecipeID
            ));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateProductInvalidInputItemTypeNull()
        {
            //Arrange
            Product newProduct = new Product() { ItemType = null, Description = "Hi", OnHandQty = 3, Name = "Taco", ReorderQty = 1, DateActive = DateTime.Now, CustomerPurchasable = true, RecipeID = 100000 };
            //Act
            _productManager.CreateProduct(newProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateProductInvalidInputItemTypeTooLong()
        {
            //Arrange
            Product newProduct = new Product() { ItemType = createString(16), Description = "Hi", OnHandQty = 3, Name = "Taco", ReorderQty = 1, DateActive = DateTime.Now, CustomerPurchasable = true, RecipeID = 100000 };
            //Act
            _productManager.CreateProduct(newProduct);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateProductInvalidInputDescriptionTooLong()
        {
            //Arrange
            Product newProduct = new Product() { ItemType = "Food", Description = createString(1001), OnHandQty = 3, Name = "Taco", ReorderQty = 1, DateActive = DateTime.Now, CustomerPurchasable = true, RecipeID = 100000 };
            //Act
            _productManager.CreateProduct(newProduct);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateProductInvalidInputNameNull()
        {
            //Arrange
            Product newProduct = new Product() { ItemType = "Food", Description = "Hi", OnHandQty = 3, Name = null, ReorderQty = 1, DateActive = DateTime.Now, CustomerPurchasable = true, RecipeID = 100000 };
            //Act
            _productManager.CreateProduct(newProduct);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateProductInvalidInputNameEmptyString()
        {
            //Arrange
            Product newProduct = new Product() { ItemType = "Food", Description = "Hi", OnHandQty = 3, Name = "", ReorderQty = 1, DateActive = DateTime.Now, CustomerPurchasable = true, RecipeID = 100000 };
            //Act
            _productManager.CreateProduct(newProduct);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateProductInvalidInputNameTooLong()
        {
            //Arrange
            Product newProduct = new Product() { ItemType = "Food", Description = "Hi", OnHandQty = 3, Name = createString(51), ReorderQty = 1, DateActive = DateTime.Now, CustomerPurchasable = true, RecipeID = 100000 };
            //Act
            _productManager.CreateProduct(newProduct);
        }


        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 2/11/2019
        /// Here starts the RetrieveProduct Unit Tests
        /// </summary>
      
        [TestMethod]
        public void TestRetrieveProductValidInput()
        {
            //Arrange
            Product newProduct = new Product();
            //Act
            newProduct = _productManager.RetrieveProduct(_products[0].ProductID);
            //Assert
            Assert.AreEqual(newProduct.ProductID, _products[0].ProductID);
            Assert.AreEqual(newProduct.ItemType, _products[0].ItemType);
            Assert.AreEqual(newProduct.RecipeID, _products[0].RecipeID);
            Assert.AreEqual(newProduct.CustomerPurchasable, _products[0].CustomerPurchasable);
            Assert.AreEqual(newProduct.Description, _products[0].Description);
            Assert.AreEqual(newProduct.OnHandQty, _products[0].OnHandQty);
            Assert.AreEqual(newProduct.Name, _products[0].Name);
            Assert.AreEqual(newProduct.ReorderQty, _products[0].ReorderQty);
            Assert.AreEqual(newProduct.DateActive, _products[0].DateActive);
            Assert.AreEqual(newProduct.Active, _products[0].Active);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveProductInvalidInput()
        {
            //Arrange
            Product newProduct = new Product();
            int invalidProductID = -1;
            //Act
            newProduct = _productManager.RetrieveProduct(invalidProductID);
        }


        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 2/11/2019
        /// Here starts the UpdateProduct Unit Tests
        /// </summary>

        [TestMethod]
        public void TestUpdateProductValidInput()
        {
            //Arrange
            Product newProduct = new Product();
            setProduct(_products[0], newProduct);
            string newDescription = "This test is updating the description in TestUpdateProductValidInput";
            newProduct.Description = newDescription;
            //Act
            _productManager.UpdateProduct(newProduct, _products[0]);
            //Assert
            _products = _productManager.RetrieveAllProducts();
            Assert.AreEqual(_productManager.RetrieveProduct(_products[0].ProductID).Description, newProduct.Description);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateProductInValidInputDescription()
        {
            //Arrange
            Product newProduct = new Product();
            setProduct(_products[0], newProduct);
            string newDescription = "This test is updating the description in TestUpdateProductValidInput" + createString(1000);
            newProduct.Description = newDescription;
            //Act
            _productManager.UpdateProduct(newProduct, _products[0]);
            //Assert
            _products = _productManager.RetrieveAllProducts();
            Assert.AreEqual(_productManager.RetrieveProduct(_products[0].ProductID).Description, newProduct.Description);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateProductInValidInputItemTypeNull()
        {
            //Arrange
            Product newProduct = new Product();
            setProduct(_products[0], newProduct);
            newProduct.ItemType = null;
            //Act
            _productManager.UpdateProduct(newProduct, _products[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateProductInValidInputItemTypeEmptyString()
        {
            //Arrange
            Product newProduct = new Product();
            setProduct(_products[0], newProduct);
            newProduct.ItemType = "";
            //Act
            _productManager.UpdateProduct(newProduct, _products[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateProductInValidInputItemTypeTooLong()
        {
            //Arrange
            Product newProduct = new Product();
            setProduct(_products[0], newProduct);
            newProduct.ItemType = createString(16);
            //Act
            _productManager.UpdateProduct(newProduct, _products[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateProductInValidInputNameNull()
        {
            //Arrange
            Product newProduct = new Product();
            setProduct(_products[0], newProduct);
            newProduct.Name = null;
            //Act
            _productManager.UpdateProduct(newProduct, _products[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateProductInValidInputNameEmptyString()
        {
            //Arrange
            Product newProduct = new Product();
            setProduct(_products[0], newProduct);
            newProduct.Name = "";
            //Act
            _productManager.UpdateProduct(newProduct, _products[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateProductInValidInputNameTooLong()
        {
            //Arrange
            Product newProduct = new Product();
            setProduct(_products[0], newProduct);
            newProduct.Name = createString(51);
            //Act
            _productManager.UpdateProduct(newProduct, _products[0]);
        }

        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 2/11/2019
        /// Here starts the DeactivateProduct Unit Tests
        /// </summary>
        [TestMethod]
        public void TestDeactivateProductValid()
        {
            //Arrange
            Product validProduct = _products[0];
            //Act
            _productManager.DeactivateProduct(validProduct);
            //Assert
            Assert.IsFalse(_productManager.RetrieveProduct(validProduct.ProductID).Active);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateProductInvalid()
        {
            //Arrange
            Product invalidProduct = _products[0];
            invalidProduct.ProductID = -1;
            //Act
            _productManager.DeactivateProduct(invalidProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteProductValid()
        {
            //Arrange
            Product invalidProduct = _products[0];
            invalidProduct.ProductID = -1;
            //Act
            _productManager.DeleteProduct(invalidProduct);
        }
    }
}
