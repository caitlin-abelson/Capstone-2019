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
    /// Author: Richard Carroll
    /// Created Date: 3/7/19
    /// 
    /// This Window Allows for Displaying GuestVehicle Data in a 
    /// List View, and has filter functionality.
    /// </summary>
    public partial class BrowseGuestVehicles : Window
    {
        private List<VMGuestVehicle> _vehicles = new List<VMGuestVehicle>();
        private GuestVehicleManager _guestVehicleManager = new GuestVehicleManager();
        private List<string> _searchOptions = new List<string>();
        private List<VMGuestVehicle> _currentList;

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// The Basic Constructor. Used for Create Functionality
        /// </summary>
        public BrowseGuestVehicles()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Refreshes the Grid and
        /// Fills the Search options for the combo box
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshGrid();
            fillOptions();
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Makes a Detail Form for adding a new GuestVehicle
        /// </summary>
        private void BtnAddNewGuestVehicle_Click(object sender, RoutedEventArgs e)
        {
            var guestVehicleDetail = new GuestVehicleDetail();
            var result = guestVehicleDetail.ShowDialog();
            if (result == true)
            {
                refreshGrid();
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Sets the combo box and Search bar to blank, and refreshes the Grid
        /// </summary>
        private void BtnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            cboSearchCategory.SelectedIndex = -1;
            txtSearchTerm.Text = "";
            refreshGrid();
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Searches through the existing Grid for data matching what's in the search bar
        /// with what's in the Grid
        /// </summary>
        private void TxtSearchTerm_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cboSearchCategory.SelectedIndex != -1)
            {
                switch (cboSearchCategory.SelectedIndex)
                {
                    case 0:
                        _currentList = _vehicles.FindAll(v => v.FirstName.ToLower().Contains(txtSearchTerm.Text));
                        applyFilters();
                        break;
                    case 1:
                        _currentList = _vehicles.FindAll(v => v.LastName.ToLower().Contains(txtSearchTerm.Text));
                        applyFilters();
                        break;
                    case 2:
                        _currentList = _vehicles.FindAll(v => v.Make.ToLower().Contains(txtSearchTerm.Text));
                        applyFilters();
                        break;
                    case 3:
                        _currentList = _vehicles.FindAll(v => v.Model.ToLower().Contains(txtSearchTerm.Text));
                        applyFilters();
                        break;
                    case 4:
                        _currentList = _vehicles.FindAll(v => v.Color.ToLower().Contains(txtSearchTerm.Text));
                        applyFilters();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Opens a Detail Form for Viewing the Details of a GuestVehicle
        /// </summary>
        private void DgGuestVehicles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgGuestVehicles.SelectedIndex != -1)
            {
                VMGuestVehicle vehicle = (VMGuestVehicle)dgGuestVehicles.SelectedItem;
                var guestVehicleDetail = new GuestVehicleDetail(vehicle, false);
                var result = guestVehicleDetail.ShowDialog();
                if (result == true)
                {
                    refreshGrid();
                }
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Opens a Detail Form for Viewing the Details of a GuestVehicle
        /// </summary>
        private void BtnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (dgGuestVehicles.SelectedIndex != -1)
            {
                VMGuestVehicle vehicle = (VMGuestVehicle)dgGuestVehicles.SelectedItem;
                var guestVehicleDetail = new GuestVehicleDetail(vehicle, false);
                guestVehicleDetail.ShowDialog();
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Opens a Detail Form for Updating a GuestVehicle
        /// </summary>
        private void BtnUpdateGuestVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (dgGuestVehicles.SelectedIndex != -1)
            {
                VMGuestVehicle vehicle = (VMGuestVehicle)dgGuestVehicles.SelectedItem;
                var guestVehicleDetail = new GuestVehicleDetail(vehicle, true);
                var result = guestVehicleDetail.ShowDialog();
                if (result == true)
                {
                    refreshGrid();
                }
            }

        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Refreshes the Grid
        /// </summary>
        private void refreshGrid()
        {
            try
            {
                _vehicles = _guestVehicleManager.RetrieveAllGuestVehicles();
                _currentList = _vehicles;
                dgGuestVehicles.ItemsSource = null;
                dgGuestVehicles.ItemsSource = _vehicles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Load Vehicle List: \n" + ex.Message);
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Fills the Search options for the combo box
        /// </summary>
        private void fillOptions()
        {
            _searchOptions.Add("First Name");
            _searchOptions.Add("Last Name");
            _searchOptions.Add("Make");
            _searchOptions.Add("Model");
            _searchOptions.Add("Color");
            cboSearchCategory.ItemsSource = _searchOptions;
        }

        private void applyFilters()
        {
            dgGuestVehicles.ItemsSource = _currentList;
        }

        private void CboSearchCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboSearchCategory.SelectedIndex != -1)
            {
                switch (cboSearchCategory.SelectedIndex)
                {
                    case 0:
                        _currentList = _vehicles.FindAll(v => v.FirstName.ToLower().Contains(txtSearchTerm.Text));
                        applyFilters();
                        break;
                    case 1:
                        _currentList = _vehicles.FindAll(v => v.LastName.ToLower().Contains(txtSearchTerm.Text));
                        applyFilters();
                        break;
                    case 2:
                        _currentList = _vehicles.FindAll(v => v.Make.ToLower().Contains(txtSearchTerm.Text));
                        applyFilters();
                        break;
                    case 3:
                        _currentList = _vehicles.FindAll(v => v.Model.ToLower().Contains(txtSearchTerm.Text));
                        applyFilters();
                        break;
                    case 4:
                        _currentList = _vehicles.FindAll(v => v.Color.ToLower().Contains(txtSearchTerm.Text));
                        applyFilters();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
