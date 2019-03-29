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
    /// Eric Bostwick
    /// Created: 3/6/2019
    /// Form for managing supplier orders
    /// </summary>  
    public partial class frmManageSupplierOrders : Window
    {
        private SupplierOrderManager _supplierOrderManager = new SupplierOrderManager();
        private SupplierManager _supplierManager = new SupplierManager();
        private SupplierOrder _supplierOrder;
        private List<Supplier> _suppliers;
        private List<SupplierOrder>  _supplierOrders;
        private List<SupplierOrder> _currentSupplierOrders;
        public frmManageSupplierOrders()
        {
            InitializeComponent();
            LoadSupplierCombo();
            LoadSupplierOrderGrid();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CboSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {            
                MessageBoxResult result;
                result = MessageBox.Show("Do You Really Want to Cancel Managing Supplier Orders?", "Cancel Supplier Order Management", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Close();
                }
                else
                {
                    return;
                }
        }
        

        private void LoadSupplierCombo()
        {
            try
            {
                _suppliers = _supplierManager.RetrieveAllSuppliers();
                cboSupplier.Items.Clear();
                foreach (Supplier supplier in _suppliers)
                {
                    cboSupplier.Items.Add(supplier.Name + " " + supplier.SupplierID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadSupplierOrderGrid()
        {
            try
            {
                _supplierOrders = _supplierOrderManager.RetrieveAllSupplierOrders();
                _currentSupplierOrders = _supplierOrders;

                dgSupplierOrders.ItemsSource = _supplierOrders;

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgSupplierOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
                _supplierOrder = (SupplierOrder)dgSupplierOrders.SelectedItem;

                var supplierOrderManager = new frmAddEditSupplierOrder(_supplierOrder, EditMode.Edit);
                var result = supplierOrderManager.ShowDialog();
            if (result == true)
            {
                LoadSupplierCombo();
                LoadSupplierOrderGrid();
            }
            else
            {
                return;
            }
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            var supplierOrderManager = new frmAddEditSupplierOrder();
            var result = supplierOrderManager.ShowDialog();
            if (result == true)
            {
                LoadSupplierCombo();
                LoadSupplierOrderGrid();
            }
            else
            {
                return;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            cboSupplier.Text = "";
            dgSupplierOrders.ItemsSource = _supplierOrders;
            dgSupplierOrders.Items.Refresh();
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            if(cboSupplier.Text.Length > 6)
            {
                int iSupplierID = int.Parse(cboSupplier.Text.Substring(cboSupplier.Text.Length - 6, 6));
                FilterOrders(iSupplierID);
            }       

           

        }
        public void FilterOrders(int iSupplierID)
        {
            try
            {               
                _currentSupplierOrders = _supplierOrders.FindAll(s => s.SupplierID == iSupplierID);

                dgSupplierOrders.ItemsSource = _currentSupplierOrders;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {   
            if((SupplierOrder)dgSupplierOrders.SelectedItem != null)
            {
                _supplierOrder = (SupplierOrder)dgSupplierOrders.SelectedItem;
                MessageBoxResult mbresult;

                mbresult = MessageBox.Show("Are you sure you want to delete order number " + _supplierOrder.SupplierOrderID  + "?", "Delete Order", MessageBoxButton.YesNo);

                if (mbresult == MessageBoxResult.No)
                {
                    return;
                }
                else
                {                    
                    if(1 == _supplierOrderManager.DeleteSupplierOrder(_supplierOrder.SupplierOrderID))
                    {
                        MessageBox.Show("Record Deleted");
                        LoadSupplierOrderGrid();
                    }

                }                
                
            }     
           
        }
        /// <summary>
        /// Kevin Broskow
        /// 3/29/2019
        /// Added functionality to recieve external orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceived_Click(object sender, RoutedEventArgs e)
        {
            if ((SupplierOrder)dgSupplierOrders.SelectedItem != null)
            {
                _supplierOrder = (SupplierOrder)dgSupplierOrders.SelectedItem;

                var orderReceived = new OrderRecieving(_supplierOrder);
                var result = orderReceived.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must select an order");
            }
        }
    }
}
