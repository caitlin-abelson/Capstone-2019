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
    /// Author: James Heim
    /// Created 2019-02-28
    /// 
    /// Interaction logic for BrowseShop.xaml
    /// </summary>
    public partial class BrowseShop : Window
    {

        List<VMBrowseShop> _allShops;
        List<VMBrowseShop> _currentShops;
        ShopManagerMSSQL _shopManager;

        /// <summary>
        /// Author: James Heim
        /// Created 2019-02-28
        /// 
        /// Only constructor for the Browse Form.
        /// Setup the manager.
        /// </summary>
        public BrowseShop()
        {
            InitializeComponent();

            _shopManager = new ShopManagerMSSQL();

            // Load all active shops and populate the data grid.
            refreshShops();

            // Focus the filter textbox.
            txtSearchName.Focus();
        }
        
        /// <summary>
        /// Author: James Heim
        /// Created 2019-02-28
        /// 
        /// Retrieve all Shops from the View Model.
        /// </summary>
        private void refreshShops()
        {
            try
            {
                _allShops = (List<VMBrowseShop>) _shopManager.RetrieveAllVMShops();
                _currentShops = _allShops;
                populateDataGrid();
            }
            catch (NullReferenceException)
            {
                // Form hasn't been instantiated yet. Ignore.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019-02-28
        /// 
        /// Populate the DataGrid with filtered list of shops.
        /// Show active or inactive based on which corresponding
        /// radio button is checked via Lambda expression.
        /// </summary>
        /// <param name="active">Sort by active or inactive.</param>
        private void populateDataGrid()
        {
            dgShops.ItemsSource = _currentShops.Where( s => s.Active == rbtnActive.IsChecked.Value);
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Filter the Shops by Name and/or Building.
        /// </summary>
        public void ApplyFilters()
        {
            try
            {

                _currentShops = _allShops;

                if (txtSearchName.Text.ToString() != "")
                {
                    _currentShops = _currentShops.FindAll(s => s.Name.ToLower().Contains(txtSearchName.Text.ToString().ToLower()));
                }

                if (txtSearchBuilding.Text.ToString() != "")
                {
                    _currentShops = _currentShops.FindAll(s => s.BuildingID.ToLower().Contains(txtSearchBuilding.Text.ToString().ToLower()));
                }

                populateDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Clear the filters and reset the textboxes.
        public void ClearFilters()
        {
            txtSearchBuilding.Text = "";
            txtSearchName.Text = "";

            _currentShops = _allShops;
            populateDataGrid();
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019-02-28
        /// 
        /// Call the filter method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Call the filter clear method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            ClearFilters();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-01
        /// 
        /// Display the Create form.
        /// If the form was saved, refresh the list of Shops and the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var varAddForm = new CreateShop();
            var formResult = varAddForm.ShowDialog();

            if (formResult == true)
            {
                // If the create form was saved,
                // Clear the filters and refresh the grid.
                ClearFilters();
                refreshShops();
            }
        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeactivate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DgShops_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Refresh the shops when Active Shops are selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtnActive_Checked(object sender, RoutedEventArgs e)
        {
            refreshShops();
        }


        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Refresh the shops when Inactive Shops are selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtnInactive_Checked(object sender, RoutedEventArgs e)
        {
            refreshShops();
        }
    }
}
