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
    /// Interaction logic for ProductBrowse.xaml
    /// </summary>
    public partial class ProductBrowse : Window
    {

        List<Product> _allProducts;
        List<Product> _currentProducts;
        ProductManagerMSSQL _productManager = new ProductManagerMSSQL();
        Product _selectedProduct = new Product();
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Initial Constructor
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        public ProductBrowse()
        {
            InitializeComponent();
            populateProducts();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler for a mouse double click on an item within the data grid.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var product = (Product)dgProducts.SelectedItem;
            var createForm = new CreateProduct(product);
            var productAdded = createForm.ShowDialog();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Method to populate the datagrid.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void populateProducts()
        {
            try
            {
                _allProducts = _productManager.RetrieveAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgProducts.ItemsSource = _allProducts;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Method to refresh the datagrid information after a change
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void refreshProducts()
        {
            try
            {
                _allProducts = _productManager.RetrieveAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            _currentProducts = _allProducts;
            dgProducts.ItemsSource = _currentProducts;
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
        /// Handler to deal with a user clicking on a add product button. Calls the createProduct window.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var createForm = new CreateProduct();
            createForm.ShowDialog();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user clicking on a button labled read/update. Checks to assure an item is selected.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedIndex != -1)
            {
                _selectedProduct = (Product)dgProducts.SelectedItem;

                var createForm = new CreateProduct(_selectedProduct);
                createForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must have a product selected.");
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user clicking on a delete button. Assures that there is an item selected.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = (Product)dgProducts.SelectedItem;
            MessageBoxResult result;
            if(dgProducts.SelectedIndex != -1)
            {
                if (selectedProduct.Active)
                {
                    result = MessageBox.Show("Are you sure you want to deactivate " + selectedProduct.Name, "Deactivating Item", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _productManager.DeactivateProduct(_selectedProduct);
                    }
                }
                if (!selectedProduct.Active)
                {
                    result = MessageBox.Show("Are you sure you want to purge " + selectedProduct.Name, "Purging Item", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _productManager.DeleteProduct(_selectedProduct);
                    }
                }
            }
            else
            {
                MessageBox.Show("You must have a product selected.");
            }
            populateProducts();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user checking a box labled active to view only active products.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbActive_Click(object sender, RoutedEventArgs e)
        {
            if((bool)cbActive.IsChecked && (bool)cbDeactive.IsChecked)
            {
                populateProducts();
            }
            else if ((bool)cbActive.IsChecked)
            {
                try
                {
                    _currentProducts = _productManager.RetrieveActiveProducts();
                    dgProducts.ItemsSource = _currentProducts;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (!(bool)cbActive.IsChecked)
            {
                populateProducts();
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user checking a box labled deactive to view only deactive *should be inactive* products.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDeactive_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)cbActive.IsChecked && (bool)cbDeactive.IsChecked)
            {
                populateProducts();
            }
            else if ((bool)cbDeactive.IsChecked)
            {
                try
                {
                    _currentProducts = _productManager.RetrieveDeactiveProducts();
                    dgProducts.ItemsSource = _currentProducts;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (!(bool)cbDeactive.IsChecked)
            {
                populateProducts();
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
                    _currentProducts = _allProducts.FindAll(b => b.Name.ToLower().Contains(txtSearchBox.Text.ToString().ToLower()));
                    dgProducts.ItemsSource = _currentProducts;
                }
                else
                {
                    MessageBox.Show("You must search for a product");
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
            populateProducts();
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
            if (headerName == "ProductID")
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
