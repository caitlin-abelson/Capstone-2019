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
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class BrowseSupplier : Window
    {
        private List<Supplier> _suppliers;
        private List<Supplier> _currentSuppliers;
        private SupplierManager _supplierManager = new SupplierManager();

        private string _deactivateMessageBoxText = "Are you sure you want to deactivate {0}?";
        private string _deactivateMessageBoxCaption = "Deactivate Confirmation";
        private string _deactivateConfirmMessageBoxText = "{0} was successfully deactivated.";
        private string _deactivateConfirmMessageBoxCaption = "Deactivate Successful";

        private string _activateMessageBoxText = "Are you sure you want to activate {0}?";
        private string _activateMessageBoxCaption = "Activate Confirmation";
        private string _activateConfirmMessageBoxText = "{0} was successfully activated.";
        private string _activateConfirmMessageBoxCaption = "Activate Successful";

        private string _deleteMessageBoxText = "Are you sure you want to delete {0}?";
        private string _deleteMessageBoxCaption = "Delete Confirmation";
        private string _deleteConfirmMessageBoxText = "{0} was successfully deleted.";
        private string _deleteConfirmMessageBoxCaption = "Delete Successful";


        public BrowseSupplier()
        {
            InitializeComponent();

            populateSuppliers();
        }

        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/01/31
        /// 
        /// View the selected record.
        /// </summary>
        public void ViewSelectedRecord(Supplier supplier)
        {
            var viewSupplierForm = new frmSupplier(supplier);

            // Capture the result of the Dialog.
            var result = viewSupplierForm.ShowDialog();

            if (result == true)
            {
                // If the form was edited, 
                // refresh the suppliers and datagrid.
                refreshSuppliers();
            }
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// 
        /// Calls the procedure to view the selected record.
        /// </summary>
        /// <remarks>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// Brings up the data grid for the user to view.
        /// 
        /// Modified: James Heim
        /// Modified: 2019/01/31
        /// Repurposed the button to view the details for the selected record
        /// since the datagrid populates on form load.
        /// 
        /// Modified by James Heim
        /// Modified on 2019-03-08
        /// Added functionality for activating inactive records.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var selectedSupplier = (Supplier)dgSuppliers.SelectedItem;

            if (selectedSupplier != null)
            {
                // Active Suppliers: View Supplier
                if (rbtnActiveSupplier.IsChecked == true)
                {
                    ViewSelectedRecord(selectedSupplier);
                }
                else if (rbtnInactiveSupplier.IsChecked == true)
                {
                    // Inactive Supplier: Reactivate Supplier.
                    activateSupplier(selectedSupplier);
                }
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019-03-08
        /// 
        /// Loads the suppliers from the database and
        /// repopulate the datagrid.
        ///
        /// <remarks>
        /// Updated by James Heim
        /// Updated 2019-03-08
        /// Cleaned code and made datagrid population its own method.
        /// </remarks>
        /// </summary>
        private void refreshSuppliers()
        {
            try
            {
                _suppliers = _supplierManager.RetrieveAllSuppliers();
                _currentSuppliers = _suppliers;

                populateSuppliers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/25/19
        /// 
        /// This is a helper method that we can use to populate the data grid with
        /// Suppliers.
        /// 
        /// <remarks>
        /// Updated by James Heim
        /// Updated 2019/02/21
        /// Now only populates _currentSuppliers with active suppliers.
        /// 
        /// Updated by James Heim
        /// Updated 2019/03/08
        /// Moved the refresh suppliers code to refreshSuppliers().
        /// Added Sort functionality and shows inactive if the inactive
        /// radio button is checked.
        /// 
        /// </remarks>
        /// </summary>
        private void populateSuppliers()
        {
            _currentSuppliers.Sort((x, y) => string.Compare(x.Name, y.Name));
            dgSuppliers.ItemsSource = _currentSuppliers.Where(s => s.Active == rbtnActiveSupplier.IsChecked);
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// 
        /// Calls the method to filter the datagrid.
        /// </summary>
        /// <remarks>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// The ReadSuppliers button allows for filtering by the company name and city location using lambda expressions.
        /// 
        /// Modified by James Heim
        /// Modified 2019/01/31
        /// Extracted the filter code to a method the button calls.
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterSuppliers();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// 
        /// The ReadSuppliers button allows for filtering by the company name and city location using lambda expressions.
        /// 
        /// <remarks>
        /// Modified by James Heim
        /// Modified 2019/01/31
        /// Moved this code out of BtnFilter_Click into its own method.
        /// 
        /// Modified by James Heim
        /// Modified 2019/03/08
        /// Now prevents cleared filters from stacking.
        /// 
        /// </remarks>
        /// </summary>
        public void FilterSuppliers()
        {
            try
            {

                _currentSuppliers = _suppliers;

                if (txtSearchSupplierName.Text.ToString() != "")
                {
                    _currentSuppliers = _currentSuppliers.FindAll(s => s.Name.ToLower().Contains(txtSearchSupplierName.Text.ToString().ToLower()));
                }

                if (txtSearchSupplierCity.Text.ToString() != "")
                {
                    _currentSuppliers = _currentSuppliers.FindAll(s => s.City.ToLower().Contains(txtSearchSupplierCity.Text.ToString().ToLower()));
                }

                populateSuppliers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: James Heim and Caitlin Abelson
        /// Created 2019-03-08
        /// 
        /// Clear filters and filter textboxes.
        /// Logic was moved here from the button handler.
        /// </summary>
        public void ClearSupplierFilters()
        {
            txtSearchSupplierCity.Text = "";
            txtSearchSupplierName.Text = "";

            _currentSuppliers = _suppliers;
            populateSuppliers();
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// 
        /// The Clear button allows the user to clear the filter that they have done so that they can see all of the 
        /// suppliers in the data grid once again.
        /// </summary>
        /// 
        /// <remarks>
        /// Updated by James Heim
        /// Updated 2019-03-08
        /// Moved logic to a public method.
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearSuppliers_Click(object sender, RoutedEventArgs e)
        {
            ClearSupplierFilters();
        }

        /// <summary>
        /// Author Caitlin Abelson
        /// Created 1/25/19
        /// 
        /// Add the selected Supplier.
        /// 
        /// <remarks>
        /// Updated by James Heim
        /// Updated 2019-03-08
        /// 
        /// </remarks>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var createSupplierForm = new frmSupplier();
            var formResult = createSupplierForm.ShowDialog();

            if (formResult == true)
            {
                // If the create form was saved,
                // clear the filters and refresh the datagrid.
                ClearSupplierFilters();
                refreshSuppliers();
            }
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-01-25
        /// 
        /// Either view an active supplier or activate an inactive supplier.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Updated by James Heim
        /// Updated 2019-03-08
        /// 
        /// Now handles reactivating inactive suppliers.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgSuppliers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedSupplier = (Supplier)dgSuppliers.SelectedItem;

            if (selectedSupplier != null)
            {
                // Active Suppliers: View Supplier
                if (rbtnActiveSupplier.IsChecked == true)
                {
                    ViewSelectedRecord(selectedSupplier);
                }
                else if (rbtnInactiveSupplier.IsChecked == true)
                {
                    // Inactive Supplier: Reactivate Supplier.
                    activateSupplier(selectedSupplier);
                }
            }
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019/02/21
        /// 
        /// Handle logic for deleting a record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteSuppliers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var supplier = (Supplier)dgSuppliers.SelectedItem;

                // Remove the supplier from the Grid to update faster.
                _currentSuppliers.Remove(supplier);
                dgSuppliers.Items.Refresh();


                // Remove the supplier from the DB.
                _supplierManager.DeleteSupplier(supplier);

                // Refresh the Supplier List.
                _currentSuppliers = null;
                populateSuppliers();
            }
            catch (NullReferenceException)
            {
                // Nothing selected. Do nothing.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019/02/21
        /// 
        /// Set the Supplier to Inactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeactivateSuppliers_Click(object sender, RoutedEventArgs e)
        {

            var selectedSupplier = (Supplier)dgSuppliers.SelectedItem;

            if (selectedSupplier != null)
            {
                if (rbtnActiveSupplier.IsChecked == true)
                {
                    // Deactivate
                    deactivateSupplier(selectedSupplier);
                }
                else if (rbtnInactiveSupplier.IsChecked == true)
                {
                    // Delete.
                    deleteSupplier(selectedSupplier);
                }
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019-03-08
        /// 
        /// Deactivates the supplier passed in.
        /// </summary>
        private void deactivateSupplier(Supplier supplier)
        {
            bool result = false;

            // Prompt the user with a confirmation dialog.
            var messageBoxResult = MessageBox.Show(
                string.Format(_deactivateMessageBoxText, supplier.Name),
                _deactivateMessageBoxCaption,
                MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    var selectedSupplier = _supplierManager.RetrieveSupplier(supplier.SupplierID);

                    result = _supplierManager.DeactivateSupplier(selectedSupplier);
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
                        string.Format(_deactivateConfirmMessageBoxText, supplier.Name),
                        _deactivateConfirmMessageBoxCaption);


                    refreshSuppliers();
                }
            }

        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019-03-09
        /// 
        /// Activates the Supplier passed in.
        /// </summary>
        /// <returns>Whether the shop was activated</returns>
        public void activateSupplier(Supplier supplier)
        {

            // Prompt the user with a confirmation dialog.
            var messageBoxResult = MessageBox.Show(
                string.Format(_activateMessageBoxText, supplier.Name),
                _activateMessageBoxCaption,
                MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    _supplierManager.ActivateSupplier(supplier);

                    MessageBox.Show(
                        string.Format(_activateConfirmMessageBoxText, supplier.Name),
                        _activateConfirmMessageBoxCaption);

                    refreshSuppliers();
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
            }
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-08
        /// 
        /// Permanently delete the selected supplier.
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        private bool deleteSupplier(Supplier supplier)
        {
            bool result = false;

            // Prompt the user with a confirmation dialog.
            var messageBoxResult = MessageBox.Show(
                string.Format(_deleteMessageBoxText, supplier.Name),
                _deleteMessageBoxCaption,
                MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    var selectedShop = _supplierManager.RetrieveSupplier(supplier.SupplierID);

                    result = _supplierManager.DeleteSupplier(selectedShop);
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
                        string.Format(_deleteConfirmMessageBoxText, supplier.Name),
                        _deleteConfirmMessageBoxCaption);


                    refreshSuppliers();
                }
            }

            return result;
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-08
        /// 
        /// Frefresh the Suppliers when the inactive checkbox is checked
        /// as well as updating the buttons appropriately.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtnInactiveSupplier_Checked(object sender, RoutedEventArgs e)
        {
            btnDeactivateSuppliers.Content = "Delete";
            btnReadSuppliers.Content = "Activate";

            refreshSuppliers();
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-08
        /// 
        /// Refresh the Suppliers when the active checkbox is checked
        /// as well as updating the buttons appropriately.
        /// </summary>
        /// <remarks>
        /// James Heim
        /// Updated 2019-03-07
        /// Added button content switching to the active context.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtnActiveSupplier_Checked(object sender, RoutedEventArgs e)
        {
            // Wait until the form is fully loaded.
            if (btnDeactivateSuppliers != null)
            {
                btnDeactivateSuppliers.Content = "Deactivate";
                btnReadSuppliers.Content = "View";
            }

            refreshSuppliers();
        }
    }
}
