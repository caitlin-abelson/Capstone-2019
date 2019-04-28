using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataObjects;
using LogicLayer;

namespace Presentation
{
    /// <summary>
    ///     Francis Mingomba
    ///     Created: 2019/04/19
    ///     Interaction logic for FrmResortVehicleCheckout.xaml
    /// </summary>
    public partial class FrmResortVehicleCheckout : UserControl
    {
        private readonly IResortVehicleCheckoutManager _resortVehicleCheckoutManager;
        private List<ResortVehicle> _availableResortVehicles;
        private List<ResortVehicleCheckoutDecorator> _currentlyCheckedOutResortVehicles;
        private Employee _employee;

        public FrmResortVehicleCheckout()
        {
			_employee = null;
			_resortVehicleCheckoutManager = new ResortVehicleCheckoutManager();
			InitializeComponent();
        }

        public FrmResortVehicleCheckout(Employee employee = null
            , IResortVehicleCheckoutManager resortVehicleCheckoutManager = null)
        {
            _employee = employee;

            _resortVehicleCheckoutManager = resortVehicleCheckoutManager;

            InitializeComponent();
        }

        #region Form Setup

        public void SetupForm(Employee employee)
        {
            _employee = employee;
            RefreshAvailableVehiclesDataGrid();
            RefreshCheckedOutVehiclesDataGrid();
        }

        private void RefreshCheckedOutVehiclesDataGrid()
        {
            _currentlyCheckedOutResortVehicles = GetResortVehicleCheckouts()?.ToList();

            if (_currentlyCheckedOutResortVehicles != null)
                dtgCheckedOutVehicles.ItemsSource = _currentlyCheckedOutResortVehicles;
        }

        private void RefreshAvailableVehiclesDataGrid()
        {
            _availableResortVehicles = GetAvailableResortVehicles()?.ToList();

            if (_availableResortVehicles != null)
                dtgAvailableVehicles.ItemsSource = _availableResortVehicles;
        }

        #endregion

        #region Core Logic

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/19
        ///
        /// Used by Vehicle Checkout Form to Refresh
        /// datagrids.
        /// </summary>
        public void RefreshDataGrids()
        {
            RefreshAvailableVehiclesDataGrid();
            RefreshCheckedOutVehiclesDataGrid();
        }

        private void TxtFilterForAvailable_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // ... filter on everything
            var filterTxt = txtFilterForAvailable.Text.ToLower();

            if (_availableResortVehicles != null)
                dtgAvailableVehicles.ItemsSource = _availableResortVehicles.Where(
                    x => x.Make.ToLower().Contains(filterTxt)
                         || x.Id.ToString().ToLower().Contains(filterTxt)
                         || x.Model.ToLower().Contains(filterTxt)
                         || x.Year.ToString().ToLower().Contains(filterTxt)
                         || x.License.ToLower().Contains(filterTxt)
                         || x.Mileage.ToString().ToLower().Contains(filterTxt)
                         || x.AvailableStr.ToLower().Contains(filterTxt)
                         || x.Capacity.ToString().ToLower().Contains(filterTxt)
                         || x.Color.ToString().Contains(filterTxt)
                         || x.ActiveStr.ToLower().Contains(filterTxt)
                ).ToList();
        }

        private void TxtFilterForCheckedOutVehicles_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // ... filter on everything
            var filterTxt = txtFilterForCheckedOutVehicles.Text.ToLower();

            if (_currentlyCheckedOutResortVehicles != null)
                dtgCheckedOutVehicles.ItemsSource = _currentlyCheckedOutResortVehicles.Where(
                    x => x.VehicleCheckoutId.ToString().ToLower().Contains(filterTxt)
                         || x.ResortVehicleId.ToString().ToLower().Contains(filterTxt)
                         || x.DateCheckedOut.ToShortDateString().ToLower().Contains(filterTxt)
                         || x.DateExpectedBack.ToShortDateString().ToLower().Contains(filterTxt)
                         || x.DateCheckedOut.ToShortDateString().ToLower().Contains(filterTxt)
                         || x.EmployeeFirstName.ToLower().Contains(filterTxt)
                         || x.EmployeeLastName.ToLower().Contains(filterTxt)
                         || x.ResortVehicleMake.ToLower().Contains(filterTxt)
                         || x.ResortVehicleModel.ToLower().Contains(filterTxt)
                ).ToList();
        }

        public void PerformVehicleCheckout(ResortVehicleCheckout vehicleCheckout)
        {
            try
            {
                _resortVehicleCheckoutManager.CheckoutVehicle(vehicleCheckout);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to check out vehicle\n");
            }

            RefreshDataGrids();

            MessageBox.Show("Success");
        }

        private void BtnCheckout_OnClick(object sender, RoutedEventArgs e)
        {
            var vehicle = (ResortVehicle) dtgAvailableVehicles.SelectedItem;

            if (vehicle == null)
            {
                MessageBox.Show("Please select an available vehicle");
                return;
            }

            var prompt = new FrmCheckoutPrompt(this, vehicle);
            prompt.ShowDialog();
        }

        private void BtnCheckIn_OnClick(object sender, RoutedEventArgs e)
        {
            var checkInVehicle = (ResortVehicleCheckoutDecorator)dtgCheckedOutVehicles.SelectedItem;

            if (checkInVehicle == null)
            {
                MessageBox.Show("Please select a vehicle to check in");
                return;
            }

            try
            {
                _resortVehicleCheckoutManager.CheckInVehicle(checkInVehicle.VehicleCheckoutId);

                RefreshDataGrids();

                MessageBox.Show("Success");
            }
            catch (Exception)
            {
                MessageBox.Show($"Unable to check in vehicle\n");
            }
        }

        #endregion

        #region External Resource Facing Functions

        public IEnumerable<ResortVehicle> GetAvailableResortVehicles()
        {
            IEnumerable<ResortVehicle> available = null;

            try
            {
                available = _resortVehicleCheckoutManager.RetrieveAvailableResortVehicles();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failure to retrieve available vehicles.");
            }

            return available;
        }

        public IEnumerable<ResortVehicleCheckoutDecorator> GetResortVehicleCheckouts()
        {
            IEnumerable<ResortVehicleCheckoutDecorator> checkedOut = null;

            try
            {
                checkedOut = _resortVehicleCheckoutManager.RetrieveCurrentlyCheckedOutVehicles();
            }
            catch (Exception)
            {
                MessageBox.Show("Failure to retrieve checked out vehicles.");
            }

            return checkedOut;
        }

        #endregion
    }
}