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
    /// Created : 3/27/2019
    /// Interaction logic for CreateHouseKeepingRequest.xaml
    /// </summary>
    public partial class CreateHouseKeepingRequest : Window
    {
        IHouseKeepingRequestManager _houseKeepingRequestManager;
        HouseKeepingRequest _existingHouseKeepingRequest;


        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// This constructor is used for Creating a HouseKeepingRequest
        /// Initializes connections to our HouseKeepingRequestManager
        /// </summary>
        public CreateHouseKeepingRequest(IHouseKeepingRequestManager houseKeepingRequestManager)
        {
            InitializeComponent();
            _houseKeepingRequestManager = houseKeepingRequestManager;


            chkActive.IsEnabled = false;
            chkActive.IsChecked = true;
            chkActive.Visibility = Visibility.Hidden;
            txtWorkingEmployee.IsEnabled = false;
            txtWorkingEmployee.Equals(null);
            _existingHouseKeepingRequest = null;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// This constructor is used for Reading and Updating a HouseKeepingRequest
        /// </summary>
        public CreateHouseKeepingRequest(HouseKeepingRequest existingHouseKeepingRequest, IHouseKeepingRequestManager houseKeepingRequestManager)
        {
            InitializeComponent();
            _houseKeepingRequestManager = houseKeepingRequestManager;
            _existingHouseKeepingRequest = existingHouseKeepingRequest;
            txtWorkingEmployee.Equals(null);
            populateFormReadOnly();
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// populates all the fields for the form 
        /// </summary>
        private void populateFormReadOnly()
        {
            txtBuildingNumber.Text = _existingHouseKeepingRequest.BuildingNumber.ToString();
            txtRoomNumber.Text = _existingHouseKeepingRequest.RoomNumber.ToString();
            txtDescription.Text = _existingHouseKeepingRequest.Description;
            chkActive.IsChecked = _existingHouseKeepingRequest.Active;
            txtWorkingEmployee.Text = _existingHouseKeepingRequest.WorkingEmployeeID.ToString();
            setReadOnly();
            btnSave.Content = "Update";
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Sets the form to read only and hides the buttons which cannot be used in read only mode
        /// </summary>
        private void setReadOnly()
        {
            txtBuildingNumber.IsReadOnly = true;
            txtRoomNumber.IsReadOnly = true;
            txtDescription.IsReadOnly = true;
            chkActive.IsEnabled = false;
            txtWorkingEmployee.IsReadOnly = true;

        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Sets the form to editable and shows the buttons which may need to be used in create/edit mode
        /// </summary>
        private void setEditable()
        {
            txtBuildingNumber.IsReadOnly = false;
            txtRoomNumber.IsReadOnly = false;
            txtDescription.IsReadOnly = false;
            txtWorkingEmployee.IsReadOnly = false;
            chkActive.IsEnabled = true;
            btnSave.Content = "Submit";
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// The function which runs when Save is clicked
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (((string)btnSave.Content) == "Submit")
            {
                if (!ValidateInput())
                {
                    return;
                }
                HouseKeepingRequest newHouseKeepingRequest = new HouseKeepingRequest();
                newHouseKeepingRequest.Active = (bool)chkActive.IsChecked;
                newHouseKeepingRequest.BuildingNumber = int.Parse(txtBuildingNumber.Text);
                newHouseKeepingRequest.RoomNumber = int.Parse(txtRoomNumber.Text);
                newHouseKeepingRequest.Description = txtDescription.Text;
                try
                {
                    if (_existingHouseKeepingRequest == null)
                    {
                        _houseKeepingRequestManager.AddHouseKeepingRequest(newHouseKeepingRequest);
                        SetError("");
                        MessageBox.Show("House Keeping Request Created Successfully: " +
                        "\nBuildingNumber: " + newHouseKeepingRequest.BuildingNumber +
                        "\nRoomNumber: " + newHouseKeepingRequest.RoomNumber +
                        "\nDescription: " + newHouseKeepingRequest.Description);
                    }
                    else
                    {
                        newHouseKeepingRequest.Active = (bool)chkActive.IsChecked;
                        newHouseKeepingRequest.WorkingEmployeeID = int.Parse(txtWorkingEmployee.Text);
                        _houseKeepingRequestManager.EditHouseKeepingRequest(_existingHouseKeepingRequest, newHouseKeepingRequest);
                        SetError("");
                        MessageBox.Show("House Keeping Request Updated Successfully: " +
                        "\nOld BuildingNumber: " + _existingHouseKeepingRequest.BuildingNumber +
                        "\nOld RoomNumber: " + _existingHouseKeepingRequest.RoomNumber +
                        "\nOld Description: " + _existingHouseKeepingRequest.Description +
                        "\n" +
                        "\nNew BuidlingNumber: " + newHouseKeepingRequest.BuildingNumber +
                        "\nNew RoomNumber: " + newHouseKeepingRequest.RoomNumber +
                        "\nNew Description: " + newHouseKeepingRequest.Description +
                        "\nNew WorkingEmployeeID: " + newHouseKeepingRequest.WorkingEmployeeID);
                    }
                }
                catch (Exception ex)
                {
                    SetError(ex.Message);
                }

                Close();
            }
            else if (((string)btnSave.Content) == "Update")
            {
                setEditable();
            }
            else
            {
                MessageBox.Show(btnSave.Content.GetType() + " " + btnSave.Content);
            }

        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Sets and displays the error to be showed on screen
        /// </summary>
        private void SetError(string error)
        {
            lblError.Content = error;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// A checklist which validates all the input in the form
        /// </summary>
        private bool ValidateInput()
        {
            if (ValidateBuildingNumber())
            {
                if (ValidateRoomNumber())
                {
                    if (ValidateDescription())
                    {
                        return true;
                    }
                    else
                    {
                        SetError("INVALID DESCRIPTION");
                    }
                }
                else
                {
                    SetError("INVALID ROOM NUMBER");
                }
            }
            else
            {
                SetError("INVALID BUILDING NUMBER");
            }
            return false;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Checks whether the Building Number is valid.
        /// </summary>
        private bool ValidateBuildingNumber()
        {
            int b;
            if (txtBuildingNumber.Text == null || txtBuildingNumber.Text == "" || int.TryParse(txtBuildingNumber.Text, out b) == false)
            {
                return false;
            }

            //Chose a range of 1-9999. Can be changed as needed
            if (int.Parse(txtBuildingNumber.Text) >= 1 && int.Parse(txtBuildingNumber.Text) <= 9999)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Checks whether the RoomNumber is valid.
        /// </summary>
        private bool ValidateRoomNumber()
        {
            int r;
            if (txtRoomNumber.Text == null || txtRoomNumber.Text == "" || int.TryParse(txtRoomNumber.Text, out r) == false)
            {
                return false;
            }

            //Chose a range of 1-9999. Can be changed as needed
            if (int.Parse(txtRoomNumber.Text) >= 1 && int.Parse(txtRoomNumber.Text) <= 9999)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Checks whether the Description is valid.
        /// </summary>
        private bool ValidateDescription()
        {
            if (txtDescription.Text == null || txtDescription.Text == "")
            {
                return false;
            }

            //Chose a range of 1-1000. Can be changed as needed
            if (txtDescription.Text.Length >= 1 && txtDescription.Text.Length <= 1001)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// The function which runs when Cancel is clicked
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
