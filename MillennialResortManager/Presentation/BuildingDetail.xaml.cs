/// <summary>
/// Danielle Russo
/// Created: 2019/01/21
/// 
/// Window that displays Building
/// </summary>
///
/// <remarks>
/// </remarks>
///

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
    /// Interaction logic for BuildingDetail.xaml
    /// </summary>
    public partial class BuildingDetail : Window
    {
        private BuildingManager buildingManager = new BuildingManager();
        private Building newBuilding;
        private Building selectedBuilding;

        private InspectionManager inspectionManager = new InspectionManager();
        private List<Inspection> inspections;

        private MaintenanceWorkOrderManagerMSSQL maintenanceManager = new MaintenanceWorkOrderManagerMSSQL();
        private List<MaintenanceWorkOrder> mainTickets;

        private RoomManager roomManager = new RoomManager();
        private List<Room> roomsInSelectedBuilding;
        private Employee employee;
        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/21
        /// 
        /// Contstructor that initializes the Building window for "Add" view.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public BuildingDetail()
        {
            InitializeComponent();
            setupBuildingInfoEditable();
            this.Title = "Add a New Building";
            this.btnPrimaryAction.Content = "Add";
            this.btnSecondaryAction.Content = "Cancel";
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/30
        /// 
        /// Constructor that initializes the Building window for "Edit" view.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        public BuildingDetail(Building building, Employee employee)
        {
            InitializeComponent();

            this.selectedBuilding = building;
            this.employee = employee;
            setupBuildingInfoEditable();
            setupSelectedBuilding();
            this.btnPrimaryAction.Content = "Save";
            this.btnSecondaryAction.Content = "Cancel";
            txtID.IsReadOnly = true;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/30
        /// 
        /// Set up window with fields populated with the selected building's
        /// information.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void setupSelectedBuilding()
        {
            txtID.Text = selectedBuilding.BuildingID;
            txtName.Text = selectedBuilding.Name;
            txtAddress.Text = selectedBuilding.Address;
            txtDescription.Text = selectedBuilding.Description;
            cboStatusID.SelectedItem = selectedBuilding.StatusID;

            setUpRooms();
            setUpInspectionTab();
            setUpMaintainanceTab();

        }

        private void setUpMaintainanceTab()
        {
            mainTickets = maintenanceManager.RetrieveAllMaintenanceWorkOrders();
            mainTickets = mainTickets.FindAll(t => t.ResortPropertyID.Equals(selectedBuilding.ResortPropertyID));


            if (mainTickets.Count > 0)
            {
                foreach (Room room in roomsInSelectedBuilding)
                {
                    List<MaintenanceWorkOrder> roomTickets = maintenanceManager.RetrieveAllMaintenanceWorkOrders()
                        .FindAll(t => t.ResortPropertyID.Equals(room.ResortPropertyID));
                    foreach (var ticket in roomTickets)
                    {
                        mainTickets.Add(ticket);
                    }
                }
            }


            dgBuildingMaintenance.ItemsSource = mainTickets;
        }


        /// <summary>
        /// Danielle Russo
        /// Created: 2019/03/19
        /// 
        /// Sets up Room datagrid with list of rooms in the selected building.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void setUpRooms()
        {
            dgRooms.ItemsSource = null;

            roomsInSelectedBuilding = roomManager.RetrieveRoomListByBuildingID(selectedBuilding.BuildingID);
            dgRooms.ItemsSource = roomsInSelectedBuilding;

            inspections = inspectionManager.RetrieveAllInspectionsByResortPropertyId(selectedBuilding.ResortPropertyID);
            dgBuildingInspections.ItemsSource = inspections;
        }

        private void setupReadOnly()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/21
        /// 
        /// Sets up an editiable version of the window, so that
        /// details of a building can be edited
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void setupBuildingInfoEditable()
        {
            txtID.IsReadOnly = false;
            txtName.IsReadOnly = false;
            txtAddress.IsReadOnly = false;
            txtDescription.IsReadOnly = false;
            cboStatusID.IsEnabled = true;
        }


        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/21
        /// 
        /// When the btnPrimaryAction button is clicked the information
        /// is either saved or added to the database.
        /// </summary>
        ///
        /// <remarks>
        /// Updated: Danielle Russo
        /// Date:    2019/03/08
        /// Added switch statement to take in account of the tabs
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrimaryAction_Click(object sender, RoutedEventArgs e)
        {
            string currentTab = ((TabItem)tabsetBldMain.SelectedItem).Header.ToString();

            switch (currentTab)
            {
                case "Info":
                    if (btnPrimaryAction.Content.ToString() == "Add")
                    {
                        createNewBuilding();
                        try
                        {
                            var buildingAdded = buildingManager.CreateBuilding(newBuilding);
                            if (buildingAdded == true)
                            {
                                this.DialogResult = true;
                                MessageBox.Show(newBuilding.BuildingID + " added.");
                            }
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message, "New building not saved.");
                        }
                        return;
                    }
                    else if (btnPrimaryAction.Content.ToString() == "Save")
                    {
                        createNewBuilding();
                        try
                        {
                            var buildingUpdated = buildingManager.UpdateBuilding(selectedBuilding, newBuilding);
                            if (buildingUpdated)
                            {
                                this.DialogResult = true;
                                MessageBox.Show(selectedBuilding.BuildingID + " updated.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Update not saved.");
                        }
                        return;
                    }
                    break;
                case "Maintenance":
                    break;
                case "Inspection":
                    break;
            }
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/21
        /// 
        /// Creates a new Building obj. to be added to the 
        /// database.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        private void createNewBuilding()
        {
            // Validate input
            if (validateAllInput())
            {
                // If validation clears, create a new building
                newBuilding = new Building()
                {
                    BuildingID = txtID.Text,
                    Name = txtName.Text,
                    Address = txtAddress.Text,
                    Description = txtDescription.Text,
                    StatusID = cboStatusID.SelectedItem.ToString()
                };
            }
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Validates input from all fields.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>True if all input was validated, false if input was not validated</returns>
        private bool validateAllInput()
        {
            bool result = false;

            // Validate all required fields have been filled out
            if (validateRequiredFields())
            {
                // Validate BuildingID
                if (validateBuildingID())
                {
                    // Validate Name
                    if (validateName())
                    {
                        // Validate Address
                        if (validateAddress())
                        {
                            // Validate Description
                            if (validateDescription())
                            {
                                // Everything was validated!
                                result = true;
                            }
                        }
                    }
                }
            }
            else
            {
                setErrorMessage("Please fill out all fields.");
            }
            return result;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Checks if description field contains a valid amount of characters.  Displays error message if length is too long.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>True if description is apporpriate length, false if description is too long</returns>
        private bool validateDescription()
        {
            bool result = true;

            // Check for Description length, max is 10000
            if (txtDescription.Text.Length > 1000)
            {
                setErrorMessage("Building description is too long, limit to 250 characters.");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Checks if address field contains a valid amount of characters.  Displays error message if length is too long.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>True if address is apporpriate length, false if address is too long</returns>
        private bool validateAddress()
        {
            bool result = true;

            // Max length 150
            if (txtAddress.Text.Length > 150)
            {
                setErrorMessage("Address it too long, limit to 150 characters");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Checks if name field contains a valid amount of characters.  Displays error message if length is too long.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>True if name is apporpriate length, false if name is too long</returns>
        private bool validateName()
        {
            bool result = true;

            // Max 150 characters
            if (txtName.Text.Length > 150)
            {
                setErrorMessage("Name is too long, limit to 150 characters");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Danielle Russo, code taken from Matt Lamarche 
        /// Created: 2019/02/04
        /// 
        /// Displays error message
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="message">The message to be displayed</param>
        private void setErrorMessage(string message)
        {
            lblError.Content = message;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Checks to see if any required fields have been left blank.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <returns>True if all req. fields are filled out, false if any req. fields are blank</returns>
        private bool validateRequiredFields()
        {
            bool result = true;
            if (txtID.Text == "" || txtName.Text == "" || txtAddress.Text == "" || txtDescription.Text == "")
            {
                result = false;
            }

            return result;

        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/04
        /// 
        /// Checks if ID field contains a valid amount of characters.  Displays error message if length is too long.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>True if ID is apporpriate length, false if ID is too long</returns>
        private bool validateBuildingID()
        {
            bool result = true;
            // Check for ID content length, max is 50
            if (txtID.Text.Length > 50)
            {
                setErrorMessage("Building ID is too long, limit to 507  characters.");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/30
        /// 
        /// Exits window then the btnSecondaryAction button is clicked.
        /// Any changes are noted saved.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSecondaryAction_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/30
        /// 
        /// Opens a new window to add a room to the selected building.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="selectedBuilding">The building that the room will be added to</param>
        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            var addRoomForm = new frmAddEditViewRoom(selectedBuilding.BuildingID, employee);
            var roomAdded = addRoomForm.ShowDialog();

            // if rooms were added, update list
            try
            {
                setUpRooms();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cboStatusID.ItemsSource = buildingManager.RetrieveAllBuildingStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/03/14
        /// 
        /// Opens a new window to add an inspection to the selected building.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddInspection_Click(object sender, RoutedEventArgs e)
        {
            var addInspectionForm = new InspectionDetail(selectedBuilding.ResortPropertyID);
            var inspectionAdded = addInspectionForm.ShowDialog();

            if (inspectionAdded == true)
            {
                try
                {
                    inspections = inspectionManager.RetrieveAllInspectionsByResortPropertyId(selectedBuilding.ResortPropertyID);
                    dgBuildingInspections.ItemsSource = inspections;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Inspection was not added.");
            }
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/03/14
        /// 
        /// Opens a new window to view the inspection record to the selected building.
        /// </summary>
        ///
        /// <remarks>
        /// Dani Russo
        /// Updated: 2019/04/18
        /// 
        /// Checks for null to prevent crashing
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectionInspection_Click(object sender, RoutedEventArgs e)
        {
            if ((Inspection)dgBuildingInspections.SelectedItem != null)
            {
                setUpSelectedInspection();
            }
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/03/14
        /// 
        /// Sets up the general building info when the "Info" tab is clicked
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabBldInfo_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/03/14
        /// 
        /// Sets up the inspection info when the "Inspections" tab is clicked
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabBldInspections_GotFocus(object sender, RoutedEventArgs e)
        {
            //setUpInspectionTab();
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/03/14
        /// 
        /// Populates fields in the "Inspections" tab
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setUpInspectionTab()
        {
            inspections = inspectionManager.RetrieveAllInspectionsByResortPropertyId(selectedBuilding.ResortPropertyID);

            if (inspections.Count > 0)
            {
                foreach (Room room in roomsInSelectedBuilding)
                {
                    inspections.AddRange(inspectionManager.RetrieveAllInspectionsByResortPropertyId(room.ResortPropertyID));
                }
            }


            dgBuildingInspections.ItemsSource = inspections;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/03/14
        /// 
        /// Makes form editable
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTernaryAction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgBuildingInspections_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((Inspection)dgBuildingInspections.SelectedItem != null)
            {
                setUpSelectedInspection();
            }
        }

        private void setUpSelectedInspection()
        {
            Inspection selectedInspection = (Inspection)dgBuildingInspections.SelectedItem;
            var inspectionDetailForm = new InspectionDetail(selectedInspection);
            var inspectionUpdated = inspectionDetailForm.ShowDialog();

            if (inspectionUpdated == true)
            {
                try
                {
                    inspections = inspectionManager.RetrieveAllInspectionsByResortPropertyId(selectedBuilding.ResortPropertyID);
                    dgBuildingInspections.ItemsSource = inspections;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void grdBldInspections_GotFocus(object sender, RoutedEventArgs e)
        {
            //setUpInspectionTab();
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/04/04
        /// 
        /// Goes to the room detail form
        /// </summary>
        ///
        /// <remarks>
        /// Dani Russo
        /// Updated: 2019/04/18
        /// 
        /// Checks for null to prevent crashing
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgRooms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((Room)dgRooms.SelectedItem != null)
            {
                selectRoom();
            }
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/04/04
        /// 
        /// Goes to the room detail form
        /// </summary>
        private void selectRoom()
        {
            Room selectedRoom = (Room)dgRooms.SelectedItem;

            var detailForm = new frmAddEditViewRoom(EditMode.View, selectedRoom.RoomID);
            var formUpdated = detailForm.ShowDialog();

            setUpRooms();

            if (formUpdated == true)
            {
                try
                {
                    setUpRooms();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        /// <summary>
        /// Danielle Russo
        /// Created: 2019/04/04
        /// 
        /// Goes to the room detail form
        /// </summary>
        ///
        /// <remarks>
        /// Dani Russo
        /// Updated: 2019/04/18
        /// 
        /// Checks for null to prevent crashing
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectRoom_Click(object sender, RoutedEventArgs e)
        {
            if ((Room)dgRooms.SelectedItem != null)
            {
                selectRoom();
            }
        }

        private void dgBuildingMaintenance_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnAddMaintenance_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSelectionMaintenance_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
