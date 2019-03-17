using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for frmManageShuttleVehicle.xaml
    /// </summary>
    public partial class FrmManageShuttleVehicle : Window
    {
        private readonly IVehicleManager _vehicleManager;
        private User _user;
        private readonly bool _isEditMode;
        private Vehicle _vehicle;
        private readonly object _callingWindow;

        public FrmManageShuttleVehicle() : this(null, new User(), new Vehicle()) { }

        /// <summary>
        /// Created by : Francis Mingomba
        /// 
        /// </summary>
        /// <param name="callingWindow">Ref to Invoking Form</param>
        /// <param name="user">current user</param>
        /// <param name="vehicle">Vehicle passed in (optional)</param>
        /// <param name="editMode">enable/disable editMode (optional)</param>
        public FrmManageShuttleVehicle(object callingWindow, User user, Vehicle vehicle = null, bool editMode = false)
        {
            _vehicleManager = new VehicleManager();
            _callingWindow = callingWindow;
            _user = user;

            _isEditMode = editMode;
            _vehicle = vehicle;

            InitializeComponent();

            setupWindow();
        }

        private void setupWindow()
        {
            // local function to disable fields
            void DisableFields()
            {
                txtMake.IsReadOnly = true;
                txtModel.IsReadOnly = true;
                btnActivate.IsEnabled = false;
                txtYearOfManufacture.IsReadOnly = true;
                txtLicense.IsReadOnly = true;
                txtMileage.IsReadOnly = true;
                txtVin.IsReadOnly = true;
                txtCapacity.IsReadOnly = true;
                cmbColor.IsEnabled = false;
                cmbTxtColor.IsReadOnly = true;
                txtPurchaseDate.IsEnabled = false;
                txtDescription.IsReadOnly = true;
                btnSave.IsEnabled = false;
            }

            // ... populate combo box
            var colors = new List<string>() { "Red", "Green", "Black", "Blue" };
            cmbTxtColor.Text = "<<Add color here>>";
            foreach (var color in colors)
                cmbColor.Items.Add(color);

            // ... Setting up visibility privileges for textfields
            if (!_isEditMode)  // ... view mode active
            {
                DisableFields();
                this.Title = "View Shuttle Vehicle";
            }
            else // ... either create or edit mode
            {
                this.Title = _vehicle == null ? "Create Shuttle Vehicle" : "Edit Shuttle Vehicle";
            }

            // ... Populate fields
            if (_vehicle != null)
            {
                txtMake.Text = _vehicle.Make;
                txtModel.Text = _vehicle.Model;
                txtYearOfManufacture.Text = _vehicle.YearOfManufacture < 0 ? "" : _vehicle.YearOfManufacture.ToString();
                txtLicense.Text = _vehicle.License;
                txtMileage.Text = _vehicle.Mileage < 0 ? "" : _vehicle.Mileage.ToString();
                txtVin.Text = _vehicle.Vin;
                txtCapacity.Text = _vehicle.Capacity < 0 ? "" : _vehicle.Capacity.ToString();

                txtPurchaseDate.SelectedDate = _vehicle.PurchaseDate;
                txtDescription.Text = _vehicle.Description;
                tbActive.Text = _vehicle.ActiveStr;

                // if color is not in combo box, add it
                if (colors.SingleOrDefault(x => x.Equals(_vehicle.Color)) == null)
                    cmbColor.Items.Add(_vehicle.Color);
                cmbColor.SelectedItem = _vehicle.Color;

                // If vehicle is not active. Display deactivation date grid and disable fields
                if (!_vehicle.Active)
                {
                    gridDeactivationDate.Visibility = Visibility.Visible;
                    DisableFields();
                    btnActivate.IsEnabled = _isEditMode; // Activate only in edit mode
                    txtDeactivationDate.Text = _vehicle.DeactivationDate?.ToShortDateString();
                }
            }
        }

        private string _errorText;
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                var vehicle = new Vehicle
                {
                    Id = _vehicle?.Id ?? 0,
                    Make = txtMake.Text,
                    Model = txtModel.Text,
                    YearOfManufacture = int.Parse(txtYearOfManufacture.Text),
                    License = txtLicense.Text,
                    Mileage = int.Parse(txtMileage.Text),
                    Vin = txtVin.Text,
                    Capacity = int.Parse(txtCapacity.Text),
                    Color = cmbColor.Text,
                    PurchaseDate = txtPurchaseDate.SelectedDate,
                    Description = txtDescription.Text,
                    Active = _vehicle?.Active ?? true,
                    DeactivationDate = _vehicle?.DeactivationDate
                };

                if (save(vehicle))
                {
                    MessageBox.Show(vehicle.Id == 0 ? "Added Successfully" : "Updated Successfully");
                }
            }
            else
            {
                MessageBox.Show(_errorText);
                _errorText = "";
            }
        }

        private void BtnActivate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _vehicleManager.ActivateVehicle(_vehicle);

                // ... synchronize _vehicle (old vehicle) with new database copy
                _vehicle = _vehicleManager.RetrieveVehicleById(_vehicle.Id);

                var caller = (FrmBrowseShuttleVehicles)_callingWindow;
                caller.RefreshShuttleVehiclesDatagrid();

                tbActive.Text = _vehicle.ActiveStr;

                EnableFields();

                gridDeactivationDate.Visibility = Visibility.Collapsed;

                MessageBox.Show("Vehicle activated successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while activating vehicle\n" + ex.Message);
            }

        }

        private bool save(Vehicle vehicle)
        {
            bool result = false;
            try
            {
                if (vehicle.Id == 0) // a vehicle is being created.
                {
                    _vehicleManager.AddVehicle(vehicle);
                    result = true;
                    clearFields();
                }
                else
                {
                    _vehicleManager.UpdateVehicle(_vehicle, vehicle);

                    // ... synchronize _vehicle (old vehicle) with new database copy
                    _vehicle = vehicle;
                    result = true;
                }

                var caller = (FrmBrowseShuttleVehicles)_callingWindow;

                // ... update calling window
                // register calling forms here for live updates
                caller.RefreshShuttleVehiclesDatagrid();
            }
            catch (Exception ex)
            {
                var errorStr = _vehicle == null ? "Error Adding Vehicle\n" : "Error Updating Vehicle\n";
                MessageBox.Show(errorStr + ex.Message);
            }
            return result;
        }

        private bool validateFields()
        {
            bool result = true;

            // ... make
            if (txtMake.Text.Length == 0)
            {
                _errorText += "Make field cannot be empty\n";
                txtMake.BorderBrush = Brushes.Red;
                result = false;
            }

            // ... model
            if (txtModel.Text.Length == 0)
            {
                _errorText += "Model field cannot be empty\n";
                txtModel.BorderBrush = Brushes.Red;
                result = false;
            }

            // ... year
            if (txtYearOfManufacture.Text.Length == 0)
            {
                _errorText += "YearOfManufacture field cannot be empty\n";
                txtYearOfManufacture.BorderBrush = Brushes.Red;
                result = false;
            }
            else if (!int.TryParse(txtYearOfManufacture.Text, out var n))
            {
                _errorText += "YearOfManufacture must be a number\n";
                txtYearOfManufacture.BorderBrush = Brushes.Red;
                result = false;
            }
            else if (n < 0)
            {
                _errorText += "Invalid YearOfManufacture\n";
                txtYearOfManufacture.BorderBrush = Brushes.Red;
                result = false;
            }

            // ... license
            if (txtLicense.Text.Length == 0)
            {
                _errorText += "License field cannot be empty\n";
                txtLicense.BorderBrush = Brushes.Red;
                result = false;
            }
            else if (txtLicense.Text.Length > 10)
            {
                _errorText += "Invalid length\n";
                txtLicense.BorderBrush = Brushes.Red;
                result = false;
            }

            // ...mileage
            if (txtMileage.Text.Length == 0)
            {
                _errorText += "Mileage field cannot be empty\n";
                txtMileage.BorderBrush = Brushes.Red;
                result = false;
            }
            else if (!int.TryParse(txtMileage.Text, out var n))
            {
                _errorText += "Mileage must be a number\n";
                txtMileage.BorderBrush = Brushes.Red;
                result = false;
            }
            else if (n < 0)
            {
                _errorText += "Mileage cannot be negative\n";
                txtMileage.BorderBrush = Brushes.Red;
                result = false;
            }

            // ... vin
            Regex regNoSpecialChar = new Regex("^[a-zA-Z0-9]*$");
            if (txtVin.Text.Length == 0)
            {
                _errorText += "VIN field cannot be empty\n";
                txtVin.BorderBrush = Brushes.Red;
                result = false;
            }
            else if (txtVin.Text.Length != 17)
            {
                _errorText += "VIN should be 17 characters in length \n";
                txtVin.BorderBrush = Brushes.Red;
                result = false;
            }
            else if (!regNoSpecialChar.IsMatch(txtVin.Text))
            {
                _errorText = "Invalid Characters for VIN\n";
                txtVin.BorderBrush = Brushes.Red;
                result = false;
            }

            // ...capacity
            if (txtCapacity.Text.Length == 0)
            {
                _errorText += "Capacity field cannot be empty\n";
                txtCapacity.BorderBrush = Brushes.Red;
                result = false;
            }
            else if (!int.TryParse(txtCapacity.Text, out var n))
            {
                _errorText += "Capacity must be a number\n";
                txtCapacity.BorderBrush = Brushes.Red;
                result = false;
            }
            else if (n < 0)
            {
                _errorText += "Capacity cannot be negative\n";
                txtCapacity.BorderBrush = Brushes.Red;
                result = false;
            }

            // ...color
            if (cmbColor.Text.Length == 0)
            {
                _errorText += "Selection needed for color field\n";
                cmbColor.BorderBrush = Brushes.Red;
                result = false;
            }

            // ...purchase date
            if (txtPurchaseDate.SelectedDate == null)
            {
                _errorText += "Selection needed for purchase date field\n";
                txtPurchaseDate.BorderBrush = Brushes.Red;
                result = false;
            }

            // ...description
            if (txtDescription.Text.Length == 0)
            {
                _errorText += "Description required\n";
                txtDescription.BorderBrush = Brushes.Red;
                result = false;
            }

            return result;
        }

        private void clearFields()
        {
            txtMake.Text = "";
            txtModel.Text = "";
            txtYearOfManufacture.Text = "";
            txtLicense.Text = "";
            txtMileage.Text = "";
            txtVin.Text = "";
            txtCapacity.Text = "";
            cmbColor.SelectedIndex = -1;
            txtPurchaseDate.SelectedDate = DateTime.Now;
            txtDescription.Text = "";
        }

        void EnableFields()
        {
            txtMake.IsReadOnly = false;
            txtModel.IsReadOnly = false;
            txtYearOfManufacture.IsReadOnly = false;
            txtLicense.IsReadOnly = false;
            txtMileage.IsReadOnly = false;
            txtVin.IsReadOnly = false;
            txtCapacity.IsReadOnly = false;
            cmbColor.IsEnabled = true;
            cmbTxtColor.IsReadOnly = false;
            txtPurchaseDate.IsEnabled = true;
            txtDescription.IsReadOnly = false;
            btnSave.IsEnabled = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region TextBox EventHandlers
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SpecialCharValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void TxtLicense_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtLicense.ClearValue(Border.BorderBrushProperty);
        }

        private void TxtMileage_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtMileage.ClearValue(Border.BorderBrushProperty);
        }

        private void TxtModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtModel.ClearValue(Border.BorderBrushProperty);
        }

        private void TxtVin_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtVin.ClearValue(Border.BorderBrushProperty);
        }

        private void TxtMake_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtMake.ClearValue(Border.BorderBrushProperty);
        }

        private void TxtYear_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            txtYearOfManufacture.ClearValue(Border.BorderBrushProperty);
        }

        private void TxtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtDescription.ClearValue(Border.BorderBrushProperty);
        }

        private void TxtCapacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtCapacity.ClearValue(Border.BorderBrushProperty);
        }

        private void TxtPurchaseDate_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPurchaseDate.ClearValue(Border.BorderBrushProperty);
        }

        #endregion
    }
}
