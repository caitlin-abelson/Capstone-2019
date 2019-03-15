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
    /// Author: Dalton Cleveland
    /// Created : 2/21/2019
    /// Interaction logic for DeactivateMaintenanceWorkOrder.xaml
    /// </summary>
    public partial class DeactivateMaintenanceWorkOrder : Window
    {
        MaintenanceWorkOrder _maintenanceWorkOrder;
        IMaintenanceWorkOrderManager _maintenanceWorkOrderManager;

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// This Constructor requires a MaintenanceWorkOrder and an instance of the IRMaintenanceWorkOrderManager 
        /// </summary>
        public DeactivateMaintenanceWorkOrder(MaintenanceWorkOrder maintenanceWorkOrder, IMaintenanceWorkOrderManager maintenanceWorkOrderManager)
        {

            InitializeComponent();
            _maintenanceWorkOrderManager = maintenanceWorkOrderManager;
            _maintenanceWorkOrder = maintenanceWorkOrder;
            txtTitleContent.Text = "Are you sure you want to delete this Maintenance Work Order?";
            txtBodyContent.Text = "Deleting this Item will remove it from our system. If you are unsure whether you should delete this please click cancel and ask your superior";
        }

        /// <summary>
        /// Author: MDalton Cleveland
        /// Created : 2/21/2019
        /// Attempts to delete the MaintenanceWorkOrder in our system when the "delete" button is clicked
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            try
            {
                _maintenanceWorkOrderManager.DeleteMaintenanceWorkOrder(_maintenanceWorkOrder.MaintenanceWorkOrderID, _maintenanceWorkOrder.Complete);
                if (_maintenanceWorkOrder.Complete)
                {
                    message = "This Maintenance Work Order was deactivated successfully";
                }
                else
                {
                    message = "This Maintenance Work Order was deleted successfully";
                }
            }
            catch (Exception ex)
            {
                message = "There was an error deleting this Maintenance Work Order: " + ex.Message;
            }
            MessageBox.Show(message);
            Close();
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Closes the window without deleting or deactivating the Maintenance Work Order
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
