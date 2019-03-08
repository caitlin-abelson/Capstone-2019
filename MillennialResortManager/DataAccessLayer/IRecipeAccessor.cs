using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IRecipeAccessor
    {
        int DeactivateRecipe(int recipeID);
        int DeleteRecipe(int recipeID);
        bool DeleteRecipeItemLines(int recipeID);
        int InsertRecipe(Recipe recipe, Item item, Offering offering);
        int InsertRecipeItemLine(RecipeItemLineVM line, int recipeID);
        int ReactivateRecipe(int recipeID);
        List<Recipe> SelectAllRecipes();
        Recipe SelectRecipeByID(int recipeID);
        List<RecipeItemLineVM> SelectRecipeLinesByID(int recipeID);
        bool UpdateRecipe(Recipe oldRecipe, Recipe newRecipe);
    }
}