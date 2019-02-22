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
    /// Richard Carroll
    /// Created: 2019/01/31
    /// 
    /// This is the Window used for viewing Internal
    /// Orders in a list View, and allows for selecting
    /// specific ones to view in a Detail Screen.
    /// Also allows for updating the OrderComplete Status
    /// While viewing orders.
    /// </summary>
    public partial class BrowseOrders : Window
    {
        private List<string> _searchCategories = new List<string>();
        private UserManager _userManager = new UserManager();
        private InternalOrderManager _internalOrderManager = new InternalOrderManager();
        private User _fullUser = new User();
        private List<VMInternalOrder> _orders = new List<VMInternalOrder>();
        private List<VMInternalOrder> _currentOrders;
        public BrowseOrders(User fullUser)
        {
            InitializeComponent();
            _fullUser = fullUser;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Filters out either the orders that are completed or the orders that 
        /// are incomplete.
        /// </summary>
        private void ChkbxOrderCompleted_Click(object sender, RoutedEventArgs e)
        {
            if (chkbxOrderCompleted.IsChecked == true)
            {
                _currentOrders = _orders.FindAll(o => o.OrderComplete == true);
                applyFilters();

            }
            else
            {
                _currentOrders = _orders.FindAll(o => o.OrderComplete == false);
                applyFilters();
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Readies the combo box for search categories with options
        /// and fills in the data grid for the first time
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgInternalOrders.Visibility = Visibility.Visible;
            refreshGrid();
            _searchCategories.Add("First Name");
            _searchCategories.Add("Last Name");
            _searchCategories.Add("Department");
            _searchCategories.Add("Description");
            cboSearchCategory.ItemsSource = _searchCategories;

        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Attempts to fill the data grid with the information in
        /// the database.
        /// </summary>
        private void refreshGrid()
        {
            try
            {
                _orders = _internalOrderManager.RetrieveAllInternalOrders();
                dgInternalOrders.ItemsSource = _orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve orders from database: \n" + ex.Message);
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Takes the order information from the grid(if applicable)
        /// and opens a new Detail Window for viewing the order information.
        /// </summary>
        private void DgInternalOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgInternalOrders.SelectedItem != null)
            {
                var order = (VMInternalOrder)dgInternalOrders.SelectedItem;
                var viewOrderDetail = new TestWindow(order);
                viewOrderDetail.ShowDialog();
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Takes the order information from the grid(if applicable)
        /// and opens a new Detail Window for viewing the order information.
        /// </summary>
        private void BtnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (dgInternalOrders.SelectedItem != null)
            {
                var order = (VMInternalOrder)dgInternalOrders.SelectedItem;
                var viewOrderDetail = new TestWindow(order);
                viewOrderDetail.ShowDialog();
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Takes information about the order from the grid and
        /// attempts to update the status of the order to complete,
        /// then refreshes the grid
        /// </summary>
        private void BtnFillOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgInternalOrders.SelectedItem != null)
            {
                
                var order = (VMInternalOrder)dgInternalOrders.SelectedItem;
                if (order.OrderComplete != true)
                {
                    try
                    {
                        if (_internalOrderManager.UpdateOrderStatusToComplete(order.InternalOrderID, order.OrderComplete))
                        {
                            MessageBox.Show("Order status successfully updated");
                            refreshGrid();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update order status");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: failed to update order status: \n" + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Actively changes the filter selected as the 
        /// text changes in the search bar
        /// </summary>
        private void TxtSearchTerm_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cboSearchCategory.SelectedIndex != -1)
            {
                switch (cboSearchCategory.SelectedIndex)
                {
                    case 0:
                        _currentOrders = _orders.FindAll(o => o.FirstName.ToLower()
                        .Contains(txtSearchTerm.Text.ToLower()));
                        applyFilters();
                        break;
                    case 1:
                        _currentOrders = _orders.FindAll(o => o.LastName.ToLower()
                        .Contains(txtSearchTerm.Text.ToLower()));
                        applyFilters();
                        break;
                    case 2:
                        _currentOrders = _orders.FindAll(o => o.DepartmentID.ToLower()
                        .Contains(txtSearchTerm.Text.ToLower()));
                        applyFilters();
                        break;
                    case 3:
                        _currentOrders = _orders.FindAll(o => o.Description.ToLower()
                        .Contains(txtSearchTerm.Text.ToLower()));
                        applyFilters();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Applies the current filters to the data grid and 
        /// updates the grid view to reflect them.
        /// </summary>
        private void applyFilters()
        {
            dgInternalOrders.ItemsSource = _currentOrders;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Removes all current filters from use and 
        /// refreshes the grid to its original state.
        /// </summary>
        private void BtnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            cboSearchCategory.SelectedIndex = -1;
            txtSearchTerm.Text = "";
            dgInternalOrders.ItemsSource = _orders;
        }

        private void BtnAddNewOrder_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                _fullUser = _userManager.RetrieveFullUserByEmail(_fullUser.Email);
                var addOrder = new TestWindow(_fullUser);
                var result = addOrder.ShowDialog();
                if (result == true)
                {
                    refreshGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Retrieve user to Add Orders \n" + ex.Message);
            }
        }
    }
}
