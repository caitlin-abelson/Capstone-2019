using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayer.Tests
{
    /// <summary>
    /// Jared Greenfield
    /// Created: 2019/02/21
    /// 
    /// All tests of the RecipeManager class
    /// </summary>
    ///
    [TestClass]
    public class RecipeManagerTest
    {
        private IRecipeManager _recipeManager;
        private List<Recipe> _recipes;
        private RecipeAccessorMock _mock;

        [TestInitialize]
        public void TestSetup()
        {
            _mock = new RecipeAccessorMock();
            _recipeManager = new RecipeManager(_mock);
            _recipes = _recipeManager.RetrieveAllRecipes();
        }
        private string createLongString(int length)
        {
            string longString = "";
            for (int i = 0; i < length; i++)
            {
                longString += "I";
            }
            return longString;
        }

        // All deactivate tests

        [TestMethod]
        public void TestDeactivateRecipeValidInput()
        {
            //Arrange
            Recipe recipe = _recipes[0];
            //Act 
            bool result = _recipeManager.DeactivateRecipe(recipe.RecipeID);
            recipe = _recipeManager.RetrieveRecipeByID(recipe.RecipeID);
            //Assert
            Assert.IsTrue(result);
            Assert.IsFalse(recipe.Active);
        }

        [TestMethod]
        public void TestDeactivateRecipeValidInputIDNotFound()
        {
            //Arrange

            //Act 
            bool result = _recipeManager.DeactivateRecipe(-1);
            //Assert
            Assert.IsFalse(result);
        }

        // All Delete tests

        [TestMethod]
        public void TestDeleteRecipeValidInput()
        {
            //Arrange

            //Act 
            bool result = _recipeManager.DeleteRecipe(100000);
            Recipe recipe = _recipeManager.RetrieveRecipeByID(100000);
            //Assert
            Assert.IsTrue(result);
            Assert.IsNull(recipe);
        }

        [TestMethod]
        public void TestDeleteRecipeValidInputIDNotFound()
        {
            //Act
            bool result = _recipeManager.DeleteRecipe(1);
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestDeleteRecipeLinesValidInput()
        {
            //Arrange

            //Act 
            bool result = _recipeManager.DeleteRecipeLines(100000);
            Recipe recipe = _recipeManager.RetrieveRecipeByID(100000);
            //Assert
            Assert.IsTrue(result);
            Assert.IsNull(recipe.RecipeLines);
        }

        [TestMethod]
        public void TestDeleteRecipeLinesValidInputIDNotFound()
        {
            //Act
            bool result = _recipeManager.DeleteRecipeLines(1);
            //Assert
            Assert.IsFalse(result);
        }

        // All Create Tests

        [TestMethod]
        public void TestCreateRecipeValidInput()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe recipe = new Recipe(100010, "Test", "Test Recipe.", DateTime.Now, true, recipeLines);

            Item item =new Item(100000, null, false, null, "Food", "Test Recipe.", 0, "Test", 0, new DateTime(2008, 11, 11), true);

            Offering offering = new Offering(100000, "Item", 100000, "Test", (decimal)30, true);

            //Act 
            int id = _recipeManager.CreateRecipe(recipe, item, offering);
            Recipe newRecipe = _recipeManager.RetrieveRecipeByID(100010);

            //Assert
            Assert.AreEqual(100010, id);
            Assert.IsNotNull(newRecipe);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeInvalidInputRecipeNameNull()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe recipe = new Recipe(100010, null, "Test Recipe.", DateTime.Now, true, recipeLines);

            Item item = new Item(100000, null, false, null, "Food", "Test Recipe.", 0, "Test", 0, new DateTime(2008, 11, 11), true);

            Offering offering = new Offering(100000, "Item", 100000, "Test", (decimal)30, true);

            //Act 
            // Recipe Name cannot be null, so this should throw an exception
            int id = _recipeManager.CreateRecipe(recipe, item, offering);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeInvalidInputRecipeNameEmptyString()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe recipe = new Recipe(100010, "", "Test Recipe.", DateTime.Now, true, recipeLines);

            Item item = new Item(100000, null, false, null, "Food", "Test Recipe.", 0, "Test", 0, new DateTime(2008, 11, 11), true);

            Offering offering = new Offering(100000, "Item", 100000, "Test", (decimal)30, true);

            //Act 
            // Recipe Name cannot be an empty string, so this should throw an exception
            int id = _recipeManager.CreateRecipe(recipe, item, offering);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeInvalidInputRecipeNameTooLong()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe recipe = new Recipe(100010, createLongString(51), "Test Recipe.", DateTime.Now, true, recipeLines);

            Item item = new Item(100000, null, false, null, "Food", "Test Recipe.", 0, "Test", 0, new DateTime(2008, 11, 11), true);

            Offering offering = new Offering(100000, "Item", 100000, "Test", (decimal)30, true);

            //Act 
            // Recipe Name has a maximum of 50 characters, so this should throw an exception
            int id = _recipeManager.CreateRecipe(recipe, item, offering);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeInvalidInputDescriptionNameTooLong()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe recipe = new Recipe(100010, "Test", "Test Recipe." + createLongString(1000), DateTime.Now, true, recipeLines);

            Item item = new Item(100000, null, false, null, "Food", "Test Recipe.", 0, "Test", 0, new DateTime(2008, 11, 11), true);

            Offering offering = new Offering(100000, "Item", 100000, "Test", (decimal)30, true);

            //Act 
            // Description has a maximum of 1000 characters, so this should throw an exception
            int id = _recipeManager.CreateRecipe(recipe, item, offering);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeInvalidInputDateAddedTooEarly()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe recipe = new Recipe(100010, "Test", "Test Recipe.", new DateTime(1899, 12, 13), true, recipeLines);

            Item item = new Item(100000, null, false, null, "Food", "Test Recipe.", 0, "Test", 0, new DateTime(2008, 11, 11), true);

            Offering offering = new Offering(100000, "Item", 100000, "Test", (decimal)30, true);

            //Act 
            //Date must be after 1900, so this should throw an exception
            int id = _recipeManager.CreateRecipe(recipe, item, offering);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeInvalidInputInvalidRecipeLines()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun" + createLongString(1000), 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe recipe = new Recipe(100010, "Test", "Test Recipe.", DateTime.Now, true, recipeLines);

            Item item = new Item(100000, null, false, null, "Food", "Test Recipe.", 0, "Test", 0, new DateTime(2008, 11, 11), true);

            Offering offering = new Offering(100000, "Item", 100000, "Test", (decimal)30, true);

            //Act 
            int id = _recipeManager.CreateRecipe(recipe, item, offering);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeInvalidInputInvalidOffering()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe recipe = new Recipe(100010, "Test", "Test Recipe.", DateTime.Now, true, recipeLines);

            Item item = new Item(100000, null, true, null, "Food", "Test Recipe.", 0, "Test", 0, new DateTime(2008, 11, 11), true);

            Offering offering = new Offering(100000, "Item", 100000, "Test" + createLongString(1000), (decimal)30, true);

            //Act 
            int id = _recipeManager.CreateRecipe(recipe, item, offering);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeInvalidInputInvalidItem()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe recipe = new Recipe(100010, "Test", "Test Recipe.", DateTime.Now, true, recipeLines);

            Item item = new Item(100000, null, false, null, "Food" + createLongString(1000), "Test Recipe.", 0, "Test", 0, new DateTime(2008, 11, 11), true);

            Offering offering = new Offering(100000, "Item", 100000, "Test", (decimal)30, true);

            //Act
            int id = _recipeManager.CreateRecipe(recipe, item, offering);

        }

        [TestMethod]
        public void TestCreateRecipeLineValidInput()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");

            //Act 
            int rows = _recipeManager.CreateRecipeItemLine(burger, burger.RecipeID);
            Recipe recipe = _recipeManager.RetrieveRecipeByID(burger.RecipeID);
            //Assert
            Assert.AreEqual(1, rows);
            CollectionAssert.Contains(recipe.RecipeLines, burger);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeLineInvalidInputNameNull()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, null, 1, "Whole");

            //Act 
            //Name cannot be null, so this should throw an exception
            int rows = _recipeManager.CreateRecipeItemLine(burger, burger.RecipeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeLineInvalidInputNameEmptyString()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "", 1, "Whole");

            //Act 
            //Name cannot be an empty string, so this should throw an exception
            int rows = _recipeManager.CreateRecipeItemLine(burger, burger.RecipeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeLineInvalidInputNameTooLong()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty" + createLongString(50), 1, "Whole");

            //Act 
            //Name has a maximum of 50 characters, so this should throw an exception
            int rows = _recipeManager.CreateRecipeItemLine(burger, burger.RecipeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeLineInvalidInputQuantityNegative()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", -1, "Whole");

            //Act 
            //Name has a maximum of 50 characters, so this should throw an exception
            int rows = _recipeManager.CreateRecipeItemLine(burger, burger.RecipeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeLineInvalidInputUnitOfMeasureNull()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, null);

            //Act 
            //UnitOfMeasure of measure cannot be null, so this should throw an exception
            int rows = _recipeManager.CreateRecipeItemLine(burger, burger.RecipeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeLineInvalidInputUnitOfMeasureEmptyString()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "");

            //Act 
            //UnitOfMeasure of measure cannot be an empty string, so this should throw an exception
            int rows = _recipeManager.CreateRecipeItemLine(burger, burger.RecipeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeLineInvalidInputUnitOfMeasureTooLong()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole" + createLongString(25));

            //Act 
            //UnitOfMeasure of measure has a maximum of 25 characters, so this should throw an exception
            int rows = _recipeManager.CreateRecipeItemLine(burger, burger.RecipeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRecipeLineInvalidInputUnitOfMeasureContainsNumber()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "1 Whole");

            //Act 
            //UnitOfMeasure of measure has a maximum of 25 characters, so this should throw an exception
            int rows = _recipeManager.CreateRecipeItemLine(burger, burger.RecipeID);
        }

        // Reactivate Tests

        [TestMethod]
        public void TestReactivateRecipeValidInput ()
        {
            //Arrange
            _recipeManager.DeactivateRecipe(100000);
            //Act 
            bool result =_recipeManager.ReactivateRecipe(100000);
            Recipe recipe = _recipeManager.RetrieveRecipeByID(100000);
            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(recipe.Active);
        }
        [TestMethod]
        public void TestReactivateRecipeValidInputRecordNotDeactivated()
        {
            //Arrange
            //Act 
            bool result = _recipeManager.ReactivateRecipe(100000);

            // Assert
            Assert.IsFalse(result);
        }

        // Retrieve Tests
        
        [TestMethod]
        public void TestRetrieveAllRecipes()
        {
            //Arrange
            List<Recipe> recipes = null;
            //Act 
            recipes = _recipeManager.RetrieveAllRecipes();

            //Assert
            Assert.IsNotNull(recipes);
            CollectionAssert.AreEqual(_recipes, recipes);
        }

        [TestMethod]
        public void TestRetrieveRecipeByID()
        {
            //Arrange
            Recipe recipe = null;
            //Act 
            recipe = _recipeManager.RetrieveRecipeByID(_recipes[0].RecipeID);

            //Assert
            Assert.IsNotNull(recipe);
            Assert.AreEqual(_recipes[0], recipe);
        }

        [TestMethod]
        public void TestRetrieveRecipeByIDIdWithNoRecord()
        {
            //Arrange
            Recipe recipe = null;
            //Act 
            recipe = _recipeManager.RetrieveRecipeByID(-1);

            //Assert
            Assert.IsNull(recipe);
        }

        [TestMethod]
        public void TestRetrieveRecipeLinesByIDValidInput()
        {
            //Arrange
            List<RecipeItemLineVM> lines = null;
            //Act 
            lines = _recipeManager.RetrieveRecipeLinesByID(_recipes[0].RecipeID);

            //Assert
            Assert.IsNotNull(lines);
            Assert.AreEqual(_recipes[0].RecipeLines, lines);
        }

        [TestMethod]
        public void TestRetrieveRecipeLinesByIDValidInputIDWithNoRecord()
        {
            //Arrange
            List<RecipeItemLineVM> lines = null;
            //Act 
            lines = _recipeManager.RetrieveRecipeLinesByID(-1);

            //Assert
            Assert.IsNull(lines);
        }

        // All Update Tests

        [TestMethod]
        public void TestUpdateRecipeValidInput()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe recipe = new Recipe(100000, "Test", "Test Recipe.", new DateTime(2004, 12, 2), true, recipeLines);

            //Act 
            bool result = _recipeManager.UpdateRecipe(_recipes[0], recipe);
            _recipes = _recipeManager.RetrieveAllRecipes();
            Recipe updatedRecipe = _recipes.Find(x => x.RecipeID == recipe.RecipeID);

            //Assert
            Assert.AreEqual(recipe.RecipeID, updatedRecipe.RecipeID);
            Assert.AreEqual(recipe.Active, updatedRecipe.Active);
            Assert.AreEqual(recipe.DateAdded, updatedRecipe.DateAdded);
            Assert.AreEqual(recipe.Description, updatedRecipe.Description);
            Assert.AreEqual(recipe.Name, updatedRecipe.Name);
            CollectionAssert.AreEqual(recipe.RecipeLines, updatedRecipe.RecipeLines);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateRecipeInvalidInputRecipeNameNull()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe newRecipe = new Recipe(100000, null, "Test Recipe.", DateTime.Now, true, recipeLines);
            Recipe oldRecipe = _recipes.Find(x => x.RecipeID == newRecipe.RecipeID);

            //Act 
            // Recipe Name cannot be null, so this should throw an exception
            bool result = _recipeManager.UpdateRecipe(oldRecipe, newRecipe);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateRecipeInvalidInputRecipeNameEmptyString()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe newRecipe = new Recipe(100000, "", "Test Recipe.", DateTime.Now, true, recipeLines);
            Recipe oldRecipe = _recipes.Find(x => x.RecipeID == newRecipe.RecipeID);

            //Act 
            // Recipe Name cannot be an empty string, so this should throw an exception
            bool result = _recipeManager.UpdateRecipe(oldRecipe, newRecipe);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateRecipeInvalidInputRecipeNameTooLong()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe newRecipe = new Recipe(100000, createLongString(1001), "Test Recipe.", DateTime.Now, true, recipeLines);
            Recipe oldRecipe = _recipes.Find(x => x.RecipeID == newRecipe.RecipeID);

            //Act 
            // Recipe Name has a maximum of 50 characters, so this should throw an exception
            bool result = _recipeManager.UpdateRecipe(oldRecipe, newRecipe);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateRecipeInvalidInputDescriptionTooLong()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe newRecipe = new Recipe(100000, "Test", "Test Recipe." + createLongString(1000), DateTime.Now, true, recipeLines);
            Recipe oldRecipe = _recipes.Find(x => x.RecipeID == newRecipe.RecipeID);

            //Act 
            //Description has a maximum of 1000 characters, so this should throw an exception
            bool result = _recipeManager.UpdateRecipe(oldRecipe, newRecipe);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateRecipeInvalidInputDateAddedTooEarly()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe newRecipe = new Recipe(100000, "Test", "Test Recipe.", new DateTime(1899, 12, 13), true, recipeLines);
            Recipe oldRecipe = _recipes.Find(x => x.RecipeID == newRecipe.RecipeID);

            //Act 
            // Date Added must be after 1900, so this should throw an exception
            bool result = _recipeManager.UpdateRecipe(oldRecipe, newRecipe);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateRecipeInvalidInputLinesInvalid()
        {
            //Arrange
            List<RecipeItemLineVM> recipeLines = new List<RecipeItemLineVM>();
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun" + createLongString(1000), 1, "Whole");
            recipeLines.Add(burger);
            recipeLines.Add(bun);
            Recipe newRecipe = new Recipe(100000, "Test", "Test Recipe.", DateTime.Now, true, recipeLines);
            Recipe oldRecipe = _recipes.Find(x => x.RecipeID == newRecipe.RecipeID);

            //Act 
            // Recipe Name cannot be null, so this should throw an exception
            bool result = _recipeManager.UpdateRecipe(oldRecipe, newRecipe);

        }
    }
}
