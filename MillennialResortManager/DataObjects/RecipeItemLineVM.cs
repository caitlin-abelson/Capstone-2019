/// <summary>
/// Jared Greenfield
/// Created: 2019/01/24
/// 
/// Used to visually display recipe lines on Datagrids.
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class RecipeItemLineVM
    {
        public RecipeItemLineVM(int itemID, int recipeID, string itemName, decimal quantity, string unitOfMeasure)
        {
            ItemID = itemID;
            RecipeID = recipeID;
            ItemName = itemName;
            Quantity = quantity;
            UnitOfMeasure = unitOfMeasure;
        }

        public RecipeItemLineVM(int itemID, string itemName, decimal quantity, string unitOfMeasure)
        {
            ItemID = itemID;
            ItemName = itemName;
            Quantity = quantity;
            UnitOfMeasure = unitOfMeasure;
        }

        public int ItemID { get; set; }
        public int RecipeID { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; }

        public bool IsValid()
        {
            bool isValid = false;
            if (ItemName.IsValidItemName()&& Quantity.IsValidRecipeQuantity() && UnitOfMeasure.IsValidRecipeUnitOfMeasure())
            {
                isValid = true;
            }
            return isValid;
        }

    }
}
