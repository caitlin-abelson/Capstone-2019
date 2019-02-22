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
        public void ViewSelectedRecord()
        {
            var supplier = (Supplier)dgSuppliers.SelectedItem;

            if (supplier != null)
            {
                var viewSupplierForm = new frmSupplier(supplier);

                // Capture the result of the Dialog.
                var result = viewSupplierForm.ShowDialog();

                if (result == true)
                {
                    // If the form was edited, refresh the datagrid.
                    try
                    {
                        _currentSuppliers = null;
                        _suppliers = _supplierManager.RetrieveAllSuppliers();

                        if (_currentSuppliers == null)
                        {
                            _currentSuppliers = _suppliers;
                        }
                        dgSuppliers.ItemsSource = _currentSuppliers;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message); ;
                    }

                }
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
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadSuppliers_Click(object sender, RoutedEventArgs e)
        {
            ViewSelectedRecord();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/25/19
        /// 
        /// This is a helper method that we can use to populate the data grid with
        /// only active Suppliers.
        /// 
        /// <remarks>
        /// Updated by James Heim
        /// Updated 2019/02/21
        /// Now only populates _currentSuppliers with active suppliers.
        /// </remarks>
        /// </summary>
        private void populateSuppliers()
        {
            try
            {
                _suppliers = _supplierManager.RetrieveAllSuppliers();
                if (_currentSuppliers == null)
                {
                    _currentSuppliers = _suppliers.FindAll(s => s.Active == true);
                }
                dgSuppliers.ItemsSource = _currentSuppliers;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        /// </remarks>
        /// </summary>
        public void FilterSuppliers()
        {
            try
            {
                if (txtSearchSupplierName.Text.ToString() != "")
                {
                    _currentSuppliers = _currentSuppliers.FindAll(s => s.Name.ToLower().Contains(txtSearchSupplierName.Text.ToString().ToLower()));
                }

                if (txtSearchSupplierCity.Text.ToString() != "")
                {
                    _currentSuppliers = _currentSuppliers.FindAll(s => s.City.ToLower().Contains(txtSearchSupplierCity.Text.ToString().ToLower()));
                }

                dgSuppliers.ItemsSource = _currentSuppliers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// 
        /// The Clear button allows the user to clear the filter that they have done so that they can see all of the 
        /// suppliers in the data grid once again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearSuppliers_Click(object sender, RoutedEventArgs e)
        {
            _currentSuppliers = _suppliers;
            dgSuppliers.ItemsSource = _currentSuppliers;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/25/19
        /// 
        /// This method allows us to select which columns we want to show to the user. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgSuppliers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yy";
            }

            string headerName = e.Column.Header.ToString();

            if (headerName == "ContactFirstName")
            {
                e.Cancel = true;
            }
            if (headerName == "ContactLastName")
            {
                e.Cancel = true;
            }
            if (headerName == "Address")
            {
                e.Cancel = true;
            }
            if (headerName == "Country")
            {
                e.Cancel = true;
            }
            if (headerName == "ZipCode")
            {
                e.Cancel = true;
            }
            if (headerName == "Active")
            {
                e.Cancel = true;
            }
            if (headerName == "DateAdded")
            {
                e.Cancel = true;
            }
            if (headerName == "SupplierEmail")
            {
                e.Cancel = true;
            }
            if (headerName == "State")
            {
                e.Cancel = true;
            }
        }

        private void BtnAddSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var createSupplierForm = new frmSupplier();
            var formResult = createSupplierForm.ShowDialog();

            if (formResult == true)
            {
                // If the create form was saved, refresh the datagrid.
                try
                {
                    _currentSuppliers = null;
                    _suppliers = _supplierManager.RetrieveAllSuppliers();

                    if (_currentSuppliers == null)
                    {
                        _currentSuppliers = _suppliers;
                    }
                    dgSuppliers.ItemsSource = _currentSuppliers;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message); ;
                }
            }
        }

        private void DgSuppliers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewSelectedRecord();
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
            try
            {
                var supplier = (Supplier)dgSuppliers.SelectedItem;

                // Remove the record from the list of Active Suppliers.
                _currentSuppliers.Remove(supplier);
                dgSuppliers.Items.Refresh();

                // Set the record to inactive.
                _supplierManager.DeactivateSupplier(supplier);

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

        private void RbtnInactiveSupplier_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
