using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for frmBrowseShuttleVehicles.xaml
    /// </summary>
    public partial class FrmBrowseShuttleVehicles : UserControl
    {
        private readonly IVehicleManager _vehicleManager;
        private List<Vehicle> _shuttleVehicles;
        private readonly User _user;

        public FrmBrowseShuttleVehicles(User user = null)
        {
            _user = user ?? new User();
            _vehicleManager = new VehicleManager();
            InitializeComponent();
        }

        /// <summary>
        /// Call constructor overload to setup window
        /// </summary>
        public FrmBrowseShuttleVehicles() : this(new User()) { }

        /// <summary>
        /// Created By: Francis Mingomba
        /// Description: Called by frmManageShuttleVehicle when 
        ///              a vehicle is created or updated
        ///              Do it on another thread to avoid hanging
        ///              form.
        /// </summary>
        public async Task RefreshShuttleVehiclesDatagrid()
        {
            await Task.Run(() => getVehicles());

            if (_shuttleVehicles != null)
                dtgShuttleVehicles.ItemsSource = _shuttleVehicles;
        }

        private void getVehicles()
        {
            try
            {
                _shuttleVehicles = _vehicleManager.RetrieveVehicles().ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace);
            }
        }

        private void BtnCreateShuttle_Click(object sender, RoutedEventArgs e)
        {
            var createShuttleFrm = new FrmManageShuttleVehicle(this, _user, null, true);
            createShuttleFrm.Show();
        }

        private void BtnViewShuttle_Click(object sender, RoutedEventArgs e)
        {
            var vehicle = (Vehicle)dtgShuttleVehicles.SelectedItem;

            // make sure a vehicle is selected
            if (vehicle == null)
            {
                MessageBox.Show("Please select a vehicle");
                return;
            }

            var createShuttleFrm = new FrmManageShuttleVehicle(this, _user, vehicle, false);
            createShuttleFrm.Show();
        }

        private void BtnEditShuttle_Click(object sender, RoutedEventArgs e)
        {
            var vehicle = (Vehicle)dtgShuttleVehicles.SelectedItem;

            // make sure a vehicle is selected
            if (vehicle == null)
            {
                MessageBox.Show("Please select a vehicle");
                return;
            }

            var createShuttleFrm = new FrmManageShuttleVehicle(this, _user, vehicle, true);
            createShuttleFrm.Show();
        }

        private void BtnDeactivateVehicle_OnClick(object sender, RoutedEventArgs e)
        {
            var vehicle = (Vehicle)dtgShuttleVehicles.SelectedItem;

            // make sure a vehicle is selected
            if (vehicle == null)
            {
                MessageBox.Show("Please select a vehicle");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to deactivate this vehicle?", "Warning", MessageBoxButton.YesNo);

            try
            {
                if (result == MessageBoxResult.Yes)
                    _vehicleManager.DeactivateVehicle(vehicle);
                RefreshShuttleVehiclesDatagrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured when trying to deactivate vehicle\n" + ex.Message);
            }
        }

        private void TxtFilterVehicles_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... filter on everything
            string filterTxt = txtFilterVehicles.Text.ToLower();

            dtgShuttleVehicles.ItemsSource = _shuttleVehicles.Where(
                x => x.Make.ToLower().Contains(filterTxt)
                     || x.Model.ToLower().Contains(filterTxt)
                     || x.YearOfManufacture.ToString().ToLower().Contains(filterTxt)
                     || x.License.ToLower().Contains(filterTxt)
                     || x.Mileage.ToString().ToLower().Contains(filterTxt)
                     || x.Vin.ToLower().Contains(filterTxt)
                     || x.Capacity.ToString().ToLower().Contains(filterTxt)
                     || x.Color.ToString().Contains(filterTxt)
                     || x.PurchaseDate.Value.ToShortDateString().Contains(filterTxt)
                     || x.Color.ToLower().Contains(filterTxt)
                     || x.ActiveStr.ToLower().Contains(filterTxt)
                ).ToList();
        }

        private void BtnDeleteVehicle_OnClick(object sender, RoutedEventArgs e)
        {
            var vehicle = (Vehicle)dtgShuttleVehicles.SelectedItem;

            // make sure a vehicle is selected
            if (vehicle == null)
            {
                MessageBox.Show("Please select a vehicle");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this vehicle?", "Warning", MessageBoxButton.YesNo);

            try
            {
                if (result == MessageBoxResult.Yes)
                    _vehicleManager.DeleteVehicle(vehicle, _user);
                RefreshShuttleVehiclesDatagrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
