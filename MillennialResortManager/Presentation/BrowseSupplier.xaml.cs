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
        private List<Suppliers> _suppliers;
        private List<Suppliers> _currentSuppliers;
        private SupplierManager _supplierManager = new SupplierManager();
        public BrowseSupplier()
        {
            InitializeComponent();
            populateSuppliers();
        }




        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// 
        /// Brings up the data grid for the user to view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadSuppliers_Click(object sender, RoutedEventArgs e)
        {
            populateSuppliers();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/25/19
        /// 
        /// This is a helper method that we can use to populate the data grid when the 
        /// program starts.
        /// </summary>
        private void populateSuppliers()
        {
            try
            {
                _suppliers = _supplierManager.GetAllSuppliers();
                if (_currentSuppliers == null)
                {
                    _currentSuppliers = _suppliers;
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
        /// The ReadSuppliers button allows for filtering by the company name and city location using lambda expressions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFilter_Click(object sender, RoutedEventArgs e)
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


    }
}
