/// <summary>
/// Jared Greenfield
/// Created: 2019/01/24
/// 
/// <remarks>
/// Jared Greenfield
/// Created: 2019/02/20
/// Added RecipeLines to object and new constructor. 
/// </remarks>
/// 
/// Represents a Recipe object. This is anything made from multiple items that the 
/// hotel can then use or sell.
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Active { get; set; }

        public List<RecipeItemLineVM> RecipeLines{get; set;}

        // Full Constructor
        public Recipe(int recipeID, string name, string description, DateTime dateAdded, bool active, List<RecipeItemLineVM> recipeLines)
        {
            this.RecipeID = recipeID;
            this.Name = name;
            this.Description = description;
            this.DateAdded = dateAdded;
            this.Active = active;
            this.RecipeLines = recipeLines;
        }
        // Constructor without RecipeLines
        public Recipe(int recipeID, string name, string description, DateTime dateAdded, bool active)
        {
            this.RecipeID = recipeID;
            this.Name = name;
            this.Description = description;
            this.DateAdded = dateAdded;
            this.Active = active;
        }
        
        // Create Constructor
        public Recipe(string name, string description, DateTime dateAdded)
        {
            
            this.Name = name;
            this.Description = description;
            this.DateAdded = dateAdded;
        }

        public bool ValidateName()
        {
            bool isValid = true;
            if (Name == "" || Name == null || Name.Length > 50)
            {
                isValid = false;
            }

            return isValid;
        }

        public bool ValidateDescription()
        {
            bool isValid = true;
            if (Description.Length > 1000)
            {
                isValid = false;
            }

            return isValid;
        }

        public bool ValidateDateAdded()
        {
            bool isValid = true;
            // Recipe cannot be added before Resort was created.
            if (DateAdded.Year < 1900)
            {
                isValid = false;
            }

            return isValid;
        }

        public bool ValidateRecipeLines()
        {
            bool isValid = true;
            foreach (var line in RecipeLines)
            {
                try
                {
                    isValid = line.IsValid();
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("The recipe lines were not valid.");
                }
            }

            return isValid;
        }

        public bool IsValid()
        {
            bool isValid = false;
            if (ValidateDateAdded() && ValidateDescription() && ValidateName() && ValidateRecipeLines())
            {
                isValid = true;
            }

            return isValid;
        }
    }

   
}
