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
    /// Interaction logic for ItemBrowse.xaml
    /// </summary>
    /// <remarks>
    /// Jared Greenfield
    /// Updated: 2019/04/03
    /// Updated to remove products and add in Item
    /// </remarks>
    public partial class ItemBrowse : Window
    {

        List<Item> _allItems;
        List<Item> _currentItems;
        ItemManager _itemManager = new ItemManager();
        Item _selectedItem = new Item();
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Updated to remove products and add in Item
        /// </remarks>
        public ItemBrowse()
        {
            InitializeComponent();
            populateItems();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler for a mouse double click on an item within the data grid.
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Updated to remove products and add in Item
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var product = (Item)dgProducts.SelectedItem;
            //var createForm = new CreateItem(product);
           // var productAdded = createForm.ShowDialog();
            refreshItems();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Updated to remove products and add in Item
        /// </remarks>
        private void populateItems()
        {
            try
            {
                _allItems = _itemManager.RetrieveAllItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgProducts.ItemsSource = _allItems;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Updated to remove products and add in Item
        /// </remarks>
        private void refreshItems()
        {
            try
            {
                _allItems = _itemManager.RetrieveAllItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            _currentItems = _allItems;
            dgProducts.ItemsSource = _currentItems;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with the user selecting cancle on the window
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProductCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user clicking on a add item button. Calls the createItem window.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var createForm = new CreateItem();
            createForm.ShowDialog();
            refreshItems();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Updated to remove products and add in Item
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedIndex != -1)
            {
                _selectedItem = (Item)dgProducts.SelectedItem;

                //var createForm = new CreateItem(_selectedItem);
                //createForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must have a item selected.");
            }
            refreshItems();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Updated to remove products and add in Item
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Item selectedItem = (Item)dgProducts.SelectedItem;
            MessageBoxResult result;
            if(dgProducts.SelectedIndex != -1)
            {
                if (selectedItem.Active)
                {
                    result = MessageBox.Show("Are you sure you want to deactivate " + selectedItem.Name, "Deactivating Item", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _itemManager.DeactivateItem(_selectedItem);
                    }
                }
                if (!selectedItem.Active)
                {
                    result = MessageBox.Show("Are you sure you want to purge " + selectedItem.Name, "Purging Item", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _itemManager.DeleteItem(_selectedItem);
                    }
                }
            }
            else
            {
                MessageBox.Show("You must have a item selected.");
            }
            populateItems();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Updated to remove products and add in Item
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbActive_Click(object sender, RoutedEventArgs e)
        {
            if((bool)cbActive.IsChecked && (bool)cbDeactive.IsChecked)
            {
                populateItems();
            }
            else if ((bool)cbActive.IsChecked)
            {
                try
                {
                    _currentItems = _itemManager.RetrieveActiveItems();
                    dgProducts.ItemsSource = _currentItems;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (!(bool)cbActive.IsChecked)
            {
                populateItems();
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user checking a box labled deactive to view only deactive *should be inactive* products.
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Updated to remove products and add in Item
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDeactive_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)cbActive.IsChecked && (bool)cbDeactive.IsChecked)
            {
                populateItems();
            }
            else if ((bool)cbDeactive.IsChecked)
            {
                try
                {
                    _currentItems = _itemManager.RetrieveDeactiveItems();
                    dgProducts.ItemsSource = _currentItems;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (!(bool)cbDeactive.IsChecked)
            {
                populateItems();
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user clicking the search button. Assures the user has entered something to search for.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSearchBox.Text.ToString() != "")
                {
                    _currentItems = _allItems.FindAll(b => b.Name.ToLower().Contains(txtSearchBox.Text.ToString().ToLower()));
                    dgProducts.ItemsSource = _currentItems;
                }
                else
                {
                    MessageBox.Show("You must search for a item.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user clciking the clear button. Clears all filters and checkboxes
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            populateItems();
            this.txtSearchBox.Text = "";
            this.cbActive.IsChecked = false;
            this.cbDeactive.IsChecked = false;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with the columns that populate on the datagrid. Can be changed moving forward as many fields have been added to original.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProducts_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
            }
            string headerName = e.Column.Header.ToString();
            if (headerName == "ItemID")
            {
                e.Cancel = true;
            }
            if (headerName == "Active")
            {
                e.Cancel = true;
            }
            if (headerName == "CustomerPurchasable")
            {
                e.Cancel = true;
            }
            if (headerName == "RecipeID")
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with the serachbox being focused. Highlights the text for easier searching.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtSearchBox.SelectAll();
        }
    }
}
