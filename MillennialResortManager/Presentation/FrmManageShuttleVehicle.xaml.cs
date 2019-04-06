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
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Manage Shuttle Vehicle Class
    /// </summary>
    public partial class FrmManageShuttleVehicle : Window
    {
        private readonly IResortVehicleManager _resortVehicleManager;
        private readonly Employee _employee;
        private readonly bool _isEditMode;
        private ResortVehicle _resortVehicle;
        private readonly object _callingWindow;
        private string _errorText;

        // combo box members
        private List<ResortProperty> _resortProperties;
        List<string> _comboBoxColors = new List<string>() { "Red", "Green", "Black", "Blue" };

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        /// 
        /// </summary>
        /// <param name="callingWindow">Ref to class invoking form</param>
        /// <param name="employee">current employee</param>
        /// <param name="resortVehicle">Vehicle passed in (optional)</param>
        /// <param name="editMode">enable/disable editMode (optional)</param>
        public FrmManageShuttleVehicle(object callingWindow, Employee employee, ResortVehicle resortVehicle = null, bool editMode = false)
        {
            _resortVehicleManager = new ResortVehicleManager();
            _callingWindow = callingWindow;

            _resortVehicle = resortVehicle;
            _employee = employee;

            _isEditMode = editMode;

            InitializeComponent();

            SetupWindow();
        }

        public FrmManageShuttleVehicle() : this(callingWindow: null, employee: new Employee(), resortVehicle: new ResortVehicle()) { }

        #region Form Setup and Management Logic

        private void SetupWindow()
        {
            EnumerateComboBoxes();

            // ... Setting up visibility based on view mode (editMode, viewMode)
            if (!_isEditMode)  // ... view mode active
            {
                DisableFields();

                this.Title = "View Shuttle Vehicle";
            }
            else // ... edit mode active (user can either create or edit)
            {
                this.Title = _resortVehicle == null ? "Create Shuttle Vehicle" : "Edit Shuttle Vehicle";
            }

            // ... Populate fields
            PopulateAllFields();
        }

        private void PopulateAllFields()
        {
            // if _resortVehicle is null (create),
            // return and do not populate fields
            if (_resortVehicle == null)
                return;

            txtMake.Text = _resortVehicle.Make;
            txtModel.Text = _resortVehicle.Model;
            txtYear.Text = _resortVehicle.Year < 0 ? "" : _resortVehicle.Year.ToString();
            txtLicense.Text = _resortVehicle.License;
            txtMileage.Text = _resortVehicle.Mileage < 0 ? "" : _resortVehicle.Mileage.ToString();
            txtCapacity.Text = _resortVehicle.Capacity < 0 ? "" : _resortVehicle.Capacity.ToString();

            txtPurchaseDate.SelectedDate = _resortVehicle.PurchaseDate;
            txtDescription.Text = _resortVehicle.Description;
            tbStatus.Text = _resortVehicle.ResortVehicleStatusId;

            // if color is not in combo box, add it
            if (_comboBoxColors.SingleOrDefault(x => x.Equals(_resortVehicle.Color)) == null)
                cmbColor.Items.Add(_resortVehicle.Color);
            cmbColor.SelectedItem = _resortVehicle.Color;

            // Select Resort Property
            cmbResortProperty.SelectedItem = _resortVehicle.ResortPropertyId.ToString();

            // override view mode (edit mode, view mode)
            // and display deactivation date grid with
            // all other fields disabled.
            if (!_resortVehicle.Active)
            {
                gridDeactivationDate.Visibility = Visibility.Visible;
                DisableFields();
                btnActivate.IsEnabled = _isEditMode; // Activate only in edit mode
                txtDeactivationDate.Text = _resortVehicle.DeactivationDate?.ToShortDateString();
            }
        }

        private void DisableFields()
        {
            txtMake.IsReadOnly = true;
            txtModel.IsReadOnly = true;
            btnActivate.IsEnabled = false;
            txtYear.IsReadOnly = true;
            txtLicense.IsReadOnly = true;
            txtMileage.IsReadOnly = true;
            txtCapacity.IsReadOnly = true;
            cmbColor.IsEnabled = false;
            cmbTxtColor.IsReadOnly = true;
            txtPurchaseDate.IsEnabled = false;
            txtDescription.IsReadOnly = true;
            btnSave.IsEnabled = false;
        }

        private void EnumerateComboBoxes()
        {
            // ... populate color combo box
            cmbTxtColor.Text = "<<Add color here>>";
            PopulateComboBox(_comboBoxColors, cmbColor);

            // ... populate resort property id combo box
            List<string> resortPropertyList = GetResortPropertiesStr();
            PopulateComboBox(resortPropertyList, cmbResortProperty);
        }

        private static void PopulateComboBox(IEnumerable<string> items, ItemsControl cmb)
        {
            foreach (var item in items)
                cmb.Items.Add(item);
        }

        private void ClearFields()
        {
            txtMake.Text = "";
            txtModel.Text = "";
            txtYear.Text = "";
            txtLicense.Text = "";
            txtMileage.Text = "";
            txtCapacity.Text = "";
            cmbColor.SelectedIndex = -1;
            cmbResortProperty.SelectedIndex = -1;
            txtPurchaseDate.SelectedDate = DateTime.Now;
            txtDescription.Text = "";
        }

        private void EnableFields()
        {
            txtMake.IsReadOnly = false;
            txtModel.IsReadOnly = false;
            txtYear.IsReadOnly = false;
            txtLicense.IsReadOnly = false;
            txtMileage.IsReadOnly = false;
            txtCapacity.IsReadOnly = false;
            cmbColor.IsEnabled = true;
            cmbTxtColor.IsReadOnly = false;
            txtPurchaseDate.IsEnabled = true;
            txtDescription.IsReadOnly = false;
            btnSave.IsEnabled = true;
        }

        #endregion

        #region External Resource Facing Functions

        private List<string> GetResortPropertiesStr()
        {
            ResortVehicleManager instance = (ResortVehicleManager)_resortVehicleManager;

            List<string> resortPropertyList = new List<string>();

            try
            {
                _resortProperties = instance.RetrieveResortProperties().ToList();

                resortPropertyList = _resortProperties.Select(x => x.ResortPropertyId.ToString()).ToList();
            }
            catch (Exception e)
            {
                // Disable form if database call fails.
                MessageBox.Show("Failure to retrieve resort properties\nDisabling form ..." + e.InnerException?.Message);

                DisableFields();
            }

            return resortPropertyList;
        }

        private bool Save(ResortVehicle resortVehicle)
        {
            bool result = false;
            try
            {
                if (resortVehicle.Id == 0) // a vehicle is being created.
                {
                    _resortVehicleManager.AddVehicle(resortVehicle);
                    result = true;
                    ClearFields();
                }
                else // vehicle is being updated
                {
                    _resortVehicleManager.UpdateVehicle(_resortVehicle, resortVehicle);

                    // ... synchronize _vehicle (old vehicle) with new database copy
                    _resortVehicle = resortVehicle;
                    result = true;
                }

                var caller = (FrmBrowseShuttleVehicles)_callingWindow;

                // ... update calling window
                // register calling forms here for live updates
                caller.RefreshShuttleVehiclesDatagrid();
            }
            catch (Exception ex)
            {
                var errorStr = _resortVehicle == null ? "Error Adding Vehicle\n" : "Error Updating Vehicle\n";

                MessageBox.Show(errorStr + ex.Message);
            }
            return result;
        }

        #endregion

        #region Core Logic

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                var vehicle = new ResortVehicle
                {
                    Id = _resortVehicle?.Id ?? 0,
                    Make = txtMake.Text,
                    Model = txtModel.Text,
                    Year = int.Parse(txtYear.Text),
                    License = txtLicense.Text,
                    Mileage = int.Parse(txtMileage.Text),
                    Capacity = int.Parse(txtCapacity.Text),
                    Color = cmbColor.Text,
                    PurchaseDate = txtPurchaseDate.SelectedDate,
                    Description = txtDescription.Text,
                    Active = _resortVehicle?.Active ?? true,
                    DeactivationDate = _resortVehicle?.DeactivationDate,
                    Available = _resortVehicle?.Available ?? true,
                    ResortVehicleStatusId = _resortVehicle?.ResortVehicleStatusId ?? ResortVehicleStatuses.Available.ToString(),
                    ResortPropertyId = int.Parse(cmbResortProperty.Text)
                };

                if (Save(vehicle))
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
                _resortVehicleManager.ActivateVehicle(_resortVehicle, _employee);

                // ... synchronize _vehicle (old vehicle) with new database copy
                _resortVehicle = _resortVehicleManager.RetrieveVehicleById(_resortVehicle.Id);

                var caller = (FrmBrowseShuttleVehicles)_callingWindow;
                caller.RefreshShuttleVehiclesDatagrid();

                tbStatus.Text = _resortVehicle.ResortVehicleStatusId;

                EnableFields();

                gridDeactivationDate.Visibility = Visibility.Collapsed;

                MessageBox.Show("Vehicle activated successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while activating vehicle\n" + ex.Message);
            }

        }

        private bool ValidateFields()
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
            if (txtYear.Text.Length == 0)
            {
                _errorText += "Year field cannot be empty\n";
                txtYear.BorderBrush = Brushes.Red;
                result = false;
            }
            else if (!int.TryParse(txtYear.Text, out var n))
            {
                _errorText += "Year must be a number\n";
                txtYear.BorderBrush = Brushes.Red;
                result = false;
            }
            else if (n < 0)
            {
                _errorText += "Invalid Year\n";
                txtYear.BorderBrush = Brushes.Red;
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

        #endregion

        #region Helper Event Handlers

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

        private void TxtMake_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtMake.ClearValue(Border.BorderBrushProperty);
        }

        private void TxtYear_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            txtYear.ClearValue(Border.BorderBrushProperty);
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }

    enum ResortVehicleStatuses
    {
        NotAvailable,
        Available,
        Decommissioned
    }
}
