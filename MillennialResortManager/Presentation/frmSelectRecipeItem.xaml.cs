/// <summary>
/// Jared Greenfield
/// Created: 2019/01/24
/// 
/// Used to select an item to add to a recipe.
/// </summary>
///
using DataObjects;
using LogicLayer;
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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for frmSelectItem.xaml
    /// </summary>
    public partial class frmSelectItem : Window
    {
        private ItemManager _itemManager;
        private ItemTypeManagerMSSQL _itemTypeManager = new ItemTypeManagerMSSQL();
        private RecipeManager _recipeManager;
        private List<Item> _items = new List<Item>();
        private Item _chosenItem = null;
        private Item _recipeItem = null;
        public frmSelectItem(RecipeManager recipeManager, ItemManager itemManager)
        {
            InitializeComponent();
            _recipeManager = recipeManager;
            _itemManager = itemManager;
            SetupSelectItem();
            
        }
        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/14
        /// 
        /// Constructor used when editing a recipe so that the current item cannot be added to the 
        /// </summary>
        public frmSelectItem(RecipeManager recipeManager, ItemManager itemManager, Item recipeItem)
        {
            InitializeComponent();
            _recipeManager = recipeManager;
            _itemManager = itemManager;
            _recipeItem = recipeItem;
            SetupSelectItem();
            
            
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// Used to hide non-essential fields and set up buttons for selecting an item to add to a recipe.
        /// </summary>
        public void SetupSelectItem()
        {
            try
            {
                RefreshItemGrid();
                List<ItemType> itemTypes = _itemTypeManager.RetrieveAllItemTypes();
                // Clears the combobox and then repopulates.
                cboItemType.Items.Clear();
                foreach (var item in itemTypes)
                {
                    cboItemType.Items.Add(item.ItemTypeID);
                }
                cboItemType.Items.Add("Item Type");
                cboItemType.SelectedItem = "Item Type";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured populating the item grid. " + ex.Message);
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// Requeries the database for a fresh set of updated items.
        /// </summary>
        public void RefreshItemGrid()
        {
            try
            {
                _items = _itemManager.RetrieveAllItems();
                // Removes the current recipe from the list of items to add to the recipe.
                if (_recipeItem != null)
                {
                    int itemIndex = _items.FindIndex(i => i.ItemID == _recipeItem.ItemID);
                    _items.RemoveAt(itemIndex);
                }
                for (int i = 0; i < _itemManager.items.Count; i++)
                {
                    for (int j = 0; j < _items.Count; j++)
                    {
                        if (_items.ElementAt(j).ItemID == _itemManager.items.ElementAt(i).ItemID)
                        {
                            _items.RemoveAt(j);
                        }
                    }
                }
                dgItemList.ItemsSource = null;
                dgItemList.ItemsSource = _items.FindAll(a => a.Active == true);
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
        /// Closes the window when they press the cancel button.
        /// </summary>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// Alters the appearance of the grid to only include useful information for someone looking to add specific ingredients.
        /// </summary>
        private void DgItemList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "ItemID":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "ItemTypeID":
                    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    e.Column.Header = "Item Type";
                    break;
                case "Name":
                    e.Column.DisplayIndex = 0;
                    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                case "Description":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "DateActive":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Active":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "OnHandQty":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "ReorderQty":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "OfferingID":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "CustomerPurchasable":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "RecipeID":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// After each KeyUp, the datagrid will be filtered by what contents are in the filter boxes.
        /// </summary>
        private void TxtItemNameSearch_KeyUp(object sender, KeyEventArgs e)
        {
            FilterItemGrid();
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// Filters the DataGrid according to the contents of the filter boxes.
        /// </summary>
        private void FilterItemGrid()
        {
            var currentItems = (IEnumerable<Item>)_items;
            if (txtItemNameSearch.Text != null || txtItemNameSearch.Text != "")
            {
                currentItems = currentItems.Where(a => a.Name.ToUpper().Contains(txtItemNameSearch.Text.ToUpper()));
            }
            if (cboItemType.SelectedItem.ToString() != "Item Type")
            {
                currentItems = currentItems.Where(a => a.ItemTypeID.Equals(cboItemType.SelectedItem.ToString()));
            }
            dgItemList.ItemsSource = currentItems;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// After each selection is changed, the datagrid will be filtered by what contents are in the filter boxes.
        /// </summary>
        private void CboItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterItemGrid();
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// When a new DataGrid item is chosen, makes the submit button appear 
        /// </summary>
        private void DgItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _chosenItem = (Item)dgItemList.SelectedItem;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// Filters the DataGrid according to the contents of the filter boxes.
        /// </summary>
        private void TxtQuantity_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidatePage();
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// If the record is in a valid state of being able to submit, makes the 
        /// submit button appear, otherwise, it disappears.
        /// </summary>
        private bool ValidatePage()
        {
            bool isPageValid = false;
            if (txtQuantity.Text.IsValidRecipeQuantity() && _chosenItem != null && txtUnitOfMeasure.Text != "" && txtUnitOfMeasure.Text.IsValidRecipeUnitOfMeasure() && _chosenItem != null)
            {
                isPageValid = true;
            }
            if (txtQuantity.Text.IsValidRecipeQuantity() != true || txtQuantity.Text == "")
            {
                MessageBox.Show("Your quantity is not valid. It must be a whole number greater than or equal to 0.");
            }
            if (!txtUnitOfMeasure.Text.IsValidRecipeUnitOfMeasure()) 
            {
                MessageBox.Show("Your unit of measure must be filled in and contain only letters.");
            }
            if (_chosenItem == null)
            {
                MessageBox.Show("You must choose an item to add.");
            }
            return isPageValid;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// Adds the RecipeItemLineVM object to the List and returns back to the Recipe page.
        /// </summary>
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePage())
            {
                var line = new RecipeItemLineVM(_chosenItem.ItemID, _chosenItem.Name, Decimal.Parse(txtQuantity.Text), txtUnitOfMeasure.Text);
                _itemManager.items.Add(_chosenItem);
                _recipeManager.RecipeLines.Add(line);
                this.DialogResult = true;
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/03/07
        /// 
        /// Makes sure the quantity matches the XXXXXX.XXXX format.
        /// </summary>
        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtQuantity.Text.Contains("."))
            {
                txtQuantity.MaxLength = 11;
                try
                {
                    int indexOfDecimal = txtQuantity.Text.IndexOf('.');
                    int positionsAfterDecimal = txtQuantity.Text.Length - 1 - indexOfDecimal;
                    if (positionsAfterDecimal > 4)
                    {
                        txtQuantity.Text = txtQuantity.Text.Remove(txtQuantity.Text.Length - 1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                txtQuantity.MaxLength = 6;
            }

            if (!txtQuantity.Text.Contains(".") && txtQuantity.Text.Length > 6)
            {
                txtQuantity.Text = txtQuantity.Text.Substring(0, 6);
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/03/07
        /// 
        /// Adjusts the length for the box when inputting a period for decimals.
        /// </summary>
        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
            {
                txtQuantity.MaxLength = 11;
            }
        }
    }
}
