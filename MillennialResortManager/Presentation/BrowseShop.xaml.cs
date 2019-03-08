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

        private string _deactivateMessageBoxText = "Are you sure you want to deactivate {0}?";
        private string _deactivateMessageBoxCaption = "Deactivate Confirmation";
        private string _deactivateConfirmMessageBoxText = "{0} was successfully deactivated.";
        private string _deactivateConfirmMessageBoxCaption = "Deactivate Successful";

        private string _activateMessageBoxText = "Are you sure you want to activate {0}?";
        private string _activateMessageBoxCaption = "Activate Confirmation";
        private string _activateConfirmMessageBoxText = "{0} was successfully activated.";
        private string _activateConfirmMessageBoxCaption = "Activate Successful";

        private string _deleteMessageBoxText = "Are you sure you want to delete {0}?" +
            "\nWARNING: This cannot be undone.";
        private string _deleteMessageBoxCaption = "Delete Confirmation";
        private string _deleteConfirmMessageBoxText = "{0} was successfully deleted.";
        private string _deleteConfirmMessageBoxCaption = "Delete Successful";

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
                _allShops = (List<VMBrowseShop>)_shopManager.RetrieveAllVMShops();
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
        /// Populate the DataGrid with filtered list of shops sorted alphabetically.
        /// Show active or inactive based on which corresponding
        /// radio button is checked via Lambda expression.
        /// </summary>
        /// 
        /// <remarks>
        /// James Heim
        /// Modified 2019-03-07
        /// Updated to sort by alphabetical name.
        /// </remarks>
        /// <param name="active">Sort by active or inactive.</param>
        private void populateDataGrid()
        {
            _currentShops.Sort((x, y) => string.Compare(x.Name, y.Name));
            dgShops.ItemsSource = _currentShops.Where(s => s.Active == rbtnActive.IsChecked.Value);
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

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-07
        /// 
        /// Either View an active shop or activate a deactivated shop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            var selectedShopVM = (VMBrowseShop)dgShops.SelectedItem;

            if (selectedShopVM != null)
            {
                if (rbtnActive.IsChecked == true)
                {
                    // View the Shop.

                }
                else if (rbtnInactive.IsChecked == true)
                {
                    // Activate the Shop.
                    activateShop(selectedShopVM);
                }
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019-03-07
        /// 
        /// Either deactivate an active shop or delete a deactivated shop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            var selectedShopVM = (VMBrowseShop)dgShops.SelectedItem;

            if (selectedShopVM != null)
            {
                if (rbtnActive.IsChecked == true)
                {
                    // Deactivate
                    deactivateShop(selectedShopVM);
                }
                else if (rbtnInactive.IsChecked == true)
                {
                    // Delete.
                    deleteShop(selectedShopVM);
                }
            }

        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-08
        /// 
        /// Permanently delete the selected shop.
        /// </summary>
        /// <param name="vmShop"></param>
        /// <returns></returns>
        private bool deleteShop(VMBrowseShop vmShop)
        {
            bool result = false;

            // Prompt the user with a confirmation dialog.
            var messageBoxResult = MessageBox.Show(
                string.Format(_deleteMessageBoxText, vmShop.Name),
                _deleteMessageBoxCaption,
                MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    var selectedShop = _shopManager.RetrieveShopByID(vmShop.ShopID);

                    result = _shopManager.DeleteShop(selectedShop);
                }
                catch (NullReferenceException nre)
                {

                    MessageBox.Show("Could not find the Shop in the database.\n" +
                        nre.Message + "\n" + nre.InnerException);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException);
                }

                if (result)
                {
                    MessageBox.Show(
                        string.Format(_deleteConfirmMessageBoxText, vmShop.Name),
                        _deleteConfirmMessageBoxCaption);


                    refreshShops();
                }
            }

            return result;
        }


        /// <summary>
        /// Author: James Heim
        /// Created 2019-03-07
        /// 
        /// Deactivates the shop passed in.
        /// </summary>
        /// <returns>Whether the Shop was deactivated</returns>
        private bool deactivateShop(VMBrowseShop vmShop)
        {
            bool result = false;

            // Prompt the user with a confirmation dialog.
            var messageBoxResult = MessageBox.Show(
                string.Format(_deactivateMessageBoxText, vmShop.Name),
                _deactivateMessageBoxCaption,
                MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    var selectedShop = _shopManager.RetrieveShopByID(vmShop.ShopID);

                    result = _shopManager.DeactivateShop(selectedShop);
                }
                catch (NullReferenceException nre)
                {

                    MessageBox.Show("Could not find the Shop in the database.\n" +
                        nre.Message + "\n" + nre.InnerException);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException);
                }

                if (result)
                {
                    MessageBox.Show(
                        string.Format(_deactivateConfirmMessageBoxText, vmShop.Name),
                        _deactivateConfirmMessageBoxCaption);


                    refreshShops();
                }
            }

            return result;
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019-03-07
        /// 
        /// Activates the shop passed in.
        /// </summary>
        /// <returns>Whether the shop was activated</returns>
        private bool activateShop(VMBrowseShop vmShop)
        {
            bool result = false;

            // Prompt the user with a confirmation dialog.
            var messageBoxResult = MessageBox.Show(
                string.Format(_activateMessageBoxText, vmShop.Name),
                _activateMessageBoxCaption,
                MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    var selectedShop = _shopManager.RetrieveShopByID(vmShop.ShopID);

                    result = _shopManager.ActivateShop(selectedShop);
                }
                catch (NullReferenceException nre)
                {

                    MessageBox.Show("Could not find the Shop in the database.\n" +
                        nre.Message + "\n" + nre.InnerException);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException);
                }

                if (result)
                {
                    MessageBox.Show(
                        string.Format(_activateConfirmMessageBoxText, vmShop.Name),
                        _activateConfirmMessageBoxCaption);


                    refreshShops();
                }
            }

            return result;
        }

        private void DgShops_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Refresh the shops when the active checkbox is checked
        /// as well as updating the buttons appropriately.
        /// </summary>
        /// <remarks>
        /// James Heim
        /// Updated 2019-03-07
        /// Added button content switching to the active context.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtnActive_Checked(object sender, RoutedEventArgs e)
        {
            // Wait until form is fully loaded.
            if (btnDeactivate != null)
            {
                btnDeactivate.Content = "Deactivate";
                btnView.Content = "View";
            }

            refreshShops();
        }


        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Refresh the shops when the inactive checkbox is checked
        /// as well as updating the buttons appropriately.
        /// </summary>
        /// <remarks>
        /// James Heim
        /// Updated 2019-03-07
        /// Added button content switching to the inactive context.
        /// </remarks>
        private void RbtnInactive_Checked(object sender, RoutedEventArgs e)
        {
            btnDeactivate.Content = "Delete";
            btnView.Content = "Activate";

            refreshShops();
        }
    }
}
