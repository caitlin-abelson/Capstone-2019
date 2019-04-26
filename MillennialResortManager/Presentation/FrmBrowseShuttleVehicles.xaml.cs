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
    /// Francis Mingomba
    /// Created: 2019/04/06
    ///
    /// Interaction logic for frmBrowseShuttleVehicles.xaml
    /// </summary>
    public partial class FrmBrowseShuttleVehicles : UserControl
    {
        private readonly IResortVehicleManager _resortVehicleManager;
        private List<ResortVehicle> _shuttleVehicles;
        private Employee _employee;

        public FrmBrowseShuttleVehicles()
        {
            _resortVehicleManager = new ResortVehicleManager();

            InitializeComponent();
        }


        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/06
        ///
        /// Initializer used by DevLauncher
        /// </summary>
        /// <param name="employee"></param>
        public void setupForm(Employee employee)
        {
            _employee = employee;
            RefreshShuttleVehiclesDatagrid();
        }

        private void DisableControls()
        {
            btnCreateShuttle.IsEnabled = false;
            btnDeactivateVehicle.IsEnabled = false;
            btnEditShuttle.IsEnabled = false;
            btnViewShuttle.IsEnabled = false;
            btnDeleteVehicle.IsEnabled = false;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/06
        ///
        /// Caller by any caller mutating resort vehicles
        /// Done asynchronously to avoid UI hang ups
        /// </summary>
        public async Task RefreshShuttleVehiclesDatagrid()
        {
            await Task.Run(() => GetVehicles());

            if (_shuttleVehicles != null)
                dtgShuttleVehicles.ItemsSource = _shuttleVehicles;
        }

        #region External Resource Facing Logic

        private void GetVehicles()
        {
            try
            {
                _shuttleVehicles = _resortVehicleManager.RetrieveVehicles().ToList();

                // populate resort property string
                foreach (var v in _shuttleVehicles)
                    v.ResortPropertyStr = ((ResortVehicleManager)_resortVehicleManager).RetrieveResortProperties()
                        .SingleOrDefault(x => x.ResortPropertyId == v.ResortPropertyId)
                        ?.ResortPropertyTypeId;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Database Error: Controls shall be disabled.\n\n{e.Message}\n{e.StackTrace}");

                this.Dispatcher.Invoke(DisableControls);                
            }
        }

        #endregion

        #region Event Handlers

        private void BtnCreateShuttle_Click(object sender, RoutedEventArgs e)
        {
            var createShuttleFrm = new FrmManageShuttleVehicle(this, _employee, null, true);

            createShuttleFrm.Show();
        }

        private void BtnViewShuttle_Click(object sender, RoutedEventArgs e)
        {
            var vehicle = (ResortVehicle)dtgShuttleVehicles.SelectedItem;

            // make sure a vehicle is selected
            if (vehicle == null)
            {
                MessageBox.Show("Please select a vehicle");
                return;
            }

            bool editMode = false;
            var createShuttleFrm = new FrmManageShuttleVehicle(this, _employee, vehicle, editMode);
            createShuttleFrm.Show();
        }

        private void BtnEditShuttle_Click(object sender, RoutedEventArgs e)
        {
            var vehicle = (ResortVehicle)dtgShuttleVehicles.SelectedItem;

            // make sure a vehicle is selected
            if (vehicle == null)
            {
                MessageBox.Show("Please select a vehicle");
                return;
            }

            bool editMode = true;
            var createShuttleFrm = new FrmManageShuttleVehicle(this, _employee, vehicle, editMode);
            createShuttleFrm.Show();
        }

        private void BtnDeactivateVehicle_OnClick(object sender, RoutedEventArgs e)
        {
            var vehicle = (ResortVehicle)dtgShuttleVehicles.SelectedItem;

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
                    _resortVehicleManager.DeactivateVehicle(vehicle, _employee);
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
                     || x.Year.ToString().ToLower().Contains(filterTxt)
                     || x.License.ToLower().Contains(filterTxt)
                     || x.Mileage.ToString().ToLower().Contains(filterTxt)
                     || x.AvailableStr.ToLower().Contains(filterTxt)
                     || x.Capacity.ToString().ToLower().Contains(filterTxt)
                     || x.Color.ToString().Contains(filterTxt)
                     || x.PurchaseDate.Value.ToShortDateString().Contains(filterTxt)
                     || x.Color.ToLower().Contains(filterTxt)
                     || x.ActiveStr.ToLower().Contains(filterTxt)
                ).ToList();
        }

        private void BtnDeleteVehicle_OnClick(object sender, RoutedEventArgs e)
        {
            var vehicle = (ResortVehicle)dtgShuttleVehicles.SelectedItem;

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
                    _resortVehicleManager.DeleteVehicle(vehicle, _employee);
                RefreshShuttleVehiclesDatagrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion


    }
}
