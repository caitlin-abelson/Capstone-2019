using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="Jared Greenfield" created="2019/01/22">
	/// Handles logic operations for Item objects.
	/// </summary>
	public class RecipeManager : IRecipeManager
	{
		private IRecipeAccessor _recipeAccessor;

		/// <summary author="Jared Greenfield" created="2019/02/21">
		/// RecipeManager is an implementation of the IRecipeManager interface to interact with the mock accessor
		/// </summary>
		public RecipeManager(RecipeAccessorMock mock)
		{
			_recipeAccessor = mock;
		}

		/// <summary author="Jared Greenfield" created="2019/02/21">
		/// RecipeManager is an implementation of the IRecipeManager interface to interact with the database.
		/// </summary>
		public RecipeManager()
		{
			_recipeAccessor = new RecipeAccessor();
		}

		private List<RecipeItemLineVM> _recipeLines = new List<RecipeItemLineVM>();
		public List<RecipeItemLineVM> RecipeLines
		{
			get
			{
				return _recipeLines;
			}
			set
			{
				_recipeLines = value;
			}
		}

		/// <summary author="Jared Greenfield" created="2019/01/25">
		/// Adds a Recipe Line to the database.
		/// </summary>
		/// <updates>
		/// <update author="Jared Greenfield" date="2019/02/21">
		/// Added the validation and exception throwing
		/// </update>
		/// </updates>
		/// <param name="recipeItemLine">The line to be added to the database.</param>
		/// <param name="recipeID">The ID of the recipe it belongs to.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>Rows affected.</returns>
		public int CreateRecipeItemLine(RecipeItemLineVM recipeItemLine, int recipeID)
		{
			int returnedValue = 0;
			if (recipeItemLine.IsValid())
			{
				try
				{
					returnedValue = _recipeAccessor.InsertRecipeItemLine(recipeItemLine, recipeID);
				}
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}
			else
			{
				throw new ArgumentException("This Recipe Line is invalid.");
			}

			return returnedValue;
		}

		/// <summary author="Jared Greenfield" created="2019/01/25">
		/// Adds a Recipe to the database and its lines.
		/// </summary>
		/// <updates>
		/// <update author="Jared Greenfield" date="2019/02/21">
		/// Added the validation and exception throwing
		/// </update>
		/// <update author="Jared Greenfield" date="2019/03/05">
		/// Added the optional offering validation if the offering wasn't null. Offerings are only needed 
		/// in specific circumstances and the validation doesn't work if the Offering is null.
		/// </update>
		/// </updates>
		/// <param name="recipe">The new recipe.</param>
		/// <param name="item">The new Item object</param>
		/// <param name="offering">The new Offering object</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>ID of Recipe.</returns>
		public int CreateRecipe(Recipe recipe, Item item, Offering offering)
		{
			int id = 0;
			if (item.CustomerPurchasable && offering != null)
			{
				if (recipe.IsValid() && item.IsValid() && offering.IsValid())
				{
					try
					{
						id = _recipeAccessor.InsertRecipe(recipe, item, offering);
					}
					catch (Exception ex)
					{
						ExceptionLogManager.getInstance().LogException(ex);
						throw ex;
					}
				}
				else
				{
					throw new ArgumentException("Given inputs were invalid.");
				}
			}
			else
			{
				if (recipe.IsValid() && item.IsValid())
				{
					try
					{
						id = _recipeAccessor.InsertRecipe(recipe, item, offering);
					}
					catch (Exception ex)
					{
						ExceptionLogManager.getInstance().LogException(ex);
						throw ex;
					}
				}
				else
				{
					throw new ArgumentException("Given inputs were invalid.");
				}
			}

			return id;
		}

		/// <summary author="Jared Greenfield" created="2019/01/29">
		/// Retrieves a Recipe based on ID. 
		/// </summary>
		/// <param name="recipeID">The ID of the recipe.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>Recipe Object</returns>
		public Recipe RetrieveRecipeByID(int recipeID)
		{
			Recipe recipe = null;

			try
			{
				recipe = _recipeAccessor.SelectRecipeByID(recipeID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return recipe;
		}

		/// <summary author="Jared Greenfield" created="2019/01/29">
		/// Retrieves lines of a Recipe.
		/// </summary>
		/// <param name="recipeID">The ID of the recipe.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of Recipe Lines.</returns>
		public List<RecipeItemLineVM> RetrieveRecipeLinesByID(int recipeID)
		{
			List<RecipeItemLineVM> lines = null;

			try
			{
				lines = _recipeAccessor.SelectRecipeLinesByID(recipeID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return lines;
		}

		/// <summary author="Jared Greenfield" created="2019/01/30">
		/// Deletes a recipe's lines.
		/// </summary>
		/// <param name="recipeID">The ID of the recipe.</param>
		/// <exception cref="SQLException">Delete Fails (example of exception tag)</exception>
		/// <returns>Rows affected.</returns>
		public bool DeleteRecipeLines(int recipeID)
		{
			bool successfulDeletion = false;
			try
			{
				successfulDeletion = _recipeAccessor.DeleteRecipeItemLines(recipeID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return successfulDeletion;
		}

		/// <summary author="Jared Greenfield" created="2019/01/31">
		/// <updates>
		/// <update author="Jared Greenfield" date="2019/02/20">
		/// Removed Recipe lines as it is now in the Recipe Object
		/// </update>
		/// </updates>
		/// Updates a recipe with an updated recipe and lines.
		/// </summary>
		/// <param name="oldRecipe">The old recipe.</param>
		/// <param name="newRecipe">The new recipe.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>True if updated successfully, false otherwise</returns>
		public bool UpdateRecipe(Recipe oldRecipe, Recipe newRecipe)
		{
			bool isUpdateSuccess = false;
			if (newRecipe.IsValid())
			{
				try
				{
					isUpdateSuccess = _recipeAccessor.UpdateRecipe(oldRecipe, newRecipe);
				}
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}
			else
			{
				throw new ArgumentException("New Recipe was invalid.");
			}
			return isUpdateSuccess;
		}

		/// <summary author="Jared Greenfield" created="2019/02/07">
		/// Retrieves a list of all Recipes.
		/// </summary>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>List of Recipe Lines.</returns>
		public List<Recipe> RetrieveAllRecipes()
		{
			List<Recipe> recipes = null;

			try
			{
				recipes = _recipeAccessor.SelectAllRecipes();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return recipes;
		}

		/// <summary author="Jared Greenfield" created="2019/02/14">
		/// Deletes the Recipe and Lines
		/// </summary>
		/// <exception cref="SQLException">Delete Fails(example of exception tag)</exception>
		/// <returns>True if successful, false if not</returns>
		public bool DeleteRecipe(int recipeID)
		{
			bool success = false;
			try
			{
				if (1 == _recipeAccessor.DeleteRecipe(recipeID))
				{
					success = true;
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return success;
		}

		/// <summary author="Jared Greenfield" created="2019/02/14">
		/// Deactivates the recipe
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public bool DeactivateRecipe(int recipeID)
		{
			bool success = false;
			try
			{
				if (1 == _recipeAccessor.DeactivateRecipe(recipeID))
				{
					success = true;
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return success;
		}

		/// <summary author="Jared Greenfield" created="2019/02/14">
		/// Reactivates the recipe
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public bool ReactivateRecipe(int recipeID)
		{
			bool success = false;
			try
			{
				if (1 == _recipeAccessor.ReactivateRecipe(recipeID))
				{
					success = true;
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return success;
		}
	}
}