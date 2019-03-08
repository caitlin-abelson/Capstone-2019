/// <summary>
/// Jared Greenfield
/// Created: 2019/01/24
/// 
/// Represents an Item object. This is anything purchased by the hotel and
/// then sold or used to make recipes.
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Item
    {
        public Item()
        {

        }
        public Item(int itemID, int? offeringID, bool customerPurchasable, int? recipeID, string itemTypeID, string description, int onHandQty, string name, int reorderQty, DateTime dateActive, bool active)
        {
            ItemID = itemID;
            OfferingID = offeringID;
            CustomerPurchasable = customerPurchasable;
            RecipeID = recipeID;
            ItemTypeID = itemTypeID;
            Description = description;
            OnHandQty = onHandQty;
            Name = name;
            ReorderQty = reorderQty;
            DateActive = dateActive;
            Active = active;
        }

        public Item(int? offeringID, bool customerPurchasable, int? recipeID, string itemTypeID, string description, int onHandQty, string name, int reorderQty)
        {
            OfferingID = offeringID;
            CustomerPurchasable = customerPurchasable;
            RecipeID = recipeID;
            ItemTypeID = itemTypeID;
            Description = description;
            OnHandQty = onHandQty;
            Name = name;
            ReorderQty = reorderQty;
        }

        public int ItemID { get; set; }
        public int? OfferingID { get; set; }
        public bool CustomerPurchasable { get; set; }
        public int? RecipeID { get; set; }
        public string ItemTypeID { get; set; }
        public string Description { get; set; }
        public int OnHandQty { get; set; }
        public string Name { get; set; }
        public int ReorderQty { get; set; }
        public DateTime DateActive { get; set; }
        public bool Active { get; set; }

        public bool ValidateItemTypeID()
        {
            bool isValid = true;
            if (ItemTypeID == null || ItemTypeID.Length > 15 || ItemTypeID.Length == 0)
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

        public bool ValidateName()
        {
            bool isValid = true;
            if (Name == "" || Name == null || Name.Length > 50)
            {
                isValid = false;
            }

            return isValid;
        }

        public bool ValidateDateActive()
        {
            bool isValid = true;
            // Item cannot be active before Resort opened up.
            if (DateActive.Year < 1900)
            {
                isValid = false;
            }

            return isValid;
        }

        public bool IsValid()
        {
            bool isValid = false;
            if (ValidateDateActive() && ValidateDescription() && ValidateItemTypeID() && ValidateName())
            {
                isValid = true;
            }

            return isValid;
        }
    }
}

