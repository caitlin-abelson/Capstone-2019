/// <summary>
/// Jared Greenfield
/// Created: 2019/01/24
/// 
/// A form for creating, viewing, and editing Recipe records.
/// </summary>
///
/// <remarks>
/// Jared Greenfield
/// Updated: 2019/02/07
/// Added new fields, logic for hiding and editing.
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for frmCreateRecipe.xaml
    /// </summary>
    public partial class frmCreateRecipe : Window
    {
        private RecipeManager _recipeManager = new RecipeManager();
        private ItemManager _itemManager = new ItemManager();
        private ItemTypeManagerMSSQL _itemTypeManager = new ItemTypeManagerMSSQL();
        private OfferingManager _offeringManager = new OfferingManager();
        private Offering _offering = null;
        private Recipe _oldRecipe;
        private Item _item;
        private User _user;
        private bool _isItemChanged = false;
        private bool _isOfferingChanged = false;

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// CREATE OPERATION
        /// A blank form for creating recipes.
        /// </summary>
        public frmCreateRecipe(User user)
        {
            InitializeComponent();
            _user = user;
            SetupCreatePage();
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// Edit / View OPERATION
        /// A form detailing a recipe record.
        /// </summary>
        /// <param name="recipe">Recipe object for filling out form.</param>
        public frmCreateRecipe(Recipe recipe, User user)
        {
            InitializeComponent();
            _user = user;
            _oldRecipe = recipe;
            try
            {
                _item = _itemManager.RetrieveItemByRecipeID(recipe.RecipeID);
                if (_item.OfferingID != null)
                {
                    _offering = _offeringManager.RetrieveOfferingByID((int)_item.OfferingID);
                }
                List<RecipeItemLineVM> recipeLines = _recipeManager.RetrieveRecipeLinesByID(_oldRecipe.RecipeID);
                dgIngredientList.ItemsSource = recipeLines;
                SetupViewPage();
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error showing this recipe. Please try agin later.");
                this.Close();
            }

        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// Used to setup a read-only version of a recipe reccord.
        /// 
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/02/07
        /// Added support for readonly to new fields. 
        /// </remarks>
        /// </summary>
        private void SetupViewPage()
        {
            try
            {
                _itemManager.items = _itemManager.RetrieveLineItemsByRecipeID(_oldRecipe.RecipeID);
                _recipeManager.RecipeLines = _recipeManager.RetrieveRecipeLinesByID(_oldRecipe.RecipeID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error gathering the recipe lines. " + ex.Message);
            }



            // Disabling Fields and Buttons
            btnDeleteIngredient.IsEnabled = false;
            txtRecipeName.IsReadOnly = true;
            txtPrice.IsReadOnly = true;
            txtDateAdded.IsReadOnly = true;
            txtRecipeDescription.IsReadOnly = true;
            chkActive.IsEnabled = false;
            chkPurchasable.IsEnabled = false;
            cboType.IsEnabled = false;


            // Field Population
            txtRecipeName.Text = _oldRecipe.Name;
            txtRecipeDescription.Text = _oldRecipe.Description;
            if (_offering != null)
            {
                txtPrice.Text = _offering.Price.ToString("C");
            }
            txtDateAdded.Text = _oldRecipe.DateAdded.ToShortDateString();
            chkActive.IsChecked = _oldRecipe.Active;
            chkPurchasable.IsChecked = _item.CustomerPurchasable;
            // Add Options to Type Combobox
            try
            {
                PopulateRecipeType();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error collecting Type options. " + ex.Message);
            }
            cboType.SelectedItem = _item.ItemTypeID;


            // Hiding Fields
            btnDeleteIngredient.Visibility = Visibility.Collapsed;
            btnAddIngredient.Visibility = Visibility.Collapsed;
            if (_offering == null)
            {
                txtPrice.Visibility = Visibility.Collapsed;
                lblPrice.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtPrice.Visibility = Visibility.Visible;
                lblPrice.Visibility = Visibility.Visible;

            }
            if ((bool)chkPurchasable.IsChecked)
            {
                lblPrice.Visibility = Visibility.Visible;
                txtPrice.Visibility = Visibility.Visible;
            }
            btnDeactivate.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
                

            // Renaming
            btnSubmit.Content = "Edit Recipe";
            lblTitle.Content = "Viewing " + _oldRecipe.Name + " Recipe";
            this.Title = "Viewing " + _oldRecipe.Name + " Recipe";
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/31
        /// 
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/02/07
        /// Added controls for new fields.
        /// </remarks>
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/03/05
        /// Added capability for Offering and Item to be updated from the form.
        /// </remarks>
        /// 
        /// Used to setup a read-only version of a recipe reccord.
        /// </summary>
        private void SetupEditPage()
        {
            
            // Field Population
            txtRecipeName.Text = _oldRecipe.Name;
            txtRecipeDescription.Text = _oldRecipe.Description;
            if (_offering != null)
            {
                txtPrice.Text = _offering.Price.ToString();
            }
            txtDateAdded.Text = _oldRecipe.DateAdded.ToShortDateString();
            chkActive.IsChecked = _oldRecipe.Active;
            cboType.SelectedItem = _item.ItemTypeID;

            //Fields and Buttons

            txtRecipeName.IsReadOnly = false;
            txtPrice.IsReadOnly = true;
            txtDateAdded.IsReadOnly = true;
            txtRecipeDescription.IsReadOnly = false;
            btnAddIngredient.IsEnabled = true;
            btnAddIngredient.Visibility = Visibility.Visible;
            chkActive.IsEnabled = false;
            cboType.IsEnabled = false;
            btnDeleteIngredient.IsEnabled = true;
            btnAddIngredient.IsEnabled = true;
            chkPurchasable.IsEnabled = false;
            btnDeactivate.Visibility = Visibility.Visible;
            if (_user.Roles.Contains("Manager"))
            {
                if (_oldRecipe.Active == false)
                {
                    btnDelete.Visibility = Visibility.Visible;
                }
            }
            txtPrice.IsReadOnly = false;
            chkPurchasable.IsEnabled = true;
            cboType.IsEnabled = true;
            

            // Renaming
            btnSubmit.Content = "Save Changes";
            lblPurchasable.Content = "Publicly Offered:";
            lblTitle.Content = "Editing " + _oldRecipe.Name + " Recipe";
            this.Title = "Editing " + _oldRecipe.Name + " Recipe";
            if (_oldRecipe.Active == false)
            {
                btnDeactivate.Content = "Reactivate";
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/02/07
        /// Added new field hidings and Type ComboBox population.
        /// </remarks>
        /// 
        /// Used to hide non-essential fields and set up buttons for creating a recipe.
        /// </summary>
        private void SetupCreatePage()
        {
            // Hide fields
            btnDeleteIngredient.Visibility = Visibility.Collapsed;
            btnSubmit.Visibility = Visibility.Collapsed;
            lblDateAdded.Visibility = Visibility.Collapsed;
            txtDateAdded.Visibility = Visibility.Collapsed;
            chkActive.Visibility = Visibility.Collapsed;
            lblActive.Visibility = Visibility.Collapsed;
            lblPrice.Visibility = Visibility.Collapsed;
            txtPrice.Visibility = Visibility.Collapsed;
            btnDeactivate.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;

            // Populate ComboBox
            try
            {
                PopulateRecipeType();
                cboType.Items.Add("Choose One");
                cboType.SelectedItem = "Choose One";
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error collecting Type options. " + ex.Message);
            }

        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Used to fill the RecipeType ComboBox.
        /// Used to determine if it is a food recipe or
        /// a blueprint for another item the Resort may make.
        /// </summary>
        private void PopulateRecipeType()
        {
            try
            {
                var itemTypes = _itemTypeManager.RetrieveAllItemTypes();
                cboType.Items.Clear();
                foreach (var itemType in itemTypes)
                {
                    cboType.Items.Add(itemType.ItemTypeID);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// Closes the window when the exit button is pressed.
        /// </summary>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/02/14
        /// Added the conditional option for adding items to the recipe when in edit mode.
        /// </remarks>
        /// Opens a window to add a specific item to the recipe.
        /// </summary>
        private void BtnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            frmSelectItem newPage;
            Item recipeItem = null;
            if (btnSubmit.Content.ToString() == "Save Changes")
            {
                try
                {
                    recipeItem = _itemManager.RetrieveItemByRecipeID(_oldRecipe.RecipeID);
                    if (recipeItem == null)
                    {
                        throw new Exception("The current recipe could not be found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The addition screen could not be brought up. " + ex.Message);
                    this.DialogResult = false;
                }
                newPage = new frmSelectItem(_recipeManager, _itemManager, recipeItem);
            }
            // Used if adding an ingredient to a new recipe.
            else
            {
                newPage = new frmSelectItem(_recipeManager, _itemManager);
            }

            bool? result = newPage.ShowDialog();
            if (result == true)
            {
                dgIngredientList.ItemsSource = null;
                dgIngredientList.ItemsSource = _recipeManager.RecipeLines;
            }
            else
            {
                MessageBox.Show("Adding an ingredient failed or cancelled.");
            }
            if (dgIngredientList.Items.Count >= 2)
            {
                btnSubmit.Visibility = Visibility.Visible;
            }
            else
            {
                btnSubmit.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/27
        /// 
        /// Opens a window to add a specific item to the recipe.
        /// </summary>
        private void DgIngredientList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "ItemName":
                    e.Column.Header = "Item Name";
                    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                case "UnitOfMeasure":
                    e.Column.Header = "Unit of Measure";
                    break;
                case "ItemID":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "RecipeID":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                

            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/30
        /// 
        /// Hides the delete ingredient button if there is no ingredient selected.
        /// </summary>
        private void DgIngredientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgIngredientList.SelectedItem != null && btnSubmit.Content.ToString() != "Edit Recipe")
            {
                btnDeleteIngredient.Visibility = Visibility.Visible;
            }
            else
            {
                btnDeleteIngredient.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/30
        /// 
        /// Removes the selected ingredient from the list of ingredients for the recipe. 
        /// Because a recipe is made of more than one item, this then hides the button to finish the recipe
        /// if there are less than 2 items in the list.
        /// </summary>
        private void BtnDeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (dgIngredientList.SelectedItem != null)
            {
                _recipeManager.RecipeLines.RemoveAt(dgIngredientList.SelectedIndex);
                _itemManager.items.RemoveAt(dgIngredientList.SelectedIndex);
                dgIngredientList.ItemsSource = null;
                dgIngredientList.ItemsSource = _recipeManager.RecipeLines;
            }
            if (dgIngredientList.Items.Count >= 2)
            {
                btnSubmit.Visibility = Visibility.Visible;
            }
            else
            {
                btnSubmit.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/30
        /// 
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/02/06
        /// Modified to allow creation of internal items and external Offerings.
        /// Ex. A recipe for a roux might be saved, but you wouldn't serve just a roux as
        /// a menu item.
        /// </remarks>
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/03/05
        /// Added Update capability for offerings and items to the edit feature.
        /// </remarks>
        /// Submits the recipe after validation to be stored in the database.
        /// </summary>
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (btnSubmit.Content.ToString() == "Submit New Recipe")
            {
                if (validateForm())
                {

                    try
                    {
                        int recipeID = 0;
                        Offering offering = null;
                        Item newItem = null;
                        // If the item will be a publicly available dish, it will be created as an Offering
                        if (chkPurchasable.IsChecked == true)
                        {
                            // Remove the 100000 when moving to production
                            offering = new Offering("Item", _user.UserID, txtRecipeDescription.Text, Decimal.Parse(txtPrice.Text));
                        }
                        newItem = new Item(null, (bool)chkPurchasable.IsChecked, recipeID, cboType.SelectedItem.ToString(), txtRecipeDescription.Text, 0, txtRecipeName.Text, 0);
                        newItem.DateActive = DateTime.Now;
                        Recipe recipe = new Recipe(txtRecipeName.Text, txtRecipeDescription.Text, DateTime.Now);
                        List<RecipeItemLineVM> lines = new List<RecipeItemLineVM>();
                        foreach (var item in dgIngredientList.Items)
                        {
                            lines.Add((RecipeItemLineVM)item);
                        }
                        recipe.RecipeLines = lines;
                        
                        // Add the recipe and the lines
                        recipeID = _recipeManager.CreateRecipe(recipe, newItem, offering);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was an error adding the recipe. " + ex.Message);
                    }
                    
                }

            }
            else if (btnSubmit.Content.ToString() == "Edit Recipe")
            {
                SetupEditPage();
            }
            else if (btnSubmit.Content.ToString() == "Save Changes")
            {
                if (validateForm())
                {
                    try
                    {
                        Recipe newRecipe = new Recipe(_oldRecipe.RecipeID,
                        txtRecipeName.Text,
                        txtRecipeDescription.Text,
                        _oldRecipe.DateAdded,
                        (bool)chkActive.IsChecked);
                        List<RecipeItemLineVM> lines = new List<RecipeItemLineVM>();
                        foreach (var item in _recipeManager.RecipeLines)
                        {
                            lines.Add((RecipeItemLineVM)item);
                        }
                        newRecipe.RecipeLines = lines;
                        bool isRecipeUpdateSuccessful = _recipeManager.UpdateRecipe(_oldRecipe, newRecipe);
                        int offeringID;
                        // If the recipe is purchasable by the public
                        if (chkPurchasable.IsChecked == true)
                        {
                            //If the recipe was previously an offering
                            if (_offering != null)
                            {
                                offeringID = _offering.OfferingID;
                                //If there was a change to offering
                                if (_isOfferingChanged)
                                {
                                    Offering newOffering = new Offering(_offering.OfferingID, "Item", _offering.EmployeeID,
                                        txtRecipeDescription.Text, Decimal.Parse(txtPrice.Text), (bool)chkActive.IsChecked);
                                    _offeringManager.UpdateOffering(_offering, newOffering);
                                }
                                else
                                {
                                    // Do nothing as nothing in offering was changed
                                }
                            }
                            else // Happens when a recipe is newly promoted to an Offering
                            {
                                Offering offering = new Offering("Item", _user.UserID, txtRecipeDescription.Text, Decimal.Parse(txtPrice.Text));
                                offeringID = _offeringManager.CreateOffering(offering);
                            }
                            if (_isItemChanged)
                            {
                                Item newItem = new Item(_item.ItemID,
                                                     (int)offeringID,
                                                     (bool)chkPurchasable.IsChecked,
                                                     _oldRecipe.RecipeID,
                                                     cboType.SelectedItem.ToString(),
                                                     txtRecipeDescription.Text,
                                                     _item.OnHandQty,
                                                     txtRecipeName.Text,
                                                     _item.ReorderQty,
                                                     _item.DateActive,
                                                     (bool)chkActive.IsChecked);
                                _itemManager.UpdateItem(_item, newItem);
                            }
                        }
                        else
                        {
                            // Was previously an offering
                            if (_offering != null)
                            {
                                Offering newOffering = new Offering(_offering.OfferingID, "Item", _offering.EmployeeID,
                                        txtRecipeDescription.Text, Decimal.Parse(txtPrice.Text), (bool)chkPurchasable.IsChecked);
                                _offeringManager.UpdateOffering(_offering, newOffering);
                                if (_isItemChanged)
                                {
                                    Item newItem = new Item(_item.ItemID,
                                                         _offering.OfferingID,
                                                         (bool)chkPurchasable.IsChecked,
                                                         _oldRecipe.RecipeID,
                                                         cboType.SelectedItem.ToString(),
                                                         txtRecipeDescription.Text,
                                                         _item.OnHandQty,
                                                         txtRecipeName.Text,
                                                         _item.ReorderQty,
                                                         _item.DateActive,
                                                         (bool)chkActive.IsChecked);
                                    _itemManager.UpdateItem(_item, newItem);
                                }
                            }
                            else
                            {
                                if (_isItemChanged)
                                {
                                    Item newItem = new Item(_item.ItemID,
                                                         null,
                                                         (bool)chkPurchasable.IsChecked,
                                                         _oldRecipe.RecipeID,
                                                         cboType.SelectedItem.ToString(),
                                                         txtRecipeDescription.Text,
                                                         _item.OnHandQty,
                                                         txtRecipeName.Text,
                                                         _item.ReorderQty,
                                                         _item.DateActive,
                                                         (bool)chkActive.IsChecked);
                                    _itemManager.UpdateItem(_item, newItem);
                                }
                            } 
                        }
                        if (isRecipeUpdateSuccessful)
                        {
                            MessageBox.Show("Update successful.");
                            this.DialogResult = true;
                        }
                        else
                        {
                            throw new Exception("Recipe update unsuccessful.");
                        }
                        _isItemChanged = false;
                        _isOfferingChanged = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was an error updating the record: " + ex.Message);
                    }
                }
                
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/27
        /// 
        /// Returns true if all fields are filled in and are valid values.
        /// Returns false if invalid or not filled in. 
        /// </summary>
        private bool validateForm()
        {
            bool isValid = false;
            string errorMessage = "There were the following errors in the form: ";
            if (txtRecipeName.Text != "" && txtRecipeName.Text != null)
            {
                if (txtRecipeDescription.Text != "" && txtRecipeDescription.Text != null)
                {
                    if (dgIngredientList.Items.Count >= 2)
                    {
                        if (cboType.SelectedItem.ToString() != "Choose One")
                        {
                            if (chkPurchasable.IsChecked == true)
                            {
                                if (txtPrice.Text.IsValidPrice())
                                {
                                    isValid = true;
                                }
                            }
                            else
                            {
                                isValid = true;
                            }
                        }
                    }
                }
            }
            if (isValid != true)
            {
                if (txtRecipeName.Text == "" || txtRecipeName.Text == null)
                {
                    errorMessage += "\n\nPlease enter a recipe name.";
                }
                if (txtRecipeDescription.Text == "" || txtRecipeDescription.Text == null)
                {
                    errorMessage += "\n\nPlease enter a description for the recipe.";
                }
                if (dgIngredientList.Items.Count < 2)
                {
                    errorMessage += "\n\nYou must have 2 or more items to make a recipe.";
                }
                if (cboType.SelectedItem.ToString() == "Choose One")
                {
                    errorMessage += "\n\nPlease select a Type other than 'Choose One'.";
                }
                if (!txtPrice.Text.IsValidPrice() && chkPurchasable.IsChecked == true)
                {
                    errorMessage += "\n\nThe price is invalid. It must be a positive decimal.";
                }
                MessageBox.Show(errorMessage);
                
            }
            
            return isValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Hides the price field if not checked, displays price if it is checked.
        /// Price only needs to be displayed if the Recipe will be offered to customers.
        /// </summary>
        private void chkPurchasable_Click(object sender, RoutedEventArgs e)
        {
            if (chkPurchasable.IsChecked == true)
            {
                lblPrice.Visibility = Visibility.Visible;
                txtPrice.Visibility = Visibility.Visible;
            }
            else
            {
                lblPrice.Visibility = Visibility.Collapsed;
                txtPrice.Visibility = Visibility.Collapsed;
            }
            _isOfferingChanged = true;
            _isItemChanged = true;
        }

        private void BtnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            if (btnDeactivate.Content.ToString() == "Deactivate")
            {
                try
                {
                    var confirmPage = new frmConfirmAction(CrudFunction.Deactivate);
                    bool? confirm = confirmPage.ShowDialog();
                    if (confirm == true)
                    {
                        try
                        {
                            _recipeManager.DeactivateRecipe(_oldRecipe.RecipeID);
                            MessageBox.Show("Deactivation success");
                            _oldRecipe.Active = false;
                            SetupEditPage();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Deactivation cancelled or failed.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Deactivation cancelled or failed.");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (btnDeactivate.Content.ToString() == "Reactivate")
            {
                try
                {
                    var confirmPage = new frmConfirmAction(CrudFunction.Reactivate);
                    bool? confirm = confirmPage.ShowDialog();
                    if (confirm == true)
                    {
                        try
                        {
                            _recipeManager.ReactivateRecipe(_oldRecipe.RecipeID);
                            MessageBox.Show("Reactivation success");
                            _oldRecipe.Active = true;
                            SetupEditPage();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Reactivation cancelled or failed.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Reactivation cancelled or failed.");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isItemChanged = true;
        }

        private void txtRecipeName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _isItemChanged = true;
        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isOfferingChanged = true;
        }

        private void txtRecipeDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isItemChanged = true;
            _isOfferingChanged = true;
        }
    }
}
