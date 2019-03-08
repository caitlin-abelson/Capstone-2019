using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created : 02/20/2019
    /// This is a mock Data accessor which implements the IRecipeAccessor interface. It is used for testing purposes only
    /// </summary>
    public class RecipeAccessorMock : IRecipeAccessor
    {
        private List<Recipe> _recipes;

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// This constructor sets up all of our dummy data we will be using
        /// </summary>
        public RecipeAccessorMock()
        {
            _recipes = new List<Recipe>();
            _recipes.Add(new Recipe(100000, "Big Burger", "It's a large burger.", new DateTime(2004, 12, 2), true));
            _recipes.Add(new Recipe(100001, "Hot Dog", "It's a hot dog.", new DateTime(2004, 12, 2), true));
            _recipes.Add(new Recipe(100002, "Milkshake", "It's a milkshake.", new DateTime(2004, 12, 2), true));

            _recipes[0].RecipeLines = new List<RecipeItemLineVM>();
            _recipes[1].RecipeLines = new List<RecipeItemLineVM>();
            _recipes[2].RecipeLines = new List<RecipeItemLineVM>();
            
            RecipeItemLineVM burger = new RecipeItemLineVM(100000, 100000, "Patty", 1, "Whole");
            RecipeItemLineVM bun = new RecipeItemLineVM(100001, 100000, "Bun", 1, "Whole");
            _recipes[0].RecipeLines.Add(burger);
            _recipes[0].RecipeLines.Add(bun);

             bun = new RecipeItemLineVM(100000, 100001, "Bun", 1, "Whole");
            RecipeItemLineVM hotdog = new RecipeItemLineVM(100000, 100001, "Hotdog", 1, "Whole");
            _recipes[1].RecipeLines.Add(bun);
            _recipes[1].RecipeLines.Add(hotdog);

            RecipeItemLineVM milk = new RecipeItemLineVM(100000, 100002, "Milk", 1, "Gallon");
            RecipeItemLineVM chocolate = new RecipeItemLineVM(100000, 100002, "Chocolate", 1, "Gallon");
            _recipes[2].RecipeLines.Add(milk);
            _recipes[2].RecipeLines.Add(chocolate);
        }

        public int DeactivateRecipe(int recipeID)
        {
            int rowsAffected = 0;
            if (_recipes.Find(x => x.RecipeID == recipeID) != null)
            {
                _recipes.Find(x => x.RecipeID == recipeID).Active = false;
                rowsAffected = 1;
            }
            return rowsAffected;
        }

        public int DeleteRecipe(int recipeID)
        {
            int rowsAffected = 0;
            Recipe recipe = _recipes.Find(x => x.RecipeID == recipeID);
            if (recipe != null)
            {
                _recipes.Remove(recipe);
                rowsAffected = 1;
            }
            return rowsAffected;
        }

        public bool DeleteRecipeItemLines(int recipeID)
        {
            bool isDeleted = false; ;
            Recipe recipe = _recipes.Find(x => x.RecipeID == recipeID);
            if (recipe != null)
            {
                recipe.RecipeLines = null;
                isDeleted = true;
            }
            return isDeleted;
        }

        public int InsertRecipe(Recipe recipe, Item item, Offering offering)
        {
            int id = 0;
            _recipes.Add(recipe);
            if (_recipes.Contains(recipe))
            {
                id = recipe.RecipeID;
            }
            return id;
        }

        public int InsertRecipeItemLine(RecipeItemLineVM line, int recipeID)
        {
            int rowsAffected = 0;
            Recipe recipe = _recipes.Find(x => x.RecipeID == recipeID);
            recipe.RecipeLines.Add(line);
            if (recipe.RecipeLines.Contains(line))
            {
                rowsAffected = 1;
            }
            return rowsAffected;
        }

        public int ReactivateRecipe(int recipeID)
        {
            int rowsAffected = 0;
            Recipe recipe = _recipes.Find(x => x.RecipeID == recipeID);
            if (recipe.Active == false)
            {
                recipe.Active = true;
                rowsAffected = 1;
            }
            return rowsAffected;
        }

        public List<Recipe> SelectAllRecipes()
        {
            return _recipes;
        }

        public Recipe SelectRecipeByID(int recipeID)
        {
            return _recipes.Find(x => x.RecipeID == recipeID);
        }

        public List<RecipeItemLineVM> SelectRecipeLinesByID(int recipeID)
        {
            List<RecipeItemLineVM> recipeLines = null;
            Recipe recipe = _recipes.Find(x => x.RecipeID == recipeID);
            if (recipe != null)
            {
                recipeLines = recipe.RecipeLines;
            }
            return recipeLines;
        }

        public bool UpdateRecipe(Recipe oldRecipe, Recipe newRecipe)
        {
            bool result = false;
            if (oldRecipe.RecipeID == newRecipe.RecipeID)
            {
               int index = _recipes.FindIndex(x => x.RecipeID == oldRecipe.RecipeID &&
                x.RecipeLines == oldRecipe.RecipeLines &&
                x.Name == oldRecipe.Name &&
                x.Active == oldRecipe.Active &&
                x.DateAdded == oldRecipe.DateAdded &&
                x.Description == oldRecipe.Description);
                _recipes.ElementAt(index).Active = newRecipe.Active;
                _recipes.ElementAt(index).Description = newRecipe.Description;
                _recipes.ElementAt(index).Name = newRecipe.Name;
                _recipes.ElementAt(index).DateAdded = newRecipe.DateAdded;
                _recipes.ElementAt(index).RecipeLines = newRecipe.RecipeLines;
                result = true;
                    
            }
            return result;
        }
    }
}
