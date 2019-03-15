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
using LogicLayer;
using DataObjects;

namespace Presentation
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 2/21/2019
    /// Interaction logic for BrowseMaintenanceWorkOrders.xaml
    /// </summary>
    public partial class BrowseMaintenanceWorkOrders : Window
    {
       List<MaintenanceWorkOrder> _allMaintenanceWorkOrders;
       List<MaintenanceWorkOrder> _currentMaintenanceWorkOrders;
       MaintenanceWorkOrderManagerMSSQL _maintenanceWorkOrderManager;

        public BrowseMaintenanceWorkOrders()
        {
           InitializeComponent();
           _maintenanceWorkOrderManager = new MaintenanceWorkOrderManagerMSSQL();
           refreshAllMaintenanceWorkOrders();
           populateMaintenanceWorkOrders();
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// gets a list of all Work Orders from our database and updates our lists
        /// </summary>
        private void refreshAllMaintenanceWorkOrders()
        {
            
            try
            {
                _allMaintenanceWorkOrders = _maintenanceWorkOrderManager.RetrieveAllMaintenanceWorkOrders();
                _currentMaintenanceWorkOrders = _allMaintenanceWorkOrders;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// sets the Data Grids Item Source to our current WorkOrders
        /// </summary>
        private void populateMaintenanceWorkOrders()
        {
            dgMaintenanceWorkOrders.ItemsSource = _currentMaintenanceWorkOrders;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when cancel is clicked
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when Add is clicked
        /// </summary>
        private void btnAddMaintenanceWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            var createMaintenanceWorkOrder = new CreateMaintenanceWorkOrder(_maintenanceWorkOrderManager);
            createMaintenanceWorkOrder.ShowDialog();
            refreshAllMaintenanceWorkOrders();
            populateMaintenanceWorkOrders(); 
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when Delete is clicked
        /// </summary>
        private void btnDeleteMaintenanceWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgMaintenanceWorkOrders.SelectedIndex != -1)
            {
                var deleteMaintenanceWorkOrder = new DeactivateMaintenanceWorkOrder(((MaintenanceWorkOrder)dgMaintenanceWorkOrders.SelectedItem), _maintenanceWorkOrderManager);
                deleteMaintenanceWorkOrder.ShowDialog();
                refreshAllMaintenanceWorkOrders();
                populateMaintenanceWorkOrders();
            }
        }


        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when Clear Filters is clicked
        /// </summary>
        private void btnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            _currentMaintenanceWorkOrders = _allMaintenanceWorkOrders;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when Filter is clicked
        /// </summary>
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented Yet");
        }

        private void filterMaintenanceWorkOrders()
        {
            string status = null;
            try
            {
                status = (cboStatus.SelectedItem).ToString();
                _currentMaintenanceWorkOrders = _allMaintenanceWorkOrders.FindAll(m => m.MaintenanceStatusID == status);

                if (cboStatus.SelectedItem.ToString() != null)
                {
                    _currentMaintenanceWorkOrders = _currentMaintenanceWorkOrders.FindAll(m => m.MaintenanceStatusID == cboStatus.SelectedItem.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// This method takes the current list of MaintenanceWorkOrdesr and filters out the deactive ones 
        /// </summary>
        private void filterActiveOnly()
        {
            _currentMaintenanceWorkOrders = _currentMaintenanceWorkOrders.FindAll(x => x.Complete == false);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// This method takes the current list of MaintenanceWorkOrders and filters out the active ones
        /// </summary>
        private void filterDeActiveOnly()
        {
            _currentMaintenanceWorkOrders = _currentMaintenanceWorkOrders.FindAll(x => x.Complete == false);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when a MaintenanceWorkOrder is double clicked
        /// </summary>
        private void dgMaintenanceWorkOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgMaintenanceWorkOrders.SelectedIndex != -1)
            {
                MaintenanceWorkOrder selectedMaintenanceWorkOrder = new MaintenanceWorkOrder();
                try
                {
                    selectedMaintenanceWorkOrder = _maintenanceWorkOrderManager.RetrieveMaintenanceWorkOrder(((MaintenanceWorkOrder)dgMaintenanceWorkOrders.SelectedItem).MaintenanceWorkOrderID);
                    var readUpdateMaintenanceWorkOrder = new CreateMaintenanceWorkOrder(selectedMaintenanceWorkOrder, _maintenanceWorkOrderManager);
                    readUpdateMaintenanceWorkOrder.ShowDialog();
                    refreshAllMaintenanceWorkOrders();
                    populateMaintenanceWorkOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to find that Maintenance Work Order\n" + ex.Message);
                }

            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when the view MaintenanceWorkOrder button is clicked. 
        /// It will launch the CreateMaintenanceWorkOrder window in view mode with the option of updating 
        /// </summary>
        private void btnViewMaintenanceWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgMaintenanceWorkOrders.SelectedIndex != -1)
            {
                MaintenanceWorkOrder selectedMaintenanceWorkOrder = new MaintenanceWorkOrder();
                try
                {
                    selectedMaintenanceWorkOrder = _maintenanceWorkOrderManager.RetrieveMaintenanceWorkOrder(((MaintenanceWorkOrder)dgMaintenanceWorkOrders.SelectedItem).MaintenanceWorkOrderID);
                    var readUpdateMaintenanceWorkOrder = new CreateMaintenanceWorkOrder(selectedMaintenanceWorkOrder, _maintenanceWorkOrderManager);
                    readUpdateMaintenanceWorkOrder.ShowDialog();
                    refreshAllMaintenanceWorkOrders();
                    populateMaintenanceWorkOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to find that Maintenance Work Order\n" + ex.Message);
                }

            }
        }

        private void dgMaintenanceWorkOrders_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
            }

            string headerName = e.Column.Header.ToString();

            if (headerName == "MaintenanceTypeID")
            {
                e.Cancel = true;
            }

            if (headerName == "MaintenanceStatusID")
            {
                e.Cancel = true;
            }
        }
    }
}
