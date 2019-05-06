
///
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="Jared Greenfield" created="2019/01/22">
	/// Handles logic operations for Recipe objects.
	/// </summary>
	public interface IRecipeManager
	{
		/// <summary author="Jared Greenfield" created="2019/01/25">
		/// Adds a Recipe to the database and its lines.
		/// </summary>
		/// <updates>
		/// <update author="Jared Greenfield" date="2019/02/20">
		/// Removed Recipe lines as it is now in the Recipe Object
		/// </update>
		/// </updates>
		/// <param name="recipe">The new recipe.</param>
		/// <param name="recipeID">The lines that belong to the recipe.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>ID of Recipe.</returns>
		int CreateRecipe(Recipe recipe, Item item, Offering offering);

		/// <summary author="Jared Greenfield" created="2019/01/25">
		/// Adds a Recipe Line to the database.
		/// </summary>
		/// <param name="recipeItemLine">The line to be added to the database.</param>
		/// <param name="recipeID">The ID of the recipe it belongs to.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>Rows affected.</returns>
		int CreateRecipeItemLine(RecipeItemLineVM recipeItemLine, int recipeID);

		/// <summary author="Jared Greenfield" created="2019/01/29">
		/// Retrieves a Recipe based on ID. 
		/// </summary>
		/// <param name="recipeID">The ID of the recipe.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>Recipe Object</returns>
		Recipe RetrieveRecipeByID(int recipeID);

		/// <summary author="Jared Greenfield" created="2019/01/29">
		/// Retrieves lines of a Recipe.
		/// </summary>
		/// <param name="recipeID">The ID of the recipe.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of Recipe Lines.</returns>
		List<RecipeItemLineVM> RetrieveRecipeLinesByID(int recipeID);

		/// <summary author="Jared Greenfield" created="2019/02/07">
		/// Retrieves a list of all Recipes.
		/// </summary>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of Recipe Lines.</returns>
		List<Recipe> RetrieveAllRecipes();

		/// <summary author="Jared Greenfield" created="2019/01/30">
		/// Deletes a recipe's lines.
		/// </summary>
		/// <param name="recipeID">The ID of the recipe.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>Rows affected.</returns>
		bool DeleteRecipeLines(int recipeID);

		/// <summary author="Jared Greenfield" created="2019/01/31">
		/// Updates a recipe with an updated recipe and lines.
		/// </summary>
		/// <update author="Jared Greenfield" date="2019/02/20">
		/// Removed Recipe lines as it is now in the Recipe Object
		/// </update>
		/// <param name="oldRecipe">The old recipe.</param>
		/// <param name="newRecipe">The new recipe.</param>
		/// <param name="recipeLines">The lines of the new recipe.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>True if updated successfully, false otherwise</returns>
		bool UpdateRecipe(Recipe oldRecipe, Recipe newRecipe);

		/// <summary author="Jared Greenfield" created="2019/02/14">
		/// Deletes the Recipe and Lines
		/// </summary>
		/// <exception cref="SQLException">Delete Fails(example of exception tag)</exception>
		/// <returns>True if successful, false if not</returns>
		bool DeleteRecipe(int recipeID);

		/// <summary author="Jared Greenfield" created="2019/02/14">
		/// Deactivates the recipe
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		bool DeactivateRecipe(int recipeID);

		/// <summary author="Jared Greenfield" created="2019/02/14">
		/// Reactivates the recipe
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		bool ReactivateRecipe(int recipeID);
    }
}