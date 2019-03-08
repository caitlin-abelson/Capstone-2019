/// <summary>
/// Jared Greenfield
/// Created: 2019/01/22
/// 
/// The concrete implementation of IRecipeAccessor. Handles storage and collection of
/// Recipe objects to and from the database.
/// </summary>
///
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccessLayer
{
    public class RecipeAccessor : IRecipeAccessor
    {

        private IItemAccessor _itemAccessor = new ItemAccessor();
        private IOfferingAccessor _offeringAccessor = new OfferingAccessor();

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/24
        ///
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2018/02/14
        /// Added TransactionScope to make sure either all recipe, item, and offering creations
        /// happen, or they rollback to ensure consistency.
        /// </remarks>
        /// 
        /// <remarks>
        /// Jared Greenfield
        /// Created: 2019/02/20
        /// Added RecipeLines to object and removed lines parameter
        /// </remarks>
        /// 
        /// Adds a Recipe and its lines to the database.
        /// </summary>
        /// <param name="recipe">A recipe object.</param>
        /// <param name="item">The Item that the recipe corresponds to.</param>
        /// <param name="offering">The Offering that may or may not exist based on whether it is decided upon creation if it is a public recipe.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Rows affected</returns>
        public int InsertRecipe(Recipe recipe, Item item, Offering offering)
        {
            int returnedID = 0;
            string cmdText = @"sp_insert_recipe";
            string cmdText2 = @"sp_insert_recipe_item_line";
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = DBConnection.GetDbConnection())
                    {
                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@Name", recipe.Name);
                        cmd1.Parameters.AddWithValue("@Description", recipe.Description);
                        var temp = cmd1.ExecuteScalar();
                        returnedID = Convert.ToInt32(temp);

                        foreach (var line in recipe.RecipeLines)
                        {
                            int result = 0;
                            SqlCommand cmd2 = new SqlCommand(cmdText2, conn);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@RecipeID", returnedID);
                            cmd2.Parameters.AddWithValue("@ItemID", line.ItemID);
                            cmd2.Parameters.AddWithValue("@Quantity", line.Quantity);
                            cmd2.Parameters.AddWithValue("@UnitOfMeasure", line.UnitOfMeasure);
                            result = cmd2.ExecuteNonQuery();
                            if (result != 1)
                            {
                                throw new Exception("Line failed to add");
                            }
                        }
                        
                        
                    }
                    int offeringID = 0;
                    if (offering != null)
                    {
                        offeringID = _offeringAccessor.InsertOffering(offering);
                    }
                    if (offeringID != 0)
                    {
                        item.OfferingID = offeringID;
                    }
                    item.RecipeID = returnedID;
                    _itemAccessor.InsertItem(item);
                    scope.Complete();
                }
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnedID;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/24
        ///
        /// Creates a recipe line under the given ID.
        /// </summary>
        /// <param name="recipeID">The ID of the recipe that the line will be listed under.</param>
        /// <param name="line">The recipe line to be added.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>1 if successful, 0 if unsuccessful</returns>
        public int InsertRecipeItemLine(RecipeItemLineVM line, int recipeID)
        {
            int returnedValue = 0;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_insert_recipe_item_line";
            try
            {
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@RecipeID", recipeID);
                cmd1.Parameters.AddWithValue("@ItemID", line.ItemID);
                cmd1.Parameters.AddWithValue("@Quantity", line.Quantity);
                cmd1.Parameters.AddWithValue("@UnitOfMeasure", line.UnitOfMeasure);
                try
                {
                    conn.Open();
                    returnedValue = cmd1.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return returnedValue;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/31
        ///
        /// Deletes the recipe lines under the given ID.
        /// </summary>
        /// <param name="recipeID">The ID of the recipe that the lines will be deleted from.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Rows Affected</returns>
        public bool DeleteRecipeItemLines(int recipeID)
        {
            bool successfulDeletion = false;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_delete_recipe_item_lines";
            try
            {
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@RecipeID", recipeID);
                try
                {
                    conn.Open();
                    int linesAffected = cmd1.ExecuteNonQuery();
                    if (linesAffected > 0)
                    {
                        successfulDeletion = true;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return successfulDeletion;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/29
        ///
        /// <remarks>
        /// Jared Greenfield
        /// Created: 2019/02/20
        /// Added transaction scope so all RecipeLines are added to object correctly or fails as one.
        /// </remarks>
        /// 
        /// Retrieves a recipe with the given ID.
        /// </summary>
        /// <param name="recipeID">The ID of the recipe to be selected.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Recipe Object</returns>
        public Recipe SelectRecipeByID(int recipeID)
        {
            Recipe recipe = null;
            string cmdText = @"sp_select_recipe";
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection conn = DBConnection.GetDbConnection())
                {
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@RecipeID", recipeID);


                    var reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            DateTime dateAdded = reader.GetDateTime(3);
                            bool active = reader.GetBoolean(4);
                            recipe = new Recipe(recipeID, name, description, dateAdded, active);
                            recipe.RecipeLines = SelectRecipeLinesByID(recipeID);
                        }
                    }
                }
                scope.Complete();
            }
            return recipe;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/29
        ///
        /// Creates a recipe line under the given ID.
        /// </summary>
        /// <param name="recipeID">The ID of the recipe that the line will be listed under.</param>
        /// <param name="line">The recipe line to be added.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>1 if successful, 0 if unsuccessful</returns>
        public List<RecipeItemLineVM> SelectRecipeLinesByID(int recipeID)
        {
            List<RecipeItemLineVM> lines = new List<RecipeItemLineVM>();
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_recipe_item_lines";
            List<Item> items = new List<Item>();
            try
            {
                items = _itemAccessor.SelectAllItems();
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@RecipeID", recipeID);
                try
                {
                    conn.Open();
                    var reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int itemID = reader.GetInt32(1);
                            decimal quantity = reader.GetDecimal(2);
                            string unitOfMeasure = reader.GetString(3);
                            var item = items.Find(x => x.ItemID == itemID);
                            string itemName = item.Name;
                            RecipeItemLineVM line = new RecipeItemLineVM(itemID, recipeID, itemName, quantity, unitOfMeasure);
                            lines.Add(line);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return lines;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/30
        ///
        /// Updates a recipe with a new recipe as well as the lines involved.
        /// </summary>
        /// 
        /// <remarks>
        /// Jared Greenfield
        /// Created: 2019/02/20
        /// Removed recipe lines parameter after it was added to object
        /// Updated foreach to include the new recipe's items
        /// </remarks>
        /// <param name="oldRecipe">The old recipe to be updated.</param>
        /// <param name="newRecipe">The updated recipe.</param>
        /// <param name="recipeLines">The updated lines for the recipe.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>true if successful, false if not</returns>
        public bool UpdateRecipe(Recipe oldRecipe, Recipe newRecipe)
        {
            bool updateSuccessful = false;
            string cmdText = @"sp_update_recipe";
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = DBConnection.GetDbConnection())
                    {
                        conn.Open();
                        // Update Recipe Itself
                        SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@RecipeID", oldRecipe.RecipeID);

                        cmd1.Parameters.AddWithValue("@OldName", oldRecipe.Name);
                        cmd1.Parameters.AddWithValue("@OldDescription", oldRecipe.Description);
                        cmd1.Parameters.AddWithValue("@OldDateAdded", oldRecipe.DateAdded);
                        cmd1.Parameters.AddWithValue("@OldActive", oldRecipe.Active);

                        cmd1.Parameters.AddWithValue("@NewName", newRecipe.Name);
                        cmd1.Parameters.AddWithValue("@NewDescription", newRecipe.Description);
                        cmd1.Parameters.AddWithValue("@NewDateAdded", newRecipe.DateAdded);
                        cmd1.Parameters.AddWithValue("@NewActive", newRecipe.Active);
                        int linesAffected = cmd1.ExecuteNonQuery();
                        if (linesAffected <= 0)
                        {
                            throw new InvalidOperationException("The recipe was not updated");
                        }

                        // Delete Recipe lines to make room for updated ones
                        bool successfulDeletion = false;
                        string cmdText2 = @"sp_delete_recipe_item_lines";
                        SqlCommand cmd2 = new SqlCommand(cmdText2, conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@RecipeID", oldRecipe.RecipeID);
                           
                        int linesDeleted = cmd2.ExecuteNonQuery();
                        if (linesDeleted > 0)
                        {
                            successfulDeletion = true;
                        }

                        // If the lines were successfully removed from the database, then add the new ones.
                        // Otherwise, throw an exception to Rollback the transaction.
                        if (successfulDeletion)
                        {
                            foreach (RecipeItemLineVM line in newRecipe.RecipeLines)
                            {
                            string cmdText3 = @"sp_insert_recipe_item_line";
                                
                            SqlCommand cmd3 = new SqlCommand(cmdText3, conn);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.AddWithValue("@RecipeID", oldRecipe.RecipeID);
                            cmd3.Parameters.AddWithValue("@ItemID", line.ItemID);
                            cmd3.Parameters.AddWithValue("@Quantity", line.Quantity);
                            cmd3.Parameters.AddWithValue("@UnitOfMeasure", line.UnitOfMeasure);
                            cmd3.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException("The lines were not deleted.");
                        }
                       
                        updateSuccessful = true;
                    }
                    scope.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return updateSuccessful;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/29
        ///
        /// <remarks>
        /// Jared Greenfield
        /// Created: 2019/02/20
        /// Added transaction scope so all RecipeLines are added to object correctly or fails as one.
        /// </remarks> 
        /// Retrieves a recipe with the given ID.
        /// </summary>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Recipe Object</returns>
        public List<Recipe> SelectAllRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            string cmdText = @"sp_select_all_recipes";
            using (SqlConnection conn = DBConnection.GetDbConnection())
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;


                var reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int recipeID = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string description = reader.GetString(2);
                        DateTime dateAdded = reader.GetDateTime(3);
                        bool active = reader.GetBoolean(4);
                        Recipe recipe = new Recipe(recipeID, name, description, dateAdded, active);
                        recipe.RecipeLines = SelectRecipeLinesByID(recipeID);
                        recipes.Add(recipe);
                    }
                }
            }
            return recipes;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/02/14
        ///
        /// Deletes the Recipe and Lines
        /// </summary>
        /// <exception cref="SQLException">Delete Fails(example of exception tag)</exception>
        /// <returns>Rows affected</returns>
        public int DeleteRecipe(int recipeID)
        {
            int rows = 0;
            string cmdText = @"sp_delete_recipe";
            
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection conn = DBConnection.GetDbConnection())
                {
                    SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    var temp = cmd1.ExecuteNonQuery();
                    rows = Convert.ToInt32(temp);
                }
                DeleteRecipeItemLines(recipeID);
                scope.Complete();
            }
            
            return rows;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/02/14
        ///
        /// Deactivates the recipe
        /// </summary>
        /// <returns>Rows affected</returns>
        public int DeactivateRecipe(int recipeID)
        {
            int rows = 0;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_deactivate_recipe";
            try
            {
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@RecipeID", recipeID);
                try
                {
                    conn.Open();
                    rows = cmd1.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/02/14
        ///
        /// Reactivates the recipe
        /// </summary>
        /// <returns>Rows affected</returns>
        public int ReactivateRecipe(int recipeID)
        {
            int rows = 0;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_reactivate_recipe";
            try
            {
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@RecipeID", recipeID);
                try
                {
                    conn.Open();
                    rows = cmd1.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
    }
}
