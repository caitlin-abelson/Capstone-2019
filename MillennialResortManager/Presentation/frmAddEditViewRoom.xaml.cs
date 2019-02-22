/// <summary>
/// Wes Richardson
/// Created: 2019/01/24
/// 
/// Handles the Controls and Displayed information for Adding, Editing and View room Details
/// </summary>
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
    public partial class frmAddEditViewRoom : Window
    {
        private RoomManager _roomMgr;
        bool inputsGood = false;
        private RoomDetailMode _mode = RoomDetailMode.Add;
        Room rm;
        Building bd;
        RoomType rt;
        int roomID;

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// Constructor for the Window when adding a room
        /// </summary>
        public frmAddEditViewRoom()
        {
            _roomMgr = new RoomManager();
            rm = new Room();
            bd = new Building();
            rt = new RoomType();
            RoomDetailMode _mode = RoomDetailMode.Add;
            InitializeComponent();
            
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// Constructor for the Window when adding a room
        /// <param name="mode">If the window sould be in View mode or Edit Mode</param>
        /// <param name="roomID">The ID of the Room to View or Edit</param>
        /// </summary>
        public frmAddEditViewRoom(RoomDetailMode mode, int roomID)
        {
            _roomMgr = new RoomManager();
            this._mode = mode;
            this.roomID = roomID;
            InitializeComponent();
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// Populates the data and setups the controls when the window loads
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_mode == RoomDetailMode.View)
            {
                try
                {
                    rm = _roomMgr.RetreieveRoomByID(roomID);
                    populateControls();
                    setupViewMode();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Could not find Room", MessageBoxButton.OK, MessageBoxImage.Warning);

                    MessageBox.Show(ex.ToString());
                }
            }
            else if (_mode == RoomDetailMode.Edit)
            {
                try
                {
                    rm = _roomMgr.RetreieveRoomByID(roomID);
                    populateControls();
                    setupEditMode();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Could not find Room", MessageBoxButton.OK, MessageBoxImage.Warning);

                    MessageBox.Show(ex.ToString());
                }
            }
            else // This would mean the only other option would be Add
            {
                setupAddMode();
            }
            this.cboBuilding.ItemsSource = _roomMgr.RetrieveBuildingList();
            this.cboRoomType.ItemsSource = _roomMgr.RetrieveRoomTypeList();
            this.cboRoomStatus.ItemsSource = _roomMgr.RetrieveRoomStatusList();
            this.cboOfferingID.ItemsSource = _roomMgr.RetrieveOfferingIDList();
            this.cboPropertyID.ItemsSource = _roomMgr.RetrieveResortPropertyIDList();
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// Controls what happens when the Add or Edit button is clicked based on the mode of the window
        /// </summary>
        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if(_mode == RoomDetailMode.View)
            {
                _mode = RoomDetailMode.Edit;
                setupEditMode();
            }
            else if(_mode == RoomDetailMode.Edit)
            {
                CheckInputs();
                if(inputsGood)
                {
                    try
                    {
                        bool updated = _roomMgr.UpdateRoom(rm);
                        if (updated == true)
                        {
                            MessageBox.Show("Room Updated");
                        }
                        else
                        {
                            MessageBox.Show("Room Was not updated");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Room Creation Failed!", MessageBoxButton.OK, MessageBoxImage.Warning);

                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else // must be in add mode
            {
                CheckInputs();
                if (inputsGood)
                {
                    try
                    {
                        bool created = _roomMgr.CreateRoom(rm);
                        if (created == true)
                        {
                            MessageBox.Show("Room Added");
                        }
                        else
                        {
                            MessageBox.Show("Room was not added");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Room Creation Failed!", MessageBoxButton.OK, MessageBoxImage.Warning);

                        MessageBox.Show(ex.ToString());
                    }
                    txtRoomNumber.Text = "";
                    txtDescription.Text = "";
                    iudCapacity.Value = 1;
                    cbxAvailable.IsChecked = false;
                }
            }
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// Checks the inputs for correct data
        /// </summary>
        private void CheckInputs()/* Add checks for Text box lengths */
        {

            if (string.IsNullOrEmpty(txtRoomNumber.Text)) // Length in DB 15
            {
                MessageBox.Show("Please enter a Room Number");
                inputsGood = false;
            }
            else if (cboBuilding.SelectedItem == null)
            {
                MessageBox.Show("Please select a building");
                inputsGood = false;
            }
            else if (cboRoomType.SelectedItem == null)
            {
                MessageBox.Show("Please select a Room Type");
                inputsGood = false;
            }
            else if (string.IsNullOrEmpty(txtDescription.Text)) // length in DB is 50
            {
                MessageBox.Show("Please enter a description");
                inputsGood = false;
            }
            else if (iudCapacity.Value == null || iudCapacity.Value.Value < 1)
            {
                MessageBox.Show("Room Capacity must be at least 1");
                inputsGood = false;
            }
            else if(iudPrice.Value == null || iudPrice.Value.Value < 1)
            {
                MessageBox.Show("Room price must be at least $1");
                inputsGood = false;
            }
            else if(cboRoomStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a Room Status");
                inputsGood = false;
            }
            else if (cboOfferingID.SelectedItem == null)
            {
                MessageBox.Show("Please select a Offering ID");
                inputsGood = false;
            }
            else if (cboPropertyID.SelectedItem == null)
            {
                MessageBox.Show("Please select a Resort Property ID");
                inputsGood = false;
            }
            else
            {
                rm.RoomNumber = txtRoomNumber.Text.Trim();
                rm.Building = this.cboBuilding.SelectedItem.ToString();
                rm.RoomType = this.cboRoomType.SelectedItem.ToString();
                rm.Description = txtDescription.Text;
                rm.Capacity = iudCapacity.Value.Value;
                rm.Price = iudPrice.Value.Value;
                rm.Available = (bool)cbxAvailable.IsChecked;
                rm.RoomStatus = this.cboRoomStatus.SelectedItem.ToString();
                rm.OfferingID = int.Parse(this.cboOfferingID.SelectedItem.ToString());
                rm.ResortPropertyID = int.Parse(this.cboPropertyID.SelectedItem.ToString());
                inputsGood = true;
            }
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/28
        /// 
        /// Sets up the window when in Add mode
        /// </summary>
        private void setupAddMode()
        {
            lockInputs(false);
            btnAddEdit.Content = "Add Room";
            this.Title = "Add Room";
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/28
        /// 
        /// Sets up the window when in Edit mode
        /// </summary>
        private void setupEditMode()
        {
            lockInputs(false);
            btnAddEdit.Content = "Edit Room";
            this.Title = "Edit Room";

        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/28
        /// 
        /// Sets up the window when in View mode
        /// </summary>
        private void setupViewMode()
        {
            lockInputs(true);
            btnAddEdit.Content = "Edit Room";
            this.Title = "View Room";
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/28
        /// 
        /// Inserts the data in the fields
        /// </summary>
        private void populateControls()
        {
            txtRoomNumber.Text = rm.RoomNumber;
            cboBuilding.SelectedItem = rm.Building;
            cboRoomType.SelectedItem = rm.RoomType;
            iudCapacity.Value = rm.Capacity;
            cbxAvailable.IsChecked = rm.Available;
            txtDescription.Text = rm.Description;
            iudPrice.Value = (int)rm.Price;
            cboRoomStatus.SelectedItem = rm.RoomStatus;
            cboOfferingID.SelectedItem = rm.OfferingID;
            cboPropertyID.SelectedItem = rm.ResortPropertyID;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/28
        /// 
        /// Locks or Unlocks the inputs
        /// </summary>
        private void lockInputs(bool readOnly)
        {
            this.txtRoomNumber.IsReadOnly = readOnly;
            this.cboBuilding.IsEnabled = !readOnly;
            this.cboRoomType.IsEnabled = !readOnly;
            this.iudCapacity.IsEnabled = !readOnly;
            this.cbxAvailable.IsEnabled = !readOnly;
            this.txtDescription.IsReadOnly = readOnly;
            this.iudPrice.IsEnabled = !readOnly;
            this.cboRoomStatus.IsEnabled = !readOnly;
            this.cboOfferingID.IsEnabled = !readOnly;
            this.cboPropertyID.IsEnabled = !readOnly;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}