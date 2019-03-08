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
    /// Author: Richard Carroll
    /// Created Date: 3/1/19
    /// 
    /// This Window Allows for Displaying GuestVehicle Data, 
    /// and the Adding to and the Updating of Vehicles in 
    /// the Database.
    /// </summary>
    public partial class GuestVehicleDetail : Window
    {
        private List<Guest> _guests = new List<Guest>();
        private GuestManager _guestManager = new GuestManager();
        private List<string> _guestNames = new List<string>();
        private GuestVehicle _guestVehicle;
        private GuestVehicle _newGuestVehicle;
        private GuestVehicleManager _guestVehicleManager = new GuestVehicleManager();
        private bool _isEditable;
        private VMGuestVehicle _vmGuestVehicle;

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// The Basic Constructor. Used for Create Functionality
        /// </summary>
        public GuestVehicleDetail()
        {
            InitializeComponent();
            _isEditable = true;
            btnUpdate.Visibility = Visibility.Hidden;
            
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// The Secondary Constructor. Used for both Read and Update
        /// Functionality.
        /// </summary>
        public GuestVehicleDetail(VMGuestVehicle vehicle, bool editable)
        {
            InitializeComponent();
            _isEditable = editable;
            _vmGuestVehicle = vehicle;
            _guestVehicle = new GuestVehicle()
            {
                GuestID = vehicle.GuestID,
                Make = vehicle.Make,
                Model = vehicle.Model,
                PlateNumber = vehicle.PlateNumber,
                Color = vehicle.Color,
                ParkingLocation = vehicle.ParkingLocation
            };
            if (_isEditable)
            {
                setupEditable();
            }
            else
            {
                setupReadOnly();
            }
            populateFields();
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Fills the Form with data from the dataGrid on BrowseGuestVehicles.
        /// </summary>
        private void populateFields()
        {
            _guestNames = new List<string>();
            _guestNames.Add(_vmGuestVehicle.FirstName + " " + _vmGuestVehicle.LastName);
            cboGuestID.ItemsSource = _guestNames;
            cboGuestID.SelectedIndex = 0;
            txtMake.Text = _vmGuestVehicle.Make;
            txtModel.Text = _vmGuestVehicle.Model;
            txtPlateNum.Text = _vmGuestVehicle.PlateNumber;
            txtColor.Text = _vmGuestVehicle.Color;
            txtParkingLocation.Text = _vmGuestVehicle.ParkingLocation;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Makes all the fields read only, used for Detail Viewing.
        /// </summary>
        private void setupReadOnly()
        {
            cboGuestID.IsEnabled = false;
            txtMake.IsReadOnly = true;
            txtModel.IsReadOnly = true;
            txtPlateNum.IsReadOnly = true;
            txtColor.IsReadOnly = true;
            txtParkingLocation.IsReadOnly = true;
            btnCancel.Content = "Back";
            btnSave.Visibility = Visibility.Hidden;
            btnUpdate.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Makes all the fields Editable, used for Updating.
        /// </summary>
        private void setupEditable()
        {
            cboGuestID.IsEnabled = true;
            txtMake.IsReadOnly = false;
            txtModel.IsReadOnly = false;
            txtPlateNum.IsReadOnly = false;
            txtColor.IsReadOnly = false;
            txtParkingLocation.IsReadOnly = false;
            
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/1/19
        /// 
        /// Attempts to populate the GuestID Combo box with human readable data
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            addGuests();
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/1/19
        /// 
        /// Attempts to populate the GuestID Combo box with human readable data
        /// </summary>
        private void addGuests()
        {
            try
            {
                _guests = _guestManager.RetrieveGuestNamesAndIds();
                foreach (var guest in _guests)
                {
                    _guestNames.Add(guest.FirstName + " " + guest.LastName);
                }
                cboGuestID.ItemsSource = _guestNames;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Retrieve Guest Names\n" + ex.Message);
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/1/19
        /// 
        /// Runs the validation method before attempting to insert a Vehicle into the 
        /// Database, and returns the result.
        /// </summary>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cboGuestID.SelectedIndex != -1)
            {
                _guestVehicle = new GuestVehicle
                {
                    GuestID = _guests[cboGuestID.SelectedIndex].GuestID,
                    Make = txtMake.Text,
                    Model = txtModel.Text,
                    PlateNumber = txtPlateNum.Text,
                    Color = txtColor.Text,
                    ParkingLocation = txtParkingLocation.Text
                };
            }
            else
            {
                MessageBox.Show("Please select a guest.");
            }
            if (validateFields())
            {
                try
                {
                    if (_guestVehicleManager.CreateGuestVehicle(_guestVehicle))
                    {
                        MessageBox.Show("Successfully Added Guest Vehicle");
                        clearFields();
                        this.DialogResult = true;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to Add Guest Vehicle: \n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Runs the validation method before attempting to update a vehicle
        /// in the Database, and returns the result.
        /// </summary>
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (cboGuestID.SelectedIndex != -1)
            {
                _newGuestVehicle = new GuestVehicle
                {
                    GuestID = _guests[cboGuestID.SelectedIndex].GuestID,
                    Make = txtMake.Text,
                    Model = txtModel.Text,
                    PlateNumber = txtPlateNum.Text,
                    Color = txtColor.Text,
                    ParkingLocation = txtParkingLocation.Text
                };
            }
            else
            {
                MessageBox.Show("Please select a guest.");
            }
            if (validateFields())
            {
                try
                {
                    if (_guestVehicleManager.UpdateGuestVehicle(_guestVehicle, _newGuestVehicle))
                    {
                        MessageBox.Show("Successfully Updated Guest Vehicle");
                        clearFields();
                        this.DialogResult = true;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to Update Guest Vehicle: \n" + ex.Message);
                }
            }


        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/1/19
        /// 
        /// Goes over each field on the form and makes them blank.
        /// </summary>
        private void clearFields()
        {
            cboGuestID.SelectedIndex = -1;
            txtColor.Text = "";
            txtMake.Text = "";
            txtModel.Text = "";
            txtParkingLocation.Text = "";
            txtPlateNum.Text = "";
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/1/19
        /// 
        /// Checks each Field on the form for bad data and returns the result.
        /// Displays a message if incorrect data was entered.
        /// </summary>
        private bool validateFields()
        {
            bool result = true;
            if (!_guestVehicle.validateMake())
            {
                MessageBox.Show("Invalid Entry for Make, please try again.");
                result = false;
            }
            else if (!_guestVehicle.validateModel())
            {
                MessageBox.Show("Invalid Entry for Model, please try again.");
                result = false;
            }
            else if (!_guestVehicle.validatePlateNumber())
            {
                MessageBox.Show("Invalid Entry for Plate Number, please try again.");
                result = false;
            }
            else if (!_guestVehicle.validateColor())
            {
                MessageBox.Show("Invalid Entry for Color, please try again.");
                result = false;
            }
            else if (!_guestVehicle.validateParkingLocation())
            {
                MessageBox.Show("Invalid Entry for Parking Location, please try again.");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Closes the window immediately if in read only mode, 
        /// otherwise, asks for confirmation to discard any unsaved changes.
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

            if (_isEditable)
            {
                var result = MessageBox.Show("Are you sure you want to exit? \n " +
                    "Changes will be Discarded!", "Discarding Vehicle changes...",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

    }
}
