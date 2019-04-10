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
        private EditMode _mode = EditMode.Add;
        Room rm;
        Building bd;
        RoomType rt;
        int roomID;
        int employeeID;

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// Constructor for the Window when adding a room
        /// </summary>
        public frmAddEditViewRoom(int employeeID = 100000)
        {
            _roomMgr = new RoomManager();
            rm = new Room();
            bd = new Building();
            rt = new RoomType();
            EditMode _mode = EditMode.Add;
            this.employeeID = employeeID;
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
        public frmAddEditViewRoom(EditMode mode, int roomID)
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
            if (_mode == EditMode.View)
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
            else if (_mode == EditMode.Edit)
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
            try
            {
                this.cboBuilding.ItemsSource = _roomMgr.RetrieveBuildingList();
                this.cboRoomType.ItemsSource = _roomMgr.RetrieveRoomTypeList();
                this.cboRoomStatus.ItemsSource = _roomMgr.RetrieveRoomStatusList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// Controls what happens when the Add or Edit button is clicked based on the mode of the window
        /// </summary>
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/04
        /// 
        /// Removed all references of Available or Active checkboxes
        /// </remarks>
        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == EditMode.View)
            {
                _mode = EditMode.Edit;
                setupEditMode();
            }
            else if (_mode == EditMode.Edit)
            {
                CheckInputs();
                if (inputsGood)
                {
                    try
                    {
                        bool updated = _roomMgr.UpdateRoom(rm);
                        if (updated == true)
                        {
                            MessageBox.Show("Room Updated");
                            this.Close();
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
            else if (_mode == EditMode.Add)
            {
                CheckInputs();
                if (inputsGood)
                {
                    try
                    {
                        bool created = _roomMgr.CreateRoom(rm, employeeID, (int)iudNumberOfRooms.Value);
                        if (created == true)
                        {
                            MessageBox.Show("Room Added");
                            this.Close();
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
                }
            }
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// Checks the inputs for correct data
        /// </summary>
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/05
        /// Removed Active and Available references
        /// </remarks>
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
            else if (dudPrice.Value == null || dudPrice.Value.Value < 1)
            {
                MessageBox.Show("Room price must be at least $1");
                inputsGood = false;
            }
            else if (cboRoomStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a Room Status");
                inputsGood = false;
            }
            else
            {
                rm.RoomNumber = txtRoomNumber.Text.Trim();
                rm.Building = this.cboBuilding.SelectedItem.ToString();
                rm.RoomType = this.cboRoomType.SelectedItem.ToString();
                rm.Description = txtDescription.Text;
                rm.Capacity = iudCapacity.Value.Value;
                rm.Price = dudPrice.Value.Value;
                rm.RoomStatus = this.cboRoomStatus.SelectedItem.ToString();
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
            btnAddEdit.Content = "Save Room";
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
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/05
        /// Removed Active and Available references
        /// </remarks>
        private void populateControls()
        {
            txtRoomNumber.Text = rm.RoomNumber;
            cboBuilding.SelectedItem = rm.Building;
            cboRoomType.SelectedItem = rm.RoomType;
            iudCapacity.Value = rm.Capacity;
            txtDescription.Text = rm.Description;
            dudPrice.Value = rm.Price;
            cboRoomStatus.SelectedItem = rm.RoomStatus;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/28
        /// 
        /// Locks or Unlocks the inputs
        /// </summary>
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/04
        /// 
        /// Removed all references of Available or Active checkboxes
        /// </remarks>
        private void lockInputs(bool readOnly)
        {
            this.txtRoomNumber.IsReadOnly = readOnly;
            this.cboBuilding.IsEnabled = !readOnly;
            this.cboRoomType.IsEnabled = !readOnly;
            this.iudCapacity.IsEnabled = !readOnly;
            this.txtDescription.IsReadOnly = readOnly;
            this.dudPrice.IsEnabled = !readOnly;
            this.cboRoomStatus.IsEnabled = !readOnly;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}